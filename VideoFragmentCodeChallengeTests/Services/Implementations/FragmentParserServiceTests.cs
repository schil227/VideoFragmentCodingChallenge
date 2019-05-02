using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.Services.Exceptions;
using VideoFragmentCodeChallenge.Services.Implementations;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallengeTests.Services.Implementations
{
    public class FragmentParserServiceTests
    {
        private IFragmentParserService sut;

        [SetUp]
        public void SetUp()
        {
            sut = new FragmentParserService();
        }

        [Test]
        public void Parse_WhenFragmentIsEmptyString_ThrowsException()
        {
            var result = Assert.Throws<FragmentParserServiceException>(() => sut.Parse(string.Empty));

            Assert.That(result.Message, Is.EqualTo($"Error parsing fragment from line \"\": line is not in the correct format. Expected: \"<digits> : <digits>\""));
        }

        [Test]
        public void Parse_WhenNumberTooLarge_ThrowsException()
        {
            // Arrange 
            var fragmentLine = "1234:12341234123412341234";

            // Act
            var result = Assert.Throws<FragmentParserServiceException>(() => sut.Parse(fragmentLine));

            // Assert
            Assert.That(result.Message, Is.EqualTo($"Error parsing fragment from line \"{fragmentLine}\": Failed to parse number."));
        }

        [Test]
        public void Parse_WhenEndTimeBeforeStartTime_Throwsexception()
        {
            // Arrange 
            var fragmentLine = "5:2";

            // Act
            var result = Assert.Throws<FragmentParserServiceException>(() => sut.Parse(fragmentLine));

            // Assert
            Assert.That(result.Message, Is.EqualTo($"Error parsing fragment from line \"{fragmentLine}\": Fragment StartTime cannot be greater than fragment EndTime."));
        }

        [Test]
        public void Parse_WhenLineWellFormed_ReturnsFragment()
        {
            // Arrange 
            var fragmentLine = "2:5";

            // Act
            var result = sut.Parse(fragmentLine);

            // Assert
            Assert.That(result.StartTime, Is.EqualTo("2"));
            Assert.That(result.StartTime, Is.EqualTo("5"));
        }
    }
}
