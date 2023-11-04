using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.DataAccess.Tests
{
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
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

        [Test]
        public void Book_WithStudyRoomBookingOne_CheckValuesFromDb()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_bongo").Options;

            // Act
            using (var context = new ApplicationDbContext(dbContextOptions)) 
            {
                var repository = new StudyRoomBookingRepository(context);

                repository.Book(StudyRoomBookingOne);
            }

            // Assert
            using (var context = new ApplicationDbContext(dbContextOptions)){
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
    }
}
