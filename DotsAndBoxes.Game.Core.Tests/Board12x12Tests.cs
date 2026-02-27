using DotsAndBoxes.Game.Core;
using System;
using Xunit;

namespace DotsAndBoxes.Game.Core.Tests
{
    public class Board12x12Tests
    {
        [Fact]
        public void CheckConstantValuesAreExpected()
        {
            // Arrange
          
            Assert.Equal(12, Board12x12.H);
            Assert.Equal(12, Board12x12.W);


        }
    }
}
