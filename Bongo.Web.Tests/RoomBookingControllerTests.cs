using Bongo.Core.Services;
using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Web.Tests
{
    [TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _bookingSvcMock;
        private RoomBookingController _bookingController;

        [SetUp]
        public void Setup()
        {
            _bookingSvcMock = new Mock<IStudyRoomBookingService>();
            _bookingController = new RoomBookingController(_bookingSvcMock.Object);
        }

        [Test]
        public void Index_GetRequest_VerifyGetAllInvoked()
        {
            _bookingController.Index();

            _bookingSvcMock.Verify(x => x.GetAllBooking(), Times.Once);
        }

        [Test]
        public void Book_ModelStateInValid_ReturnsSameView()
        {
            _bookingController.ModelState.AddModelError("error", "error");

            var result = _bookingController.Book(new StudyRoomBooking());

            ViewResult viewResult = result as ViewResult;

            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.AreEqual("Book", viewResult.ViewName);
        }

        [Test]
        public void Book_NotSuccessful_ReturnsNoRoomAvailableCode()
        {
            _bookingSvcMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns(new StudyRoomBookingResult() 
                {
                    Code = StudyRoomBookingCode.NoRoomAvailable
                });

            var result = _bookingController.Book(new StudyRoomBooking());

            ViewResult viewResult = result as ViewResult;

            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.AreEqual("No Study Room available for selected date", viewResult.ViewData["Error"]);
        }

        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            //arrage
            _bookingSvcMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.Success,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Date = booking.Date,
                    Email = booking.Email
                });

            //act
            var result = _bookingController.Book(new StudyRoomBooking()
            {
                Date = DateTime.Now,
                Email = "john@dotnetmastery.com",
                FirstName = "John",
                LastName = "DotNetMastery",
                StudyRoomId = 1
            });

            //assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = result as RedirectToActionResult;
            Assert.AreEqual("John", actionResult.RouteValues["FirstName"]);
            Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
        }
    }
}
