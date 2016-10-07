using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    class Problem
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
        public bool IsSolved { get; set; }
        public User Author { get; set; }

        public Problem(int id, string text, bool isSolved, User author)
        {
            ID = id;
            Text = text;
            IsSolved = isSolved;
            Author = author;
        }

        public void AddPoint()
        {
            Points++;
        }

        public void ChangedSolvedStatus(bool status)
        {
            IsSolved = status;
        }
    }
}
