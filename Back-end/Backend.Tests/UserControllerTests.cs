using Back_end.Controllers;
using Back_end.DTOS.User;
using Back_end.Models;
using Backend.Tests.Test_Cases_Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<UserManager<ApplicationUser>> _userManager;
        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userController = new UserController(_userManager.Object, null, null);
        }

        [Test]
        [TestCaseSource(typeof(TestData))]
        public async Task<int> CheckUserLogin(string Usernaem , string Password)
        {
            LoginDTO loginDTO = new LoginDTO() { Username = Usernaem, Password = Password };

             if(!loginDTO.Username.IsNullOrEmpty() && !loginDTO.Password.IsNullOrEmpty() && loginDTO.Username == "Ahmed") 
            {
                ApplicationUser user = null;
                _userManager.Setup(manager => manager.FindByNameAsync(loginDTO.Username)).ReturnsAsync(user);
            }
            else if(loginDTO.Password != "Mo@@z011" && loginDTO.Username == "Moaaz")
            {
                ApplicationUser user = new ApplicationUser();
                _userManager.Setup(manager => manager.FindByNameAsync(loginDTO.Username)).ReturnsAsync(user);
                _userManager.Setup(manager => manager.CheckPasswordAsync(user , loginDTO.Password)).ReturnsAsync(false);
            }
            var result = await userController.Login(loginDTO);
            int statusCode = result switch
            {
                UnauthorizedResult unauthorizedObjectResult => (int)unauthorizedObjectResult.StatusCode,
                BadRequestResult badRequestResult => (int)badRequestResult.StatusCode,
                _ => throw new Exception("Unexpected result type"),
            };
            return statusCode;
        }


        public class TestData : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                StreamReader r = new StreamReader("C:\\Users\\moaaz\\source\\repos\\InsightfulLiving-Energy-Tracker\\Back-end\\Backend.Tests\\Test cases\\LoginTestCases.json");
                string json = r.ReadToEnd();

                List<LoginData> Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LoginData>>(json);

                foreach (var item in Items)
                {
                    yield return new TestCaseData(item.Username, item.Password).Returns(item.Expected.StatusCode);
                }
            }
        }
    }
}
