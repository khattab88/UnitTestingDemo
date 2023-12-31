﻿using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
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

        private StudyRoomBooking _request;
        private List<StudyRoom> _availableRooms;

        [SetUp]
        public void Setup()
        {
            _request = new()
            {
                FirstName = "Ben",
                LastName = "Stark",
                Email = "bstark@gmail.com",
                Date = new DateTime(2024, 1, 1)
            };

            _availableRooms = new List<StudyRoom>{
                new StudyRoom
                {
                    Id = 1,
                    RoomName = "Regular",
                    RoomNumber = "A101"
                }};

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

        [TestCase]
        public void BookStudyRoom_InputNullRequest_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => _bookingSvc.BookStudyRoom(null));

            //Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);    
            Assert.AreEqual("request", exception.ParamName);
        }

        [TestCase]
        public void BookStudyRoom_BookWithAvailableRoom_ReturnsResultWithAllValues()
        {
            // arrange
            StudyRoomBooking savedStudyRoomBooking = null;

            _roomRepoMock.Setup(x => x.GetAll()).Returns(_availableRooms);
            _bookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    savedStudyRoomBooking = booking;
                });

            // act
            _bookingSvc.BookStudyRoom(_request);

            // assert
            _bookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            Assert.NotNull(savedStudyRoomBooking);
            Assert.AreEqual(_request.FirstName, savedStudyRoomBooking.FirstName);
            Assert.AreEqual(_request.LastName, savedStudyRoomBooking.LastName);
            Assert.AreEqual(_request.Email, savedStudyRoomBooking.Email);
            Assert.AreEqual(_request.Date, savedStudyRoomBooking.Date);
            Assert.AreEqual(_availableRooms.First().Id, savedStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValuesMatchInResult()
        {
            StudyRoomBookingResult result = _bookingSvc.BookStudyRoom(_request);

            Assert.NotNull(result);
            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
            Assert.AreEqual(_request.Email, result.Email);
            Assert.AreEqual(_request.Date, result.Date);
        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvability_ReturnsSuccessResultCode(bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableRooms.Clear();
            }
            return _bookingSvc.BookStudyRoom(_request).Code;
        }

        [TestCase(0, false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailalbility_ReturnsBookingId
            (int expectedBookingId, bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableRooms.Clear();
            }


            _bookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = 55;
                });

            var result = _bookingSvc.BookStudyRoom(_request);
            Assert.AreEqual(expectedBookingId, result.BookingId);
        }

        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked()
        {
            _availableRooms.Clear();
            var result = _bookingSvc.BookStudyRoom(_request);
            _bookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);

        }
    }
}
