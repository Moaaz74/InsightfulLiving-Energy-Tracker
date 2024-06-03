using Back_end.Controllers;
using Back_end.DTOS.Home;
using Back_end.Models;
using Back_end.Services.HomeService;
using Backend.Tests.Test_Cases_Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;

namespace Backend.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller;
        private Mock<IHomeService> _homeService;
        private Mock<UserManager<ApplicationUser>> _userManager;

        [SetUp]
        public void SetUp()
        {
            _homeService = new Mock<IHomeService>();
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _controller = new HomeController(_userManager.Object , _homeService.Object);
        }

        [Test]
        [TestCaseSource(typeof(TestData))]
        public async Task<int> CheckHomeCreating(string? UserId , string? NoRooms)
        {
            HomeCreateDto dto = new HomeCreateDto() { NumberOfRooms = NoRooms, UserId = UserId };

            var result =  await  _controller.CreateHome(dto);
            int statusCode = result switch
            {
                NotFoundObjectResult notFoundResult => (int)notFoundResult.StatusCode,
                BadRequestObjectResult badRequestResult => (int)badRequestResult.StatusCode,
                OkObjectResult okResult => (int)okResult.StatusCode,
                _ => throw new Exception("Unexpected result type"), // Handle unexpected results
            };
            return statusCode;
        }
        
        public class TestData : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                StreamReader r = new StreamReader("C:\\Users\\moaaz\\source\\repos\\InsightfulLiving-Energy-Tracker\\Back-end\\Backend.Tests\\Test cases\\CreateHomeActionTestData.json");
                string json = r.ReadToEnd();

                List<CreateHomeData> Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CreateHomeData>>(json);

                foreach (var item in Items)
                {
                    yield return new TestCaseData(item.UserId, item.NumberOfRooms).Returns(item.Expected.StatusCode);
                }
            }
        }

    }
}
