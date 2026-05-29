using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class CommandeDocumentTests
    {
        private readonly DateTime dateCommande = new DateTime(2024, 1, 10);

        private CommandeDocument CreerCommande()
        {
            return new CommandeDocument("00001", dateCommande, 29.99, 3, "00010", "00001", "En cours");
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerCommande().Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseDateCommande()
        {
            Assert.AreEqual(dateCommande, CreerCommande().DateCommande);
        }

        [TestMethod]
        public void Constructeur_InitialiseMontant()
        {
            Assert.AreEqual(29.99, CreerCommande().Montant);
        }

        [TestMethod]
        public void Constructeur_InitialiseNbExemplaire()
        {
            Assert.AreEqual(3, CreerCommande().NbExemplaire);
        }

        [TestMethod]
        public void Constructeur_InitialiseIdLivreDvd()
        {
            Assert.AreEqual("00010", CreerCommande().IdLivreDvd);
        }

        [TestMethod]
        public void Constructeur_InitialiseIdSuivi()
        {
            Assert.AreEqual("00001", CreerCommande().IdSuivi);
        }

        [TestMethod]
        public void Constructeur_InitialiseLibelleSuivi()
        {
            Assert.AreEqual("En cours", CreerCommande().LibelleSuivi);
        }
    }
}
