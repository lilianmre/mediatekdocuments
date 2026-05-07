using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeAbonnement : commande d'abonnement à une revue
    /// </summary>
    public class CommandeAbonnement
    {
        public string Id { get; set; }
        public DateTime DateCommande { get; set; }
        public double Montant { get; set; }
        public DateTime DateFinAbonnement { get; set; }
        public string IdRevue { get; set; }

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
