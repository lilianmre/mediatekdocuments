using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier RevueEnAlerte : revue dont l'abonnement expire bientôt
    /// </summary>
    public class RevueEnAlerte
    {
        /// <summary>Titre de la revue</summary>
        public string Titre { get; set; }
        /// <summary>Date de fin de l'abonnement</summary>
        public DateTime DateFinAbonnement { get; set; }

        /// <summary>
        /// Crée une nouvelle revue en alerte
        /// </summary>
        /// <param name="titre">titre de la revue</param>
        /// <param name="dateFinAbonnement">date de fin d'abonnement</param>
        public RevueEnAlerte(string titre, DateTime dateFinAbonnement)
        {
            Titre = titre;
            DateFinAbonnement = dateFinAbonnement;
        }
    }
}
