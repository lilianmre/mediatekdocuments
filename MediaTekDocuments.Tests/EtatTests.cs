using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class EtatTests
    {
        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Etat etat = new Etat("00001", "Neuf");
            Assert.AreEqual("00001", etat.Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelle()
        {
            Etat etat = new Etat("00001", "Neuf");
            Assert.AreEqual("Neuf", etat.Libelle);
        }
    }
}
