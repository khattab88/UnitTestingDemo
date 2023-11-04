using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.DataAccess.Tests
{
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        private StudyRoomBooking StudyRoomBookingOne;
        private StudyRoomBooking StudyRoomBookingTwo;

        public StudyRoomBookingRepositoryTests()
        {
            StudyRoomBookingOne = new()
            {
                FirstName = "Ben",
                LastName = "Stark",
                Email = "bstark@gmail.com",
                Date = new DateTime(2024, 1, 1),
                StudyRoomId = 1,
                BookingId = 11,
            };

            StudyRoomBookingTwo = new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "jdoe@gmail.com",
                Date = new DateTime(2024, 2, 2),
                StudyRoomId = 2,
                BookingId = 22,
            };
        }

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_bongo").Options;
        }


        [Test]
        [Order(1)]
        public void Book_WithStudyRoomBookingOne_CheckValuesFromDb()
        {
            // Arrange
            
            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new StudyRoomBookingRepository(context);

                repository.Book(StudyRoomBookingOne);
            }

            // Assert
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(b => b.BookingId == 11);

                Assert.IsNotNull(bookingFromDb);
                Assert.AreEqual(StudyRoomBookingOne.BookingId, bookingFromDb?.BookingId);
                Assert.AreEqual(StudyRoomBookingOne.StudyRoomId, bookingFromDb?.StudyRoomId);
                Assert.AreEqual(StudyRoomBookingOne.FirstName, bookingFromDb?.FirstName);
                Assert.AreEqual(StudyRoomBookingOne.LastName, bookingFromDb?.LastName);
                Assert.AreEqual(StudyRoomBookingOne.Email, bookingFromDb?.Email);
                Assert.AreEqual(StudyRoomBookingOne.Date, bookingFromDb?.Date);
            }
        }

        [Test]
        [Order(2)]
        public void GetAll_WithBookingOneAndTwo_CheckBothBookingsFromDb()
        {
            // Arrange
            List<StudyRoomBooking> expected = new() { StudyRoomBookingOne, StudyRoomBookingTwo };

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();

                var repository = new StudyRoomBookingRepository(context);

                repository.Book(StudyRoomBookingOne);
                repository.Book(StudyRoomBookingTwo);
            }

            // Act
            List<StudyRoomBooking> result = new();
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new StudyRoomBookingRepository(context);

                result = repository.GetAll(null).ToList();
            }

            // Assert
            CollectionAssert.AreEqual(expected, result, new StudyRoomBookingComparer());
        }
    }

    class StudyRoomBookingComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            StudyRoomBooking booking1 = (StudyRoomBooking)x;
            StudyRoomBooking booking2 = (StudyRoomBooking)y;

            if (booking1?.BookingId != booking2?.BookingId) return 1;

            return 0;
        }
    }
}
