using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GmailAppClone.Models
{
    public class Email : INotifyPropertyChanged
    {
        // Email Model
        public Email(string from, string to, string body = null, string subject = "(empty subject)", string base64ImageAttachment = null)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            ImageAttachment = base64ImageAttachment;
            Date = DateTime.UtcNow; // Taking the actual date with UtcNow method.
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ImageAttachment { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

