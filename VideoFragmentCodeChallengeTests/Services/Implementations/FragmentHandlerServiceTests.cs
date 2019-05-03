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
        private Mock<IUniqueViewTimeCalculatorService> mockUniqueViewTimeCalculatorService;
        private Mock<IFragmentLoaderService> mockFragmentLoaderService;
        private IFragmentHandlerService sut;

        [SetUp]
        public void SetUp()
        {
            moq = new MockRepository(MockBehavior.Strict);
            mockUniqueViewTimeCalculatorService = moq.Create<IUniqueViewTimeCalculatorService>();
            mockFragmentLoaderService = moq.Create<IFragmentLoaderService>();
            sut = new FragmentHandlerService(mockFragmentLoaderService.Object, mockUniqueViewTimeCalculatorService.Object);
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
            mockUniqueViewTimeCalculatorService.Setup(s => s.CalculateTotal(loadedFragments)).Returns(0);

            // Act
            var result = sut.Handle(fragmentFileName);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
    }
}
