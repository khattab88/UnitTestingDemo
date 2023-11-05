using Bongo.Core.Services.IServices;
using Bongo.Web.Controllers;
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
    }
}
