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
            Assert.That(coordExpected, Is.EqualTo(coordActual));
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
            Assert.That(coordExpected, Is.EqualTo(coordActual));
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
            Assert.That(coordExpected, Is.EqualTo(coordActual));
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
            Assert.That(coordExpected, Is.EqualTo(coordActual));
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
            Assert.That(coordExpected, Is.EqualTo(coordActual));
        }

        [Test]
        public void TryDecodeEdgeId_When168_ReturnCoord()
        {
            // Arrange
            int edgeId = 168;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 12,
                y = 0
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordExpected, Is.EqualTo(coordActual));
        }

        [Test]
        public void TryDecodeEdgeId_When311_ReturnCoord()
        {
            // Arrange
            int edgeId = 311;

            EdgeCoord coordExpected = new EdgeCoord
            {
                orient = Enum.EdgeOrient.Vertical,
                x = 12,
                y = 11
            };


            // Act
            EdgeHelper.TryDecode(edgeId, out EdgeCoord coordActual);

            // Assert
            Assert.That(coordExpected, Is.EqualTo(coordActual));
        }

        [Test]
        public void TryEncode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            

            // Act
            

            // Assert
            Assert.Fail();
        }

        [Test]
        public void IsValidEdgeId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            

            // Act
            
            // Assert
            Assert.Fail();
        }

        [Test]
        public void IsValidCoord_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            

            // Act
            

            // Assert
            Assert.Fail();
        }
    }
}
