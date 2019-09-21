using System;
using System.Collections.Generic;
using System.Text;

namespace ecomZadanie.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
    public class UserDetailsRoot
    {
        public bool IsSuccess { get; set; }
        public UserDetails Data { get; set; }
    }
}
