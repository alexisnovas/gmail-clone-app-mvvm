﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GmailAppClone.Models;
using GmailAppClone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GmailAppClone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultEmailDetailPage : ContentPage
    {
        public ConsultEmailDetailPage(Email email)
        {
            InitializeComponent();
            BindingContext = new ConsultEmailDetailViewModel(email);
        }
    }
}