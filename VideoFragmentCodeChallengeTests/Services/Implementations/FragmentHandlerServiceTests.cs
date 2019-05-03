using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;
using VideoFragmentCodeChallenge.Services.Exceptions;
using VideoFragmentCodeChallenge.Services.Implementations;
using VideoFragmentCodeChallenge.Services.Interfaces;
using VideoFragmentCodeChallenge.Services.Interfaces.Factories;

namespace VideoFragmentCodeChallengeTests.Services.Implementations
{
    public class FragmentHandlerServiceTests
    {
        private MockRepository moq;
        private Mock<IFragmentTotalCalculatorService> mockFragmentTotalCalculatorService;
        private Mock<IFragmentLoaderService> mockFragmentLoaderService;
        private IFragmentHandlerService sut;

        [SetUp]
        public void SetUp()
        {
            moq = new MockRepository(MockBehavior.Strict);
            mockFragmentTotalCalculatorService = moq.Create<IFragmentTotalCalculatorService>();
            mockFragmentLoaderService = moq.Create<IFragmentLoaderService>();
            sut = new FragmentHandlerService(mockFragmentLoaderService.Object, mockFragmentTotalCalculatorService.Object);
        }

        [TearDown]
        public void TearDown() => moq.VerifyAll();

        [Test]
        public void Handle_WhenFragmentDataFileNameProvided_LoadsFragmentsAndCalculatesTotals()
        {
            // Arrange
            var fragmentFileName = "myfile.txt";
            var loadedFragments = new List<Fragment>
            {
                new Fragment()
            };

            mockFragmentLoaderService.Setup(s => s.Load(fragmentFileName)).Returns(loadedFragments);
            mockFragmentTotalCalculatorService.Setup(s => s.CalculateTotal(loadedFragments)).Returns(0);

            // Act
            var result = sut.Handle(fragmentFileName);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
    }
}
