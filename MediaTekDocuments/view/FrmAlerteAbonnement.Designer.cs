
namespace MediaTekDocuments.view
{
    partial class FrmAlerteAbonnement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvAlertes = new System.Windows.Forms.DataGridView();
            this.btnFermer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertes)).BeginInit();
            this.SuspendLayout();
            //
            // lblMessage
            //
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(420, 15);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Les abonnements suivants arrivent à expiration dans moins de 30 jours :";
            //
            // dgvAlertes
            //
            this.dgvAlertes.AllowUserToAddRows = false;
            this.dgvAlertes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlertes.Location = new System.Drawing.Point(12, 35);
            this.dgvAlertes.MultiSelect = false;
            this.dgvAlertes.Name = "dgvAlertes";
            this.dgvAlertes.ReadOnly = true;
            this.dgvAlertes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlertes.Size = new System.Drawing.Size(460, 200);
            this.dgvAlertes.TabIndex = 1;
            //
            // btnFermer
            //
            this.btnFermer.Location = new System.Drawing.Point(190, 248);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(100, 30);
            this.btnFermer.TabIndex = 2;
            this.btnFermer.Text = "Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            //
            // FrmAlerteAbonnement
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 291);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dgvAlertes);
            this.Controls.Add(this.btnFermer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlerteAbonnement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alerte - Abonnements expirant bientôt";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridView dgvAlertes;
        private System.Windows.Forms.Button btnFermer;
    }
}
