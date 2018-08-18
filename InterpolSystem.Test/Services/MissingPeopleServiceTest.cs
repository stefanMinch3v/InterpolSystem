namespace InterpolSystem.Test.Services
{
    using FluentAssertions;
    using InterpolSystem.Data;
    using InterpolSystem.Data.Models;
    using InterpolSystem.Data.Models.Enums;
    using InterpolSystem.Services.Implementations;
    using InterpolSystem.Services.Models.MissingPeople;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class MissingPeopleServiceTest
    {

        public MissingPeopleServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public void ShouldReturnCorrectResultWithCorrectModel()
        {
            // Arrange
            var db = this.GetDatabase();

            var missingPeopleData = this.GetMissingPeopleData();

            db.AddRange(missingPeopleData);

            db.SaveChanges();

            var missingPeopleService = new MissingPeopleService(db);

            // Act
            var result = missingPeopleService.All().OrderBy(r => r.Id);

            // Assert
            result
                .Should().AllBeOfType<MissingPeopleListingServiceModel>()
                .And.HaveCount(2);

            result
                .Should().Match(r =>
                    r.ElementAt(0).Id == 1
                    && r.ElementAt(1).Id == 2);
        }

        [Fact]
        public void ShouldReturnCorrectPersonDetails()
        {
            // Arrange
            var db = this.GetDatabase();

            var missingPeopleData = this.GetMissingPeopleData();

            db.AddRange(missingPeopleData);

            db.SaveChanges();

            var missingPeopleService = new MissingPeopleService(db);

            // Act
            var resultPesho = missingPeopleService.GetPerson(1);
            var resultIvan = missingPeopleService.GetPerson(2);

            // Assert
            resultPesho.FirstName
                .Should().Match(r => r.Equals("Pesho"));

            resultIvan.FirstName
                .Should().Match(r => r.Equals("Ivan"));
        }

        [Fact]
        public void ShouldReturnTrueIfPersonExists()
        {
            // Arrange
            var db = this.GetDatabase();

            var missingPeopleData = this.GetMissingPeopleData();

            db.AddRange(missingPeopleData);

            db.SaveChanges();

            var missingPeopleService = new MissingPeopleService(db);

            // Act
            var result = missingPeopleService.IsPersonExisting(1);

            // Assert
            result
                .Should().BeTrue();
        }

        [Fact]
        public void ShouldGetTheCorrectResultForSearchingCriteria()
        {
            // Arrange
            var db = this.GetDatabase();

            var missingPeopleData = this.GetMissingPeopleData();

            db.AddRange(missingPeopleData);

            db.SaveChanges();

            var missingPeopleService = new MissingPeopleService(db);

            // Act
            var searchName = "Pesho";
            var result = missingPeopleService.SearchByComponents(false, 0, false, Gender.Male, searchName, null, null, 0);

            // Assert
            result
                .Should().HaveCount(1)
                .And.Match(r => r.ElementAt(0).FirstName == searchName);
        }

        [Fact]
        public void ShouldReturnTwoAsMethodCountIsInvoked()
        {
            // Arrange
            var db = this.GetDatabase();

            var missingPeopleData = this.GetMissingPeopleData();

            db.AddRange(missingPeopleData);

            db.SaveChanges();

            var missingPeopleService = new MissingPeopleService(db);

            // Act
            var result = missingPeopleService.Total();

            // Assert
            result
                .Should().Be(2);
        }

        private IEnumerable<IdentityParticularsMissing> GetMissingPeopleData()
        {
            var firstMissingPerson = new IdentityParticularsMissing
            {
                Id = 1,
                FirstName = "Pesho",
                LastName = "Peshev",
                DateOfBirth = DateTime.UtcNow,
                PlaceOfDisappearance = "UK, London"
            };

            var secondMissingPerson = new IdentityParticularsMissing
            {
                Id = 2,
                FirstName = "Ivan",
                LastName = "Ivanov",
                DateOfBirth = DateTime.UtcNow,
                PlaceOfDisappearance = "UK, Liverpool"
            };

            return new List<IdentityParticularsMissing> { firstMissingPerson, secondMissingPerson };
        }

        private InterpolDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<InterpolDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options; // create everytime unique name otherwise all methods will share the same database between them

            return new InterpolDbContext(dbOptions);
        }
    }
}
