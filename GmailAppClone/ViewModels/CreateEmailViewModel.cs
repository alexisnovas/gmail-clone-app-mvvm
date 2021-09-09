using System;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GmailAppClone.ViewModels
{
    public class CreateEmailViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Email> _emails;

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ImageSource Attachment { get; set; }

        private string _base64ImageAttachment { get; set; }
        private string _attachmentPath { get; set; }

        // Declaring commands
        public ICommand CreateEmailCommand { get; set; }
        public ICommand AttachImageCommand { get; set; }

        public CreateEmailViewModel(ObservableCollection<Models.Email> emails)
        {
            _emails = emails;

            CreateEmailCommand = new Command(CreateEmail);
            AttachImageCommand = new Command(AttachImage);
        }

        private bool requiredFieldsEmpty()
        {
            bool isSenderEmpty = string.IsNullOrEmpty(From);
            bool isReceiverEmpty = string.IsNullOrEmpty(To);

            return isSenderEmpty && isReceiverEmpty;
        }

        private async void CreateEmail()
        {
            if (requiredFieldsEmpty())
            {
                await App.Current.MainPage.DisplayAlert("Error", "The Sender or receiver field cannot be empty", "Ok");

                return;
            }

            var newEmail = new Models.Email(From, To, Subject, Body, _base64ImageAttachment);
            _emails.Add(newEmail);

            // Converting the emails colections to a JSON format.
            var jsonString = JsonConvert.SerializeObject(_emails);
            Preferences.Set("emails", jsonString);

            await App.Current.MainPage.Navigation.PopAsync();

            OpenExternalEmailApp(); // Function to open the real email app passing the subject and body data.
            PushNotification(); // Function for pushing a notification after the email is created.
        }


        private async void OpenExternalEmailApp()
        {
            var recipients = new List<string>() { To };
            var attachments = new List<EmailAttachment>() { new EmailAttachment(_attachmentPath) };

            var message = new EmailMessage
            {
                Subject = Subject,
                Body = Body,
                To = recipients,
                Attachments = attachments,
            };

            await Xamarin.Essentials.Email.ComposeAsync(message);
        }

        private async void PushNotification()
        {
            var notification = new NotificationRequest
            {
                Title = "Local Notification",
                Description = "Email sent successfully!",
                Schedule =
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                }
            };

            await NotificationCenter.Current.Show(notification);
        }

        private async void AttachImage()
        {
            var image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please select an image from your gallery"
            });

            _attachmentPath = image.FullPath;
            _base64ImageAttachment = Convert.ToBase64String(File.ReadAllBytes(image.FullPath));

            var imageStream = await image.OpenReadAsync();
            Attachment = ImageSource.FromStream(() => imageStream);
        }
    }
}
