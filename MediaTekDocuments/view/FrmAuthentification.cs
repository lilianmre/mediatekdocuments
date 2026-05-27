using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Fenêtre d'authentification : premier écran affiché au démarrage
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        /// <summary>
        /// Utilisateur authentifié, accessible après DialogResult.OK
        /// </summary>
        public Utilisateur UtilisateurConnecte { get; private set; }

        private readonly FrmAuthentificationController controller;

        public FrmAuthentification()
        {
            InitializeComponent();
            controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Tente d'authentifier l'utilisateur avec les identifiants saisis
        /// </summary>
        private void BtnConnexion_Click(object sender, System.EventArgs e)
        {
            string login = txbLogin.Text.Trim();
            string pwd = txbPwd.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Veuillez saisir un login et un mot de passe.", "Champs obligatoires",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Utilisateur utilisateur = controller.GetUtilisateur(login, pwd);

            if (utilisateur == null)
            {
                MessageBox.Show("Login ou mot de passe incorrect.", "Échec de connexion",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbPwd.Text = "";
                txbPwd.Focus();
                return;
            }

            // Service Culture : aucun droit d'accès
            if (utilisateur.IdService == "00003")
            {
                MessageBox.Show("Vos droits ne sont pas suffisants pour accéder à cette application.",
                    "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            UtilisateurConnecte = utilisateur;
            this.DialogResult = DialogResult.OK;
        }
    }
}
