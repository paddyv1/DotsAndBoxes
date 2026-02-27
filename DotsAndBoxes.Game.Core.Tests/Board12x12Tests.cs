using DotsAndBoxes.Game.Core;
using System;
using Xunit;

namespace DotsAndBoxes.Game.Core.Tests
{
    public class Board12x12Tests
    {
        [Fact]
        public void CheckHeightAndWidthValuesAreExpected()
        {
            Assert.Equal(12, Board12x12.H);
            Assert.Equal(12, Board12x12.W);
        }

        [Fact]
        public void Check_HEdge_VEdge_EdgeCount_BoxCount_ValuesAreExpected()
        {
            Assert.Equal(156, Board12x12.HEdgeCount);
            Assert.Equal(156, Board12x12.VEdgeCount);
            Assert.Equal(144, Board12x12.BoxCount);
            Assert.Equal(312, Board12x12.EdgeCount);
        }
    }
}
