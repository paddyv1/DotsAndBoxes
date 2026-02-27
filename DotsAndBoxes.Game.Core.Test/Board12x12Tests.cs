using Allure;
using Allure.NUnit;

namespace DotsAndBoxes.Game.Core.Test
{
    [AllureNUnit]
    public class Board12x12Tests
    {
       

        [Test]
        public void CheckHeightAndWidthValuesAreExpected()
        {
            Assert.That(Board12x12.H, Is.EqualTo(12));
            Assert.That(Board12x12.W, Is.EqualTo(12));

        }

        [Test]
        public void Check_HEdge_VEdge_EdgeCount_BoxCount_ValuesAreExpected()
        {
            Assert.That(Board12x12.HEdgeCount, Is.EqualTo(156));
            Assert.That(Board12x12.VEdgeCount, Is.EqualTo(156));
            Assert.That(Board12x12.BoxCount, Is.EqualTo(144));
            Assert.That(Board12x12.EdgeCount, Is.EqualTo(312));
        }
    }
}
