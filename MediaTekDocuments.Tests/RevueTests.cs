using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class RevueTests
    {
        private Revue CreerRevue()
        {
            return new Revue("00001", "Le Monde", "lemonde.jpg", "00001", "Actualité",
                "00001", "Adulte", "00001", "Presse", "Quotidienne", 1);
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerRevue().Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseTitre()
        {
            Assert.AreEqual("Le Monde", CreerRevue().Titre);
        }

        [TestMethod]
        public void Constructeur_InitialisePeriodicite()
        {
            Assert.AreEqual("Quotidienne", CreerRevue().Periodicite);
        }

        [TestMethod]
        public void Constructeur_InitialiseDelaiMiseADispo()
        {
            Assert.AreEqual(1, CreerRevue().DelaiMiseADispo);
        }

        [TestMethod]
        public void Constructeur_InitialiseGenre()
        {
            Assert.AreEqual("Actualité", CreerRevue().Genre);
        }

        [TestMethod]
        public void Constructeur_InitialisePublic()
        {
            Assert.AreEqual("Adulte", CreerRevue().Public);
        }

        [TestMethod]
        public void Constructeur_InitialiseRayon()
        {
            Assert.AreEqual("Presse", CreerRevue().Rayon);
        }
    }
}
