using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;
using VideoFragmentCodeChallenge.Services.Implementations;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallengeTests.Services.Implementations
{
    public class FragmentTotalCalculatorServiceTests
    {
        private IFragmentTotalCalculatorService sut;

        [SetUp]
        public void SetUp()
        {
            sut = new FragmentTotalCalculatorService();
        }

        [Test]
        public void CalculateTotal_WhenNoFragmentsSupplied_Returns0()
        {
            // Arrange
            var fragments = new List<Fragment>();

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
    }
}
