using Knight.Common.Controls.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;

namespace Knight.Common.Controls.Wpf
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class DateIntegerChartLineTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void ConstructorTest()
        {
            foreach (DateIntegerChartLine line in DataProvider())
            {
                Assert.AreEqual(Colors.AliceBlue, line.Color);
                Assert.AreEqual(1, line.Thickness);
                Assert.AreEqual(string.Empty, line.Description);
            }
        }

        List<DateIntegerChartLine> DataProvider()
        {
            List<DateIntegerChartLine> lines = new List<DateIntegerChartLine>();

            lines.Add(new DateIntegerChartLine(Colors.AliceBlue, -1, null));
            lines.Add(new DateIntegerChartLine(Colors.AliceBlue, 0, null));
            lines.Add(new DateIntegerChartLine(Colors.AliceBlue, 1, string.Empty));
            lines.Add(new DateIntegerChartLine(Colors.AliceBlue, 1, ""));
            lines.Add(new DateIntegerChartLine(Colors.AliceBlue, 1, " "));

            return lines;
        }
    }
}
