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
            List<RevueEnAlerte> alertes = Access.GetInstance().GetRevuesAbonnementExpirant();
            if (alertes != null && alertes.Count > 0)
            {
                new FrmAlerteAbonnement(alertes).ShowDialog();
            }
            Application.Run(new FrmMediatek());
        }
    }
}
