using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class CommandeAbonnementTests
    {
        private readonly DateTime dateCommande = new DateTime(2024, 1, 1);
        private readonly DateTime dateFin = new DateTime(2024, 12, 31);

        private CommandeAbonnement CreerAbonnement()
        {
            return new CommandeAbonnement("00001", dateCommande, 49.99, dateFin, "00010");
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerAbonnement().Id);
        }

        [TestMethod]
        public void Constructeur_InitialiseDateCommande()
        {
            Assert.AreEqual(dateCommande, CreerAbonnement().DateCommande);
        }

        [TestMethod]
        public void Constructeur_InitialiseMontant()
        {
            Assert.AreEqual(49.99, CreerAbonnement().Montant);
        }

        [TestMethod]
        public void Constructeur_InitialiseDateFinAbonnement()
        {
            Assert.AreEqual(dateFin, CreerAbonnement().DateFinAbonnement);
        }

        [TestMethod]
        public void Constructeur_InitialiseIdRevue()
        {
            Assert.AreEqual("00010", CreerAbonnement().IdRevue);
        }
    }
}
