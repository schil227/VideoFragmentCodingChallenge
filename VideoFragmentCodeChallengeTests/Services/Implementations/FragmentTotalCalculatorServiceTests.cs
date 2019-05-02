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

        [Test]
        public void CalculateTotal_WhenOnlyOneFragment_ReturnsTheTotalFromThatFragment()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void CalculateTotal_WhenTwoFragmentsDoNotOverlap_ReturnsCombinedSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 6,
                    EndTime = 8
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void CalculateTotal_WhenTwoFragmentsAreAdjacent_ReturnsCombinedSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 4,
                    EndTime = 8
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void CalculateTotal_WhenTwoFragmentsAreOverlapping_ReturnsSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 3,
                    EndTime = 8
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void CalculateTotal_WhenTwoFragmentsTheSame_ReturnsSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void CalculateTotal_WhenFragmentEclipsedByAnother_ReturnsSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 1,
                    EndTime = 3
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void CalculateTotal_WhenTwoFragmentsOutOfOrder_ReturnsSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 3,
                    EndTime = 8
                },
                new Fragment
                {
                    StartTime = 0,
                    EndTime = 4
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void CalculateTotal_WhenSeveralFragmentsCalculated_ReturnsSum()
        {
            // Arrange
            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    StartTime = 3,
                    EndTime = 8
                },
                new Fragment
                {
                    StartTime = 1,
                    EndTime = 4
                },
                new Fragment
                {
                    StartTime = 2,
                    EndTime = 3
                },
                new Fragment
                {
                    StartTime = 10,
                    EndTime = 13
                },
                new Fragment
                {
                    StartTime = 15,
                    EndTime = 18
                },
                new Fragment
                {
                    StartTime = 18,
                    EndTime = 20
                }
            };

            // Act
            var result = sut.CalculateTotal(fragments);

            // Assert
            Assert.That(result, Is.EqualTo(15));
        }
    }
}
