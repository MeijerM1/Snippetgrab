using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snippetgrab.data;

namespace Snippetgrab.Logic
{
    public class UserRepository
    {
        private IUserContext context;
        
        public UserRepository(IUserContext context)
        {
            this.context = context;
        }

        public List<User> GetAll()
        {
            return context.GetAll();
        }

        public List<User> GetAllAdmin()
        {
            return context.GetAllAdmin();
        }

        public User GetById(int id)
        {
            return context.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return context.GetByEmail(email);
        }

        public bool Insert(User user)
        {
            return context.Insert(user);
        }

        public bool Remove(User user)
        {
            return context.Remove(user);
        }

        public bool Update(User user)
        {
            return context.Update(user);
        }

        public bool CheckPasssword(string email, string password)
        {
            return context.CheckPassword(email, password);
        }
    }
}
