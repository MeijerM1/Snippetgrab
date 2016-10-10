using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    class Snippet
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int Points { get; set; }
        public bool IsPrivate { get; set; }
        public User Author { get; set; }

        public Snippet(int id, string code, bool isPrivate, User author)
        {
            ID = id;
            Code = code;
            IsPrivate = isPrivate;
            Author = author;
        }

        public void AddPoint()
        {
            Points++;
        }

        public void ChangePrivate(bool status)
        {
            IsPrivate = status;
        }
    }
}
