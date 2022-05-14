using System.Collections;
using System.Collections.Generic;
using System.Linq;
using grud_backend.Models;

namespace grud_backend.Helpers
{
    public  static class HelperMethods
    {
        public static IEnumerable<User> WithoutPassword(this IEnumerable<User> users)
        {
            return users.Select(user => user.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}