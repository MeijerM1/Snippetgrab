using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Reputation { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        // Very insecure, temporary solution
        protected readonly string password;

        public User(string name, DateTime joindate, int reputation, string email, bool isAdmin, string password)
        {
            Name = name;
            JoinDate = joindate;
            Reputation = reputation;
            Email = email;
            IsAdmin = isAdmin;
            this.password = password;
        }

        public User(int id, string name, DateTime joindate, int reputation, string email, bool isAdmin, string password)
        {
            ID = id;
            Name = name;
            JoinDate = joindate;
            Reputation = reputation;
            Email = email;
            IsAdmin = isAdmin;
            this.password = password;
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

        public string GetPassword()
        {
            return password;
        }
    }
}
