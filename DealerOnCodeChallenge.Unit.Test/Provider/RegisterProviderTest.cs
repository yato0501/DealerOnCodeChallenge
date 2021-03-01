using DealerOnCodeChallenge.Business;
using DealerOnCodeChallenge.Models;
using DealerOnCodeChallenge.Provider;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOnCodeChallenge.Unit.Test.Provider
{
    [TestFixture]
    public class RegisterProviderTest
    {
        private MockRepository _mockRepo;
        private Mock<IInteractionProvider> _mockInteractionProvider;
        private Mock<IItemStoreProvider> _mockItemStoreProvider;
        private Mock<ITotalCalculatorProvider> _mockTotalCalculatorProvider;
        private RegisterProvider _sut;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new MockRepository(MockBehavior.Strict);
            _mockInteractionProvider = _mockRepo.Create<IInteractionProvider>();
            _mockItemStoreProvider = _mockRepo.Create<IItemStoreProvider>();
            _mockTotalCalculatorProvider = _mockRepo.Create<ITotalCalculatorProvider>();

            _mockInteractionProvider.Setup(x => x.AddCustomItemKey).Returns("c");
            _mockInteractionProvider.Setup(x => x.CheckoutKey).Returns("t");

            IServiceCollection sc = new ServiceCollection();
            IServiceProvider sp = sc.AddScoped(s => _mockInteractionProvider.Object)
            .AddScoped(s => _mockItemStoreProvider.Object)
            .AddScoped(s => _mockTotalCalculatorProvider.Object)
            .AddScoped<RegisterProvider>()
            .BuildServiceProvider();

            _sut = sp.GetRequiredService<RegisterProvider>();


        }

        [TearDown]
        public void TearDown()
        {
            _mockRepo.VerifyAll();
        }

        [Test]
        public void RegisterProcess_invalidKeyEntered_callsNotifyInvalidInputToDisplayMessage()
        {
            // Arrange
            var expectedAvailableItems = new List<Item>();
            var expectedKey1 = "wrongKey";
            var expectedKey2 = "t";
            var expectedCalculatedTotal = new List<CalculatedItem>();

            _mockInteractionProvider.Setup(x => x.PrintItemsFromCart(It.IsAny<List<Item>>()));
            _mockInteractionProvider.Setup(x => x.PrintItemCatalog());
            _mockInteractionProvider.SetupSequence(x => x.GetUserInputItemOrInstruction())
                .Returns(expectedKey1)
                .Returns(expectedKey2);
            _mockItemStoreProvider.Setup(x => x.GetReadonlyAvailableItems()).Returns(expectedAvailableItems.AsReadOnly());
            _mockInteractionProvider.Setup(x => x.NotifyInvalidInput());
            _mockTotalCalculatorProvider.Setup(x => x.CalculateItemTotals(It.IsAny<IEnumerable<Item>>())).Returns(expectedCalculatedTotal);
            _mockInteractionProvider.Setup(x => x.ShowTotal(It.IsAny<IEnumerable<CalculatedItem>>()));


            // Act
            _sut.RegisterProcess();


            // Assert
            _mockInteractionProvider.Verify(x => x.NotifyInvalidInput(), Times.Once);
        }
    }
}
