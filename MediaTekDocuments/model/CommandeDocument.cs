using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument : commande d'un livre ou d'un dvd
    /// Combine les données de commande, commandedocument et suivi
    /// </summary>
    public class CommandeDocument
    {
        /// <summary>Identifiant de la commande</summary>
        public string Id { get; set; }
        /// <summary>Date de la commande</summary>
        public DateTime DateCommande { get; set; }
        /// <summary>Montant de la commande</summary>
        public double Montant { get; set; }
        /// <summary>Nombre d'exemplaires commandés</summary>
        public int NbExemplaire { get; set; }
        /// <summary>Identifiant du livre ou dvd commandé</summary>
        public string IdLivreDvd { get; set; }
        /// <summary>Identifiant de l'étape de suivi</summary>
        public string IdSuivi { get; set; }
        /// <summary>Libellé de l'étape de suivi</summary>
        public string LibelleSuivi { get; set; }

        /// <summary>
        /// Crée une nouvelle commande de document
        /// </summary>
        /// <param name="id">identifiant de la commande</param>
        /// <param name="dateCommande">date de la commande</param>
        /// <param name="montant">montant</param>
        /// <param name="nbExemplaire">nombre d'exemplaires</param>
        /// <param name="idLivreDvd">identifiant du livre ou dvd</param>
        /// <param name="idSuivi">identifiant de l'étape de suivi</param>
        /// <param name="libelleSuivi">libellé de l'étape de suivi</param>
        public CommandeDocument(string id, DateTime dateCommande, double montant,
            int nbExemplaire, string idLivreDvd, string idSuivi, string libelleSuivi)
        {
            Id = id;
            DateCommande = dateCommande;
            Montant = montant;
            NbExemplaire = nbExemplaire;
            IdLivreDvd = idLivreDvd;
            IdSuivi = idSuivi;
            LibelleSuivi = libelleSuivi;
        }
    }
}
