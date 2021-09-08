using GmailAppClone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace GmailAppClone.ViewModels
{
    public class ConsultEmailDetailViewModel : BaseViewModel
    {
        public Email chossenEmail { get; set; }
        public ImageSource Attachment
        {
            get
            {
                if (chossenEmail.ImageAttachment != null)
                {
                    var byteImageArray = Convert.FromBase64String(chossenEmail.ImageAttachment);
                    var stream = new MemoryStream(byteImageArray);

                    return ImageSource.FromStream(() => stream);
                }

                return null; // If there's no image attachment we just return null.
            }
        }

        // Consult email constructor
        public ConsultEmailDetailViewModel(Email email)
        {
            chossenEmail = email;
        }
    }
}
