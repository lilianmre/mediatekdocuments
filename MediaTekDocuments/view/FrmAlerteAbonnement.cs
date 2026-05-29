using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    public partial class FrmAlerteAbonnement : Form
    {
        public FrmAlerteAbonnement(List<RevueEnAlerte> revues)
        {
            InitializeComponent();
            dgvAlertes.DataSource = revues;
            if (dgvAlertes.Columns.Count > 0)
            {
                if (dgvAlertes.Columns.Contains("Titre"))
                    dgvAlertes.Columns["Titre"].HeaderText = "Titre de la revue";
                if (dgvAlertes.Columns.Contains("DateFinAbonnement"))
                    dgvAlertes.Columns["DateFinAbonnement"].HeaderText = "Fin d'abonnement";
                dgvAlertes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
