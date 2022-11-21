using System;
using TestProjectPoint.Models;
using Xunit;

namespace TestProjectPoint
{
    //It's tests for a Point for Prototype Pattern
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var line1 = new Line
            {
                Start = new Point { X = 3, Y = 3 },
                End = new Point { X = 10, Y = 10 }
            };

            var line2 = line1.DeepCopy();
            line1.Start.X = line1.End.X = line1.Start.Y = line1.End.Y = 0;

            Assert.Equal(3, line2.Start.X);
            Assert.Equal(3, line2.Start.Y);
            Assert.Equal(10, line2.End.X);
            Assert.Equal(10, line2.End.Y);
        }
    }
}
