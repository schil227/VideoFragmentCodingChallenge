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
    /* Coding Challenge: Things I thought of
     * 
     *      Strict Moq:
     *      Unit tests that are very specific are wonderful. They
     *  Ensure that the code is behaving *exactly* as expected.
     *  I've heard some developers equate using strict mocks to 
     *  pouring concrete all over the implementation, however
     *      1) This is a good thing. As a developer, you need to
     *          pay the price of knowing exactly what you wrote
     *          and how it works at least once, and when you do,
     *          you can trust the unit tests. And:
     *      2) Being irritated that the unit tests have to change
     *          Is more indicative that the code is doing too much.
     *      The Alternative to strict mocking is to have loose mocking,
     *      which undermines the whole point of unit testing. Unit tests
     *      are supposed to be the first line of defence against bugs
     *      by forcing you to look at what you've written, and to 
     *      ensure that other developers (or you) in the future can
     *      confidently say that something is working.
     *      
     *      </soapbox>
     */
    public class FragmentLoaderServiceTests
    {
        private MockRepository moq;
        private Mock<IStreamReaderFactory> mockStreamReaderFactory;
        private Mock<IFragmentParserService> mockFragmentParserService;
        private IFragmentLoaderService sut;

        [SetUp]
        public void SetUp()
        {
            moq = new MockRepository(MockBehavior.Strict);
            mockStreamReaderFactory = moq.Create<IStreamReaderFactory>();
            mockFragmentParserService = moq.Create<IFragmentParserService>();
            sut = new FragmentLoaderService(mockStreamReaderFactory.Object, mockFragmentParserService.Object);
        }

        [TearDown]
        public void TearDown() => moq.VerifyAll();

        [Test]
        public void Load_WhenStreamReaderFactoryErrorsOnTheFileName_ThrowsException()
        {
            // Arrange
            var filename = "test.txt";
            mockStreamReaderFactory.Setup(f => f.Create(filename)).Throws(new IOException());

            // Act
            var result = Assert.Throws<FragmentPopulatorServiceException>(() => sut.Load(filename));

            // Assert
            Assert.That(result.Message, Is.EqualTo($"Failed to read the file {filename}"));
        }

        [Test]
        public void Load_WhenNoLinesFound_ReturnsEmptyList()
        {
            // Arrange
            var filename = "test.txt";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(""));

            mockStreamReaderFactory.Setup(f => f.Create(filename)).Returns(new StreamReader(stream));

            // Act
            var result = sut.Load(filename);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Load_WhenOnlyLineIsCommentedOut_ReturnsEmptyList()
        {
            // Arrange
            var filename = "test.txt";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("#"));

            mockStreamReaderFactory.Setup(f => f.Create(filename)).Returns(new StreamReader(stream));

            // Act
            var result = sut.Load(filename);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Load_WhenOnlyLineIsWhiteSpace_ReturnsEmptyList()
        {
            // Arrange
            var filename = "test.txt";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(" "));

            mockStreamReaderFactory.Setup(f => f.Create(filename)).Returns(new StreamReader(stream));

            // Act
            var result = sut.Load(filename);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Load_WhenOneValidLine_ReturnsOneFragment()
        {
            // Arrange
            var filename = "test.txt";

            var fragment = new Fragment
            {
                StartTime = 123,
                EndTime = 456
            };

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes("123:456"));

            mockStreamReaderFactory.Setup(f => f.Create(filename)).Returns(new StreamReader(stream));
            mockFragmentParserService.Setup(s => s.Parse("123:456")).Returns(fragment);

            // Act
            var result = sut.Load(filename);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(fragment));
        }

        [Test]
        public void Load_WhenSeveralDifferentLinesSupplied_ReturnsSeveralFragment()
        {
            // Arrange
            var filename = "test.txt";

            var fragment1 = new Fragment
            {
                StartTime = 123,
                EndTime = 456
            };

            var fragment2 = new Fragment
            {
                StartTime = 11,
                EndTime = 22
            };

            StringBuilder testFile = new StringBuilder();
            testFile.AppendLine("# My Test file");
            testFile.AppendLine("              ");
            testFile.AppendLine("123:456");
            testFile.AppendLine("11:22");


            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(testFile.ToString()));

            mockStreamReaderFactory.Setup(f => f.Create(filename)).Returns(new StreamReader(stream));
            mockFragmentParserService.Setup(s => s.Parse("123:456")).Returns(fragment1);
            mockFragmentParserService.Setup(s => s.Parse("11:22")).Returns(fragment2);

            // Act
            var result = sut.Load(filename);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First(), Is.EqualTo(fragment1));
            Assert.That(result.Last(), Is.EqualTo(fragment2));
        }
    }
}
