using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier RevueEnAlerte : revue dont l'abonnement expire bientôt
    /// </summary>
    public class RevueEnAlerte
    {
        public string Titre { get; set; }
        public DateTime DateFinAbonnement { get; set; }

        public RevueEnAlerte(string titre, DateTime dateFinAbonnement)
        {
            Titre = titre;
            DateFinAbonnement = dateFinAbonnement;
        }
    }
}
