using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UberBarber.User
{
    public class CurrentUser
    {
        static CurrentUser current_user = new();

        public bool Is_worker = false;
        public string Username = "none";

        public static void Set_username_permissions(string username, bool is_worker)
        {
            current_user.Username = username;
            current_user.Is_worker= is_worker;
        }

        public static string Get_username()
        {
            return current_user.Username;
        }

        public static bool Get_is_worker()
        {
            return current_user.Is_worker;
        }
    }

}
