using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class CategorieTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Categorie categorie = new Genre("00001", "Roman");
            Assert.AreEqual("00001", categorie.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Categorie categorie = new Genre("00001", "Roman");
            Assert.AreEqual("Roman", categorie.Libelle);
        }

        [TestMethod]
        public void ToString_RetourneLibelle()
        {
            Categorie categorie = new Genre("00001", "Roman");
            Assert.AreEqual("Roman", categorie.ToString());
        }
    }

    [TestClass]
    public class GenreTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Genre genre = new Genre("00001", "Roman");
            Assert.AreEqual("00001", genre.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Genre genre = new Genre("00001", "Roman");
            Assert.AreEqual("Roman", genre.Libelle);
        }
    }

    [TestClass]
    public class PublicTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Public lePublic = new Public("00001", "Adulte");
            Assert.AreEqual("00001", lePublic.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Public lePublic = new Public("00001", "Adulte");
            Assert.AreEqual("Adulte", lePublic.Libelle);
        }
    }

    [TestClass]
    public class RayonTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Rayon rayon = new Rayon("00001", "Littérature");
            Assert.AreEqual("00001", rayon.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Rayon rayon = new Rayon("00001", "Littérature");
            Assert.AreEqual("Littérature", rayon.Libelle);
        }
    }

    [TestClass]
    public class SuiviTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Suivi suivi = new Suivi("00001", "En cours");
            Assert.AreEqual("00001", suivi.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Suivi suivi = new Suivi("00001", "En cours");
            Assert.AreEqual("En cours", suivi.Libelle);
        }
    }
}
