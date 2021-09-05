using GmailAppClone.Models;
using GmailAppClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GmailAppClone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEmailPage : ContentPage
    {
        public CreateEmailPage(ObservableCollection<Email> emails)
        {
            InitializeComponent();
            BindingContext = new CreateEmailViewModel(emails);
        }
    }
}