using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class DvdTests
    {
        private Dvd CreerDvd()
        {
            return new Dvd("00001", "Inception", "inception.jpg", 148, "Christopher Nolan",
                "Un voleur spécialisé dans l'extraction.", "00001", "Science-Fiction",
                "00001", "Adulte", "00001", "Cinéma");
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerDvd().Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseTitre()
        {
            Assert.AreEqual("Inception", CreerDvd().Titre);
        }

        [TestMethod]
        public void Constructeur_InitialiseDuree()
        {
            Assert.AreEqual(148, CreerDvd().Duree);
        }

        [TestMethod]
        public void Constructeur_InitialiseRealisateur()
        {
            Assert.AreEqual("Christopher Nolan", CreerDvd().Realisateur);
        }

        [TestMethod]
        public void Constructeur_InitialiseSynopsis()
        {
            Assert.AreEqual("Un voleur spécialisé dans l'extraction.", CreerDvd().Synopsis);
        }

        [TestMethod]
        public void Constructeur_InitialiseGenre()
        {
            Assert.AreEqual("Science-Fiction", CreerDvd().Genre);
        }

        [TestMethod]
        public void Constructeur_InitialisePublic()
        {
            Assert.AreEqual("Adulte", CreerDvd().Public);
        }

        [TestMethod]
        public void Constructeur_InitialiseRayon()
        {
            Assert.AreEqual("Cinéma", CreerDvd().Rayon);
        }
    }
}
