using Allure.NUnit;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Helper;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace DotsAndBoxes.Game.Core.Test.Helper
{
    [AllureNUnit]
    public class EdgeHelperTests
    {
        [Test]
        public void TryDecodeEdgeId_When0_ReturnCoord()
        {
            // Arrange
            int edgeId = 0;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Horizontal,
                x = 0,
                y = 0
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);
            
            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When11_ReturnCoord()
        {
            // Arrange
            int edgeId = 11;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Horizontal,
                x = 11,
                y = 0
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When12_ReturnCoord()
        {
            // Arrange
            int edgeId = 12;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Horizontal,
                x = 0,
                y = 1
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When155_ReturnCoord()
        {
            // Arrange
            int edgeId = 155;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Horizontal,
                x = 11,
                y = 12
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When156_ReturnCoord()
        {
            // Arrange
            int edgeId = 156;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 0,
                y = 0
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When168_ReturnCoord()
        {
            // Arrange
            int edgeId = 168;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 0,
                y = 1
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryDecodeEdgeId_When311_ReturnCoord()
        {
            // Arrange
            int edgeId = 311;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 11,
                y = 12
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordActual, Is.EqualTo(coordExpected));
        }

        [Test]
        public void TryEncode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            EdgeCoord coord = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 11,
                y = 12
            };
            int edgeId;

            int ExpectededgeId = 311;

            // Act
            EdgeHelper.TryEncode(coord, out edgeId);
            
            
            // Assert
            Assert.That(edgeId, Is.EqualTo(ExpectededgeId));
        }

        [Test]
        public void IsValidEdgeId_StateUnderTest_ExpectedBehavior()
        {
            // Valid edge IDs
            Assert.That(EdgeHelper.IsValidEdgeId(0), Is.True);
            Assert.That(EdgeHelper.IsValidEdgeId(155), Is.True);
            Assert.That(EdgeHelper.IsValidEdgeId(156), Is.True);
            Assert.That(EdgeHelper.IsValidEdgeId(311), Is.True);

            // Invalid edge IDs
            Assert.That(EdgeHelper.IsValidEdgeId(-1), Is.False);
            Assert.That(EdgeHelper.IsValidEdgeId(312), Is.False);
        }

        [Test]
        public void IsValidCoord_StateUnderTest_ExpectedBehavior()
        {
            // Valid coordinates
            var validCoord = new EdgeCoord { orient = Enum.EdgeOrient.Horizontal, x = 0, y = 0 };
            Assert.That(EdgeHelper.IsValidCoord(validCoord), Is.True);

            validCoord = new EdgeCoord { orient = Enum.EdgeOrient.Vertical, x = 12, y = 12 };
            Assert.That(EdgeHelper.IsValidCoord(validCoord), Is.True);

            // Invalid coordinates
            var invalidCoord1 = new EdgeCoord { orient = Enum.EdgeOrient.Horizontal, x = -1, y = 0 };
            Assert.That(EdgeHelper.IsValidCoord(invalidCoord1), Is.False);

            var invalidCoord2 = new EdgeCoord { orient = Enum.EdgeOrient.Vertical, x = 0, y = 13 };
            Assert.That(EdgeHelper.IsValidCoord(invalidCoord2), Is.False);

            Assert.That(EdgeHelper.IsValidCoord(null), Is.False);
        }
    }
}
