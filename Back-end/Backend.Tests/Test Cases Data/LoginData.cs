using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Test_Cases_Data
{
    public class LoginData
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ExpectedResult Expected { get; set; }
    }
}
