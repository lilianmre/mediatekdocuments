using MediaTekDocuments.view;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MediaTekDocuments
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Fenêtre d'authentification
            FrmAuthentification frmAuth = new FrmAuthentification();
            if (frmAuth.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Utilisateur utilisateur = frmAuth.UtilisateurConnecte;

            // Alerte abonnements expirants uniquement pour le service Diffusion (commandes)
            if (utilisateur.IdService == "00001")
            {
                List<RevueEnAlerte> alertes = Access.GetInstance().GetRevuesAbonnementExpirant();
                if (alertes != null && alertes.Count > 0)
                {
                    new FrmAlerteAbonnement(alertes).ShowDialog();
                }
            }

            Application.Run(new FrmMediatek(utilisateur));
        }
    }
}
