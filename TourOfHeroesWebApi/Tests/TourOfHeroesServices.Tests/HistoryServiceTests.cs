using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TourOfHeroesData.Common.Contracts;
using TourOfHeroesData.Models;
using TourOfHeroesServices.Contracts;
using TourOfHeroesServices.Tests.Common;
using Xunit;

namespace TourOfHeroesServices.Tests
{
    public class HistoryServiceTests
    {
        private IHistoryService _historyService;

        private List<EditHistory> GetTestData()
        {
            return new List<EditHistory>()
            {
                new EditHistory()
                {
                    Id = 1,
                    NewValue = "New value",
                    OldValue = "Old value",
                    EditedOn = DateTime.Now
                },
                new EditHistory()
                {
                    Id = 2,
                    NewValue = "New value",
                    OldValue = "Old value",
                    EditedOn = DateTime.Now
                }
            };
        }

        public HistoryServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(100);
        }

        [Fact]
        public void GetAllHistory_WithExistentData_ShouldReturnHistoryCorrect()
        {
            string errorMessagePrefix = "BlogService GetAllPosts() method does not work properly.";

            var historyRepo = new Mock<IRepository<EditHistory>>();

            historyRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._historyService = new HistoryService(historyRepo.Object);

            var expectedResults = GetTestData();
            var actualResults = this._historyService.GetAllHistory().ToList();

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.NewValue == actualEntry.NewValue, errorMessagePrefix + " " + "NewValue is not returned properly.");
                Assert.True(expectedEntry.OldValue == actualEntry.OldValue, errorMessagePrefix + " " + "OldValue is not returned properly.");
                Assert.True(expectedEntry.EditedOn.ToString(CultureInfo.CurrentCulture) == actualEntry.EditedOn.ToString(CultureInfo.CurrentCulture), errorMessagePrefix + " " + "EditedOn is not returned properly.");
            }
        }

        [Fact]
        public void GetAllHistory_WithNonExistentData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "BlogService GetAllPosts() method does not work properly.";

            var historyRepo = new Mock<IRepository<EditHistory>>();

            historyRepo.Setup(x => x.All()).Returns(new List<EditHistory>(0).AsQueryable());

            this._historyService = new HistoryService(historyRepo.Object);

            var expectedResults = 0;
            var actualResults = this._historyService.GetAllHistory().ToList().Count;

            Assert.True(expectedResults == actualResults, errorMessagePrefix);
        }

        [Fact]
        public void DeleteHistory_WithExistentHistoryId_ShouldDeleteHistoryProperly()
        {
            string errorMessagePrefix = "BlogService DeleteHistory() method does not work properly.";

            var historyRepo = new Mock<IRepository<EditHistory>>();

            historyRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._historyService = new HistoryService(historyRepo.Object);

            var isDeleted = this._historyService.DeleteHistory(1).IsCompletedSuccessfully;

            Assert.True(isDeleted, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteHistory_WithNonExistentHistoryId_ShouldThrowInvalidOperationException()
        {
            var historyRepo = new Mock<IRepository<EditHistory>>();

            historyRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._historyService = new HistoryService(historyRepo.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._historyService.DeleteHistory(-1));
        }
    }
}