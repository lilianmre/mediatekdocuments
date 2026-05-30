using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeAbonnement : commande d'abonnement à une revue
    /// </summary>
    public class CommandeAbonnement
    {
        /// <summary>Identifiant de la commande</summary>
        public string Id { get; set; }
        /// <summary>Date de la commande</summary>
        public DateTime DateCommande { get; set; }
        /// <summary>Montant de l'abonnement</summary>
        public double Montant { get; set; }
        /// <summary>Date de fin de l'abonnement</summary>
        public DateTime DateFinAbonnement { get; set; }
        /// <summary>Identifiant de la revue</summary>
        public string IdRevue { get; set; }

        /// <summary>
        /// Crée une nouvelle commande d'abonnement
        /// </summary>
        /// <param name="id">identifiant de la commande</param>
        /// <param name="dateCommande">date de la commande</param>
        /// <param name="montant">montant</param>
        /// <param name="dateFinAbonnement">date de fin d'abonnement</param>
        /// <param name="idRevue">identifiant de la revue</param>
        public CommandeAbonnement(string id, DateTime dateCommande, double montant,
            DateTime dateFinAbonnement, string idRevue)
        {
            Id = id;
            DateCommande = dateCommande;
            Montant = montant;
            DateFinAbonnement = dateFinAbonnement;
            IdRevue = idRevue;
        }
    }
}
