using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class RevueEnAlerteTests
    {
        private readonly DateTime dateFin = new DateTime(2024, 12, 31);

        private RevueEnAlerte CreerRevueEnAlerte()
        {
            return new RevueEnAlerte("Le Monde", dateFin);
        }

        [TestMethod]
        public void Constructeur_InitialiseTitre()
        {
            Assert.AreEqual("Le Monde", CreerRevueEnAlerte().Titre);
        }

        [TestMethod]
        public void Constructeur_InitialiseDateFinAbonnement()
        {
            Assert.AreEqual(dateFin, CreerRevueEnAlerte().DateFinAbonnement);
        }
    }
}
