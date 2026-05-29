using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class FrmMediatekControllerTests
    {
        [TestMethod]
        public void ParutionDansAbonnement_DateDansLaPeriode_RetourneVrai()
        {
            DateTime dateCommande = new DateTime(2024, 1, 1);
            DateTime dateFin = new DateTime(2024, 12, 31);
            DateTime dateParution = new DateTime(2024, 6, 15);
            Assert.IsTrue(FrmMediatekController.ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void ParutionDansAbonnement_DateAvantPeriode_RetourneFaux()
        {
            DateTime dateCommande = new DateTime(2024, 1, 1);
            DateTime dateFin = new DateTime(2024, 12, 31);
            DateTime dateParution = new DateTime(2023, 12, 31);
            Assert.IsFalse(FrmMediatekController.ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void ParutionDansAbonnement_DateApresPeriode_RetourneFaux()
        {
            DateTime dateCommande = new DateTime(2024, 1, 1);
            DateTime dateFin = new DateTime(2024, 12, 31);
            DateTime dateParution = new DateTime(2025, 1, 1);
            Assert.IsFalse(FrmMediatekController.ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void ParutionDansAbonnement_DateEgaleDebut_RetourneVrai()
        {
            DateTime dateCommande = new DateTime(2024, 1, 1);
            DateTime dateFin = new DateTime(2024, 12, 31);
            DateTime dateParution = new DateTime(2024, 1, 1);
            Assert.IsTrue(FrmMediatekController.ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void ParutionDansAbonnement_DateEgaleFin_RetourneVrai()
        {
            DateTime dateCommande = new DateTime(2024, 1, 1);
            DateTime dateFin = new DateTime(2024, 12, 31);
            DateTime dateParution = new DateTime(2024, 12, 31);
            Assert.IsTrue(FrmMediatekController.ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }
    }
}
