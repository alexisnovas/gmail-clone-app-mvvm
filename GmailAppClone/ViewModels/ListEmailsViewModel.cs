using GmailAppClone.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GmailAppClone.ViewModels
{
    public class ListEmailsViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Email> Emails { get; set; } = new ObservableCollection<Models.Email>();

        private Models.Email _selectedEmail;
        public Models.Email SelectedEmail
        {
            get
            {
                return _selectedEmail;
            }
            set
            {
                _selectedEmail = value;

                if (_selectedEmail != null)
                {
                    SelectedEmailCommand.Execute(_selectedEmail);
                    SelectedEmail = null;
                }
            }
        }

        public ICommand NavigateToCreateEmailCommand { get; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand SelectedEmailCommand { get; set; }

        public ListEmailsViewModel()
        {
            NavigateToCreateEmailCommand = new Command(OnCreateEmail);
            SelectedEmailCommand = new Command<Models.Email>(OnEmailSelected);
            DeleteEmailCommand = new Command<Models.Email>(DeleteEmail);

            LoadEmails();
        }

        private async void OnCreateEmail()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateEmailPage(Emails));
        }

        private void DeleteEmail(Models.Email email)
        {
            Emails.Remove(email);

            var jsonString = JsonConvert.SerializeObject(Emails);
            Preferences.Set("emails", jsonString);
        }

        private void LoadEmails()
        {
            var emailListString = Preferences.Get("emails", null);

            if (emailListString != null)
            {
                var emailList = JsonConvert.DeserializeObject<ObservableCollection<Models.Email>>(emailListString);
                Emails = emailList;
            }
        }

        private async void OnEmailSelected(Models.Email email)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ConsultEmailDetailPage(email));
        }
    }
}
