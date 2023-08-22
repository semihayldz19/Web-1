using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.InAndOutModels
{
    public class MUser
    {

        public class Form
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? NickName { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
        public class FilterForm : MFilterFormBase
        {

        }

        public class Response
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string NickName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class LoginForm
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
       
    }
}
