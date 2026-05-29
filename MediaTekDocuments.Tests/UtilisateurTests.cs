using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class UtilisateurTests
    {
        private Utilisateur CreerUtilisateur()
        {
            return new Utilisateur("admin", "Admin", "Super", "00001", "Diffusion");
        }

        [TestMethod]
        public void Constructeur_InitialiseLogin()
        {
            Assert.AreEqual("admin", CreerUtilisateur().Login);
        }

        [TestMethod]
        public void Constructeur_InitialiseNom()
        {
            Assert.AreEqual("Admin", CreerUtilisateur().Nom);
        }

        [TestMethod]
        public void Constructeur_InitialisePrenom()
        {
            Assert.AreEqual("Super", CreerUtilisateur().Prenom);
        }

        [TestMethod]
        public void Constructeur_InitialiseIdService()
        {
            Assert.AreEqual("00001", CreerUtilisateur().IdService);
        }

        [TestMethod]
        public void Constructeur_InitialiseService()
        {
            Assert.AreEqual("Diffusion", CreerUtilisateur().Service);
        }
    }
}
