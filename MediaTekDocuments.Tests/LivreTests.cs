using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class LivreTests
    {
        private Livre CreerLivre()
        {
            return new Livre("00001", "Les Misérables", "miserables.jpg", "978-2070409228",
                "Victor Hugo", "Classiques", "00001", "Roman", "00001", "Adulte", "00001", "Littérature");
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerLivre().Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseTitre()
        {
            Assert.AreEqual("Les Misérables", CreerLivre().Titre);
        }

        [TestMethod]
        public void Constructeur_InitialiseIsbn()
        {
            Assert.AreEqual("978-2070409228", CreerLivre().Isbn);
        }

        [TestMethod]
        public void Constructeur_InitialiseAuteur()
        {
            Assert.AreEqual("Victor Hugo", CreerLivre().Auteur);
        }

        [TestMethod]
        public void Constructeur_InitialiseCollection()
        {
            Assert.AreEqual("Classiques", CreerLivre().Collection);
        }

        [TestMethod]
        public void Constructeur_InitialiseGenre()
        {
            Assert.AreEqual("Roman", CreerLivre().Genre);
        }

        [TestMethod]
        public void Constructeur_InitialisePublic()
        {
            Assert.AreEqual("Adulte", CreerLivre().Public);
        }

        [TestMethod]
        public void Constructeur_InitialiseRayon()
        {
            Assert.AreEqual("Littérature", CreerLivre().Rayon);
        }
    }
}
