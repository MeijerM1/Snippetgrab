using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    class Message
    {
        public int ID { get; set; }
        public string MessageContent { get; set; }
        public User Sender { get; set; }
        public User Receipent { get; set; }

        public Message(int id, string messageContent, User sender, User receipent)
        {
            ID = id;
            MessageContent = messageContent;
            Sender = sender;
            Receipent = receipent;
        }
    }
}
