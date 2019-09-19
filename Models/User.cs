using System;
using System.Collections.Generic;
using System.Text;

namespace ecomZadanie.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserRoot
    {
        public bool IsSuccess { get; set; }
        public List<User> Data { get; set; }
    }
}
