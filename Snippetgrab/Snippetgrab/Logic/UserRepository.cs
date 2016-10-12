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
        private readonly IUserContext _context;
        
        public UserRepository(IUserContext context)
        {
            this._context = context;
        }

        public List<User> GetAll()
        {
            return _context.GetAll();
        }

        public List<User> GetAllAdmin()
        {
            return _context.GetAllAdmin();
        }

        public User GetById(int id)
        {
            return _context.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return _context.GetByEmail(email);
        }

        public bool Insert(User user, string password)
        {
            return _context.Insert(user, password);
        }

        public bool Remove(string email)
        {
            return _context.Remove(email);
        }

        public bool Update(User user)
        {
            return _context.Update(user);
        }

        public bool CheckPasssword(string email, string password)
        {
            return _context.CheckPassword(email, password);
        }
    }
}
