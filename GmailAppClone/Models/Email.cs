using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GmailAppClone.Models
{
    public class Email : INotifyPropertyChanged
    {
        // Email Model
        public Email(string from, string to, string body = null, string subject = "(empty subject)", string base64Image = null)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            Date = DateTime.UtcNow;
            Base64Attachment = base64Image;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Base64Attachment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

