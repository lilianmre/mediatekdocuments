using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class ExemplaireTests
    {
        private readonly DateTime dateAchat = new DateTime(2024, 3, 15);

        private Exemplaire CreerExemplaire()
        {
            return new Exemplaire(1, dateAchat, "photo.jpg", "00001", "00001");
        }

        [TestMethod]
        public void Constructeur_InitialiseNumero()
        {
            Assert.AreEqual(1, CreerExemplaire().Numero);
        }

        [TestMethod]
        public void Constructeur_InitialiseDateAchat()
        {
            Assert.AreEqual(dateAchat, CreerExemplaire().DateAchat);
        }

        [TestMethod]
        public void Constructeur_InitialisePhoto()
        {
            Assert.AreEqual("photo.jpg", CreerExemplaire().Photo);
        }

        [TestMethod]
        public void Constructeur_InitialiseIdEtat()
        {
            Assert.AreEqual("00001", CreerExemplaire().IdEtat);
        }

        [TestMethod]
        public void Constructeur_InitialiseId()
        {
            Assert.AreEqual("00001", CreerExemplaire().Id);
        }
    }
}
