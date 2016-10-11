using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippetgrab.data
{
    public interface IUserContext
    {
        List<User> GetAll();

        User GetById(int id);

        User GetByEmail(string email);

        List<User> GetAllAdmin();

        bool Insert(User user);

        bool Remove(int id);

        bool Update(User user);

        bool CheckPassword(string email, string password);
    }
}
