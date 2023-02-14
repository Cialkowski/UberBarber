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

        public static bool Is_worker = false;
        public static string Username = "none";
        public static int User_id = 0;

        public static void Set_username_permissions(string username, bool is_worker)
        {
            Username = username;
            Is_worker= is_worker;
        }

        public static void Set_user_id(int user_id)
        {
            User_id = user_id;
        }

        public static int Get_user_id()
        {
            return User_id;
        }

        public static string Get_username()
        {
            return Username;
        }

        public static bool Get_is_worker()
        {
            return Is_worker;
        }
    }

}
