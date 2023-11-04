using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private Mock<IStudyRoomRepository> _roomRepoMock;
        private Mock<IStudyRoomBookingRepository> _bookingRepoMock;
        private StudyRoomBookingService _bookingSvc;

        [SetUp]
        public void Setup()
        {
            _roomRepoMock = new Mock<IStudyRoomRepository>();
            _bookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _bookingSvc = new StudyRoomBookingService(_bookingRepoMock.Object, _roomRepoMock.Object);
        }

        [TestCase]
        public void GetAll_Invoke_VerifyMethodIsCalled()
        {
            _bookingSvc.GetAllBooking();

            _bookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }
    }
}
