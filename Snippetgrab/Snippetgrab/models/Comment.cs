using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        public Comment ReplyTo { get; set; }
        public Problem BelongsTo { get; set; }

        public Comment(int id, string text, User author, Comment replyto, Problem belongsTo)
        {
            ID = id;
            Text = text;
            Author = author;
            ReplyTo = replyto;
            BelongsTo = belongsTo;
        }
    }
}
