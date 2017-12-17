using System;
using Chimera.DataAcess;
using Chimera.Entities;
using NUnit.Framework;

namespace Chimera.DataAccess.Test
{
    [TestFixture]
    public class BirthPlaceAdapterTest
    {
        [Test]
        public void CreateTest()
        {
            const string connectionString = "";

            var birthPlace = new BirthPlace
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Name = "Test Place",
                Region = null,
            };

            var bpAdapter = new BirthPlaceAdapter(connectionString);

            bool insert = bpAdapter.Insert(birthPlace);
            Assert.IsTrue(insert);
        }
    }
}
