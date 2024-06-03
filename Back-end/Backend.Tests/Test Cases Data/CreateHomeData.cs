using Back_end.DTOS.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Test_Cases_Data
{
    public class CreateHomeData
    {
        public string Name { get; set; }
        public string NumberOfRooms { get; set; }
        public string UserId {  get; set; }
        public ExpectedResult Expected { get; set; }
    }

    public class ExpectedResult
    {
        public int StatusCode { get; set; }
    }
}
