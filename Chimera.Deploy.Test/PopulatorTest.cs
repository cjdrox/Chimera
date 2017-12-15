using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Chimera.Deploy.Test
{
    [TestFixture]
    public class PopulatorTest
    {
        [Test]
        public void PopulatorReturnNonZeroClasses()
        {
            Populator populator = new Populator();

            List<Type> types = populator.GetTypes();

            Assert.IsNotEmpty(types);
        }
    }
}
