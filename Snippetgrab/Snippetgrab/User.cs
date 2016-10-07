using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Reputation { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public User(int id, string name, DateTime joindate, int reputation, string email, bool IsAdmin)
        {
            ID = id;
            Name = name;
            JoinDate = joindate;
        }

        public void ChangePassword(string password)
        {
            throw new NotImplementedException();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void AddReputation()
        {
            Reputation++;
        }

        public void ChangeAdminStatus(bool status)
        {
            IsAdmin = status;
        }
    }
}
