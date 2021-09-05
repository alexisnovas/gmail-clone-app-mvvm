using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GmailAppClone.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailItemContentView : ContentView
    {
        public EmailItemContentView()
        {
            InitializeComponent();
        }
    }
}