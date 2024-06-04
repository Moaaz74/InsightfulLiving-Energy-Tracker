using Back_end.Controllers;
using Back_end.DTOS.Room;
using Back_end.Services.RoomService;
using Backend.Tests.Test_Cases_Data;
using Microsoft.AspNetCore.Mvc;
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
    public class RoomControllerTest
    {
        private Mock<IRoomService> roomService;
        private RoomController roomController;

        [SetUp]
        public void SetUp()
        {
            roomService = new Mock<IRoomService> ();
            roomController = new RoomController(roomService.Object);
        }

        [Test]
        [TestCaseSource(typeof(TestData))]
        public async Task<int> CheckRoomViewing(int id)
        {
            RoomViewDto dto = null;
            if (id == 1)
            {
                dto = new RoomViewDto();

            }
            roomService.Setup(service => service.ViewRoom(id)).ReturnsAsync(dto);
            var result = await roomController.ViewRoom(id);
            int statusCode = result.Result switch
            {
                NotFoundObjectResult notFoundObjectResult => (int)notFoundObjectResult.StatusCode,
                BadRequestResult badRequestResult => (int)badRequestResult.StatusCode,
                OkObjectResult okObjectResult => (int)okObjectResult.StatusCode,
                _ => throw new Exception("Unexpected result type"),
            };

            return statusCode;
        }

        public class TestData : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                StreamReader r = new StreamReader("C:\\Users\\moaaz\\source\\repos\\InsightfulLiving-Energy-Tracker\\Back-end\\Backend.Tests\\Test cases\\ViewRoomTestData.json");
                string json = r.ReadToEnd();

                List<ViewRoomData> Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ViewRoomData>>(json);

                foreach (var item in Items)
                {
                    yield return new TestCaseData(item.Id).Returns(item.Expected.StatusCode);
                }
            }
        }

    }
}
