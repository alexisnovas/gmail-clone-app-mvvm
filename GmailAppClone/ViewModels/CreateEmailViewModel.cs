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

        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public ImageSource Attachment { get; set; }

        private string _base64Image { get; set; }
        private string _attachmentPath { get; set; }

        public ICommand CreateEmailCommand { get; set; }
        public ICommand AttachPhotoCommand { get; set; }

        public CreateEmailViewModel(ObservableCollection<Models.Email> emails)
        {
            _emails = emails;

            CreateEmailCommand = new Command(CreateEmail);
            AttachPhotoCommand = new Command(AttachPhoto);
        }

        private async void CreateEmail()
        {
            if (areRequiredFieldsEmpty())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Missing sender or receiver", "Ok");

                return;
            }

            var newEmail = new Models.Email(From, To, Body, Subject, _base64Image);
            _emails.Add(newEmail);

            var jsonString = JsonConvert.SerializeObject(_emails);
            Preferences.Set("emails", jsonString);

            await App.Current.MainPage.Navigation.PopAsync();

            OpenExternalEmailApp();
            SendNotification();
        }

        private bool areRequiredFieldsEmpty()
        {
            bool isSenderEmpty = string.IsNullOrEmpty(From);
            bool isReceiverEmpty = string.IsNullOrEmpty(To);

            return isSenderEmpty && isReceiverEmpty;
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

        private async void SendNotification()
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

        private async void AttachPhoto()
        {
            var image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            _attachmentPath = image.FullPath;
            _base64Image = Convert.ToBase64String(File.ReadAllBytes(image.FullPath));

            var imageStream = await image.OpenReadAsync();
            Attachment = ImageSource.FromStream(() => imageStream);
        }
    }
}
