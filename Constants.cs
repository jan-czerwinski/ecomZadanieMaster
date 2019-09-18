using System;
using System.Collections.Generic;
using System.Text;

namespace ecomZadanie
{
    public static class Constants
    {
        public static string UsersUrl = "https://internshiptaskuserslist.azurewebsites.net/api/users?code=gbgu4CbgdAlsS0xIVaNkckK4vTd0qIFNxaQYzIHLaqyomquJwuy/ig==";
        public static string UserDetailUrl(int Id)  //this probably shouldnt be in constants
        {
            string baseUrl = "https://internshiptaskuserslist.azurewebsites.net/api/users/{0}?code=9XuCxWZqJavOAWHPcWD/97mMeJkK0mSVMA9A6MQ9n4R1B/6fpsxGqw==";
            return string.Format(baseUrl, Id);
        }
    }
}
