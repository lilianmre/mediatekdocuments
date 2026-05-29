using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// Applique les restrictions d'accès selon le service de l'utilisateur connecté
        /// </summary>
        /// <param name="utilisateur">utilisateur authentifié</param>
        internal FrmMediatek(Utilisateur utilisateur)
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            AppliquerDroitsAcces(utilisateur);
        }

        /// <summary>
        /// Rend invisibles les onglets non accessibles selon le service de l'utilisateur
        /// Service Diffusion (00001) : accès complet
        /// Service Prêt (00002) : documents uniquement, onglets commandes masqués
        /// </summary>
        /// <param name="utilisateur">utilisateur authentifié</param>
        private void AppliquerDroitsAcces(Utilisateur utilisateur)
        {
            if (utilisateur.IdService == "00002")
            {
                tabOngletsApplication.TabPages.Remove(tabCommandeLivres);
                tabOngletsApplication.TabPages.Remove(tabCommandeDvd);
                tabOngletsApplication.TabPages.Remove(tabCommandeRevues);
            }
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }
        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Paarutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
        #endregion

        #region Onglet Commandes Livres
        private readonly BindingSource bdgCommandesLivres = new BindingSource();
        private readonly BindingSource bdgSuivisLivres = new BindingSource();
        private List<Livre> lesLivresPourCommande = new List<Livre>();
        private List<CommandeDocument> lesCommandesLivreAffichees = new List<CommandeDocument>();
        private Livre livreCourantCommande = null;

        private void RemplirComboSuivi(List<Categorie> lesSuivis, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesSuivis;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
                cbx.SelectedIndex = -1;
        }

        private void TabCommandeLivres_Enter(object sender, EventArgs e)
        {
            lesLivresPourCommande = controller.GetAllLivres();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivisLivres, cbxCommandeLivreSuivi);
            VideCommandeLivreInfos();
            HashSet<string> idsLivres = new HashSet<string>(lesLivresPourCommande.ConvertAll(l => l.Id));
            lesCommandesLivreAffichees = controller.GetAllCommandesDocument()
                .FindAll(c => idsLivres.Contains(c.IdLivreDvd));
            RemplirCommandesLivresListe(lesCommandesLivreAffichees);
        }

        private void BtnCommandeLivreRechercher_Click(object sender, EventArgs e)
        {
            string numRecherche = txbCommandeLivreNumRecherche.Text.Trim();
            if (string.IsNullOrEmpty(numRecherche))
            {
                MessageBox.Show("Veuillez saisir un numéro de livre.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Livre livre = lesLivresPourCommande.Find(x => x.Id.Equals(numRecherche));
            if (livre == null)
            {
                MessageBox.Show("Numéro de livre introuvable.", "Livre non trouvé",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VideCommandeLivreInfos();
                return;
            }

            livreCourantCommande = livre;
            AfficheCommandeLivreInfos(livre);
            ChargerCommandesLivre(livre.Id);
        }

        private void AfficheCommandeLivreInfos(Livre livre)
        {
            txbCommandeLivreTitre.Text = livre.Titre;
            txbCommandeLivreAuteur.Text = livre.Auteur;
            txbCommandeLivreIsbn.Text = livre.Isbn;
            txbCommandeLivreCollection.Text = livre.Collection;
            txbCommandeLivreGenre.Text = livre.Genre;
            txbCommandeLivrePublic.Text = livre.Public;
            txbCommandeLivreRayon.Text = livre.Rayon;
            txbCommandeLivreImage.Text = livre.Image;
            try { pcbCommandeLivreImage.Image = Image.FromFile(livre.Image); }
            catch (Exception) { pcbCommandeLivreImage.Image = null; }
        }

        private void VideCommandeLivreInfos()
        {
            livreCourantCommande = null;
            txbCommandeLivreTitre.Text = "";
            txbCommandeLivreAuteur.Text = "";
            txbCommandeLivreIsbn.Text = "";
            txbCommandeLivreCollection.Text = "";
            txbCommandeLivreGenre.Text = "";
            txbCommandeLivrePublic.Text = "";
            txbCommandeLivreRayon.Text = "";
            txbCommandeLivreImage.Text = "";
            pcbCommandeLivreImage.Image = null;
            lesCommandesLivreAffichees.Clear();
            dgvCommandesLivres.DataSource = null;
        }

        private void ChargerCommandesLivre(string idLivre)
        {
            lesCommandesLivreAffichees = controller.GetCommandesDocument(idLivre);
            RemplirCommandesLivresListe(lesCommandesLivreAffichees);
        }

        private void RechargerCommandesLivres()
        {
            if (livreCourantCommande != null)
                ChargerCommandesLivre(livreCourantCommande.Id);
            else
            {
                HashSet<string> ids = new HashSet<string>(lesLivresPourCommande.ConvertAll(l => l.Id));
                lesCommandesLivreAffichees = controller.GetAllCommandesDocument().FindAll(c => ids.Contains(c.IdLivreDvd));
                RemplirCommandesLivresListe(lesCommandesLivreAffichees);
            }
        }

        private void RemplirCommandesLivresListe(List<CommandeDocument> commandes)
        {
            bdgCommandesLivres.DataSource = commandes;
            dgvCommandesLivres.DataSource = bdgCommandesLivres;
            if (dgvCommandesLivres.Columns.Count > 0)
            {
                if (dgvCommandesLivres.Columns.Contains("IdLivreDvd"))
                    dgvCommandesLivres.Columns["IdLivreDvd"].Visible = false;
                if (dgvCommandesLivres.Columns.Contains("IdSuivi"))
                    dgvCommandesLivres.Columns["IdSuivi"].Visible = false;
                if (dgvCommandesLivres.Columns.Contains("Id"))
                    dgvCommandesLivres.Columns["Id"].HeaderText = "N° commande";
                if (dgvCommandesLivres.Columns.Contains("DateCommande"))
                    dgvCommandesLivres.Columns["DateCommande"].HeaderText = "Date";
                if (dgvCommandesLivres.Columns.Contains("Montant"))
                    dgvCommandesLivres.Columns["Montant"].HeaderText = "Montant (€)";
                if (dgvCommandesLivres.Columns.Contains("NbExemplaire"))
                    dgvCommandesLivres.Columns["NbExemplaire"].HeaderText = "Nb exemplaires";
                if (dgvCommandesLivres.Columns.Contains("LibelleSuivi"))
                    dgvCommandesLivres.Columns["LibelleSuivi"].HeaderText = "Suivi";
                dgvCommandesLivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void DgvCommandesLivres_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandesLivres.CurrentCell != null && bdgCommandesLivres.Count > 0)
            {
                try
                {
                    CommandeDocument cmd = (CommandeDocument)bdgCommandesLivres.List[bdgCommandesLivres.Position];
                    for (int i = 0; i < cbxCommandeLivreSuivi.Items.Count; i++)
                    {
                        if (((Categorie)cbxCommandeLivreSuivi.Items[i]).Id == cmd.IdSuivi)
                        {
                            cbxCommandeLivreSuivi.SelectedIndex = i;
                            break;
                        }
                    }
                }
                catch (Exception) { cbxCommandeLivreSuivi.SelectedIndex = -1; }
            }
        }

        private void DgvCommandesLivres_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (lesCommandesLivreAffichees == null || lesCommandesLivreAffichees.Count == 0) return;
            string titre = dgvCommandesLivres.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sorted;
            switch (titre)
            {
                case "N° commande":
                    sorted = lesCommandesLivreAffichees.OrderBy(o => o.Id).ToList(); break;
                case "Date":
                    sorted = lesCommandesLivreAffichees.OrderByDescending(o => o.DateCommande).ToList(); break;
                case "Montant (€)":
                    sorted = lesCommandesLivreAffichees.OrderBy(o => o.Montant).ToList(); break;
                case "Nb exemplaires":
                    sorted = lesCommandesLivreAffichees.OrderBy(o => o.NbExemplaire).ToList(); break;
                case "Suivi":
                    sorted = lesCommandesLivreAffichees.OrderBy(o => o.LibelleSuivi).ToList(); break;
                default:
                    return;
            }
            RemplirCommandesLivresListe(sorted);
        }

        private void BtnCommandeLivreEnregistrer_Click(object sender, EventArgs e)
        {
            if (livreCourantCommande == null)
            {
                MessageBox.Show("Veuillez d'abord rechercher un livre.", "Aucun livre sélectionné",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nextId = controller.GetNextCommandeId();
            CommandeDocument nouvelleCommande = new CommandeDocument(
                nextId,
                dtpCommandeLivreDate.Value.Date,
                (double)nudCommandeLivreMontant.Value,
                (int)nudCommandeLivreNbEx.Value,
                livreCourantCommande.Id,
                "00001",
                "en cours"
            );

            bool succes = controller.CreerCommande(nouvelleCommande);
            if (succes)
            {
                MessageBox.Show("Commande enregistrée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerCommandesLivre(livreCourantCommande.Id);
                nudCommandeLivreMontant.Value = 0;
                nudCommandeLivreNbEx.Value = 1;
                dtpCommandeLivreDate.Value = DateTime.Today;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommandeLivreModifierSuivi_Click(object sender, EventArgs e)
        {
            if (dgvCommandesLivres.CurrentCell == null || bdgCommandesLivres.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande.", "Aucune commande sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbxCommandeLivreSuivi.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner une étape de suivi.", "Étape non sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommandeDocument cmd = (CommandeDocument)bdgCommandesLivres.List[bdgCommandesLivres.Position];
            Categorie nouveauSuivi = (Categorie)cbxCommandeLivreSuivi.SelectedItem;

            if ((cmd.IdSuivi == "00003" || cmd.IdSuivi == "00004") &&
                (nouveauSuivi.Id == "00001" || nouveauSuivi.Id == "00002"))
            {
                MessageBox.Show("Une commande livrée ou réglée ne peut pas revenir à une étape précédente.",
                    "Transition interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nouveauSuivi.Id == "00004" && cmd.IdSuivi != "00003")
            {
                MessageBox.Show("Une commande ne peut être réglée que si elle est livrée.",
                    "Transition interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool succes = controller.ModifierSuiviCommande(cmd.Id, nouveauSuivi.Id);
            if (succes)
            {
                MessageBox.Show("Suivi mis à jour avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RechargerCommandesLivres();
            }
            else
            {
                MessageBox.Show("Erreur lors de la mise à jour du suivi.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommandeLivreSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvCommandesLivres.CurrentCell == null || bdgCommandesLivres.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.", "Aucune commande sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommandeDocument cmd = (CommandeDocument)bdgCommandesLivres.List[bdgCommandesLivres.Position];

            if (cmd.IdSuivi == "00003" || cmd.IdSuivi == "00004")
            {
                MessageBox.Show("Impossible de supprimer une commande livrée ou réglée.",
                    "Suppression interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Supprimer la commande n°{cmd.Id} du {cmd.DateCommande:dd/MM/yyyy} ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool succes = controller.SupprimerCommande(cmd.Id);
                if (succes)
                {
                    MessageBox.Show("Commande supprimée avec succès.", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RechargerCommandesLivres();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de la commande.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Onglet Commandes DVD
        private readonly BindingSource bdgCommandesDvd = new BindingSource();
        private readonly BindingSource bdgSuivisDvd = new BindingSource();
        private List<Dvd> lesDvdPourCommande = new List<Dvd>();
        private List<CommandeDocument> lesCommandesDvdAffichees = new List<CommandeDocument>();
        private Dvd dvdCourantCommande = null;

        private void TabCommandeDvd_Enter(object sender, EventArgs e)
        {
            lesDvdPourCommande = controller.GetAllDvd();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivisDvd, cbxCommandeDvdSuivi);
            VideCommandeDvdInfos();
            HashSet<string> idsDvd = new HashSet<string>(lesDvdPourCommande.ConvertAll(d => d.Id));
            lesCommandesDvdAffichees = controller.GetAllCommandesDocument()
                .FindAll(c => idsDvd.Contains(c.IdLivreDvd));
            RemplirCommandesDvdListe(lesCommandesDvdAffichees);
        }

        private void BtnCommandeDvdRechercher_Click(object sender, EventArgs e)
        {
            string numRecherche = txbCommandeDvdNumRecherche.Text.Trim();
            if (string.IsNullOrEmpty(numRecherche))
            {
                MessageBox.Show("Veuillez saisir un numéro de DVD.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dvd dvd = lesDvdPourCommande.Find(x => x.Id.Equals(numRecherche));
            if (dvd == null)
            {
                MessageBox.Show("Numéro de DVD introuvable.", "DVD non trouvé",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VideCommandeDvdInfos();
                return;
            }

            dvdCourantCommande = dvd;
            AfficheCommandeDvdInfos(dvd);
            ChargerCommandesDvd(dvd.Id);
        }

        private void AfficheCommandeDvdInfos(Dvd dvd)
        {
            txbCommandeDvdTitre.Text = dvd.Titre;
            txbCommandeDvdRealisateur.Text = dvd.Realisateur;
            txbCommandeDvdDuree.Text = dvd.Duree.ToString();
            txbCommandeDvdSynopsis.Text = dvd.Synopsis;
            txbCommandeDvdGenre.Text = dvd.Genre;
            txbCommandeDvdPublic.Text = dvd.Public;
            txbCommandeDvdRayon.Text = dvd.Rayon;
            txbCommandeDvdImage.Text = dvd.Image;
            try { pcbCommandeDvdImage.Image = Image.FromFile(dvd.Image); }
            catch (Exception) { pcbCommandeDvdImage.Image = null; }
        }

        private void VideCommandeDvdInfos()
        {
            dvdCourantCommande = null;
            txbCommandeDvdTitre.Text = "";
            txbCommandeDvdRealisateur.Text = "";
            txbCommandeDvdDuree.Text = "";
            txbCommandeDvdSynopsis.Text = "";
            txbCommandeDvdGenre.Text = "";
            txbCommandeDvdPublic.Text = "";
            txbCommandeDvdRayon.Text = "";
            txbCommandeDvdImage.Text = "";
            pcbCommandeDvdImage.Image = null;
            lesCommandesDvdAffichees.Clear();
            dgvCommandesDvd.DataSource = null;
        }

        private void ChargerCommandesDvd(string idDvd)
        {
            lesCommandesDvdAffichees = controller.GetCommandesDocument(idDvd);
            RemplirCommandesDvdListe(lesCommandesDvdAffichees);
        }

        private void RechargerCommandesDvd()
        {
            if (dvdCourantCommande != null)
                ChargerCommandesDvd(dvdCourantCommande.Id);
            else
            {
                HashSet<string> ids = new HashSet<string>(lesDvdPourCommande.ConvertAll(d => d.Id));
                lesCommandesDvdAffichees = controller.GetAllCommandesDocument().FindAll(c => ids.Contains(c.IdLivreDvd));
                RemplirCommandesDvdListe(lesCommandesDvdAffichees);
            }
        }

        private void RemplirCommandesDvdListe(List<CommandeDocument> commandes)
        {
            bdgCommandesDvd.DataSource = commandes;
            dgvCommandesDvd.DataSource = bdgCommandesDvd;
            if (dgvCommandesDvd.Columns.Count > 0)
            {
                if (dgvCommandesDvd.Columns.Contains("IdLivreDvd"))
                    dgvCommandesDvd.Columns["IdLivreDvd"].Visible = false;
                if (dgvCommandesDvd.Columns.Contains("IdSuivi"))
                    dgvCommandesDvd.Columns["IdSuivi"].Visible = false;
                if (dgvCommandesDvd.Columns.Contains("Id"))
                    dgvCommandesDvd.Columns["Id"].HeaderText = "N° commande";
                if (dgvCommandesDvd.Columns.Contains("DateCommande"))
                    dgvCommandesDvd.Columns["DateCommande"].HeaderText = "Date";
                if (dgvCommandesDvd.Columns.Contains("Montant"))
                    dgvCommandesDvd.Columns["Montant"].HeaderText = "Montant (€)";
                if (dgvCommandesDvd.Columns.Contains("NbExemplaire"))
                    dgvCommandesDvd.Columns["NbExemplaire"].HeaderText = "Nb exemplaires";
                if (dgvCommandesDvd.Columns.Contains("LibelleSuivi"))
                    dgvCommandesDvd.Columns["LibelleSuivi"].HeaderText = "Suivi";
                dgvCommandesDvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void DgvCommandesDvd_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandesDvd.CurrentCell != null && bdgCommandesDvd.Count > 0)
            {
                try
                {
                    CommandeDocument cmd = (CommandeDocument)bdgCommandesDvd.List[bdgCommandesDvd.Position];
                    for (int i = 0; i < cbxCommandeDvdSuivi.Items.Count; i++)
                    {
                        if (((Categorie)cbxCommandeDvdSuivi.Items[i]).Id == cmd.IdSuivi)
                        {
                            cbxCommandeDvdSuivi.SelectedIndex = i;
                            break;
                        }
                    }
                }
                catch (Exception) { cbxCommandeDvdSuivi.SelectedIndex = -1; }
            }
        }

        private void DgvCommandesDvd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (lesCommandesDvdAffichees == null || lesCommandesDvdAffichees.Count == 0) return;
            string titre = dgvCommandesDvd.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sorted;
            switch (titre)
            {
                case "N° commande":
                    sorted = lesCommandesDvdAffichees.OrderBy(o => o.Id).ToList(); break;
                case "Date":
                    sorted = lesCommandesDvdAffichees.OrderByDescending(o => o.DateCommande).ToList(); break;
                case "Montant (€)":
                    sorted = lesCommandesDvdAffichees.OrderBy(o => o.Montant).ToList(); break;
                case "Nb exemplaires":
                    sorted = lesCommandesDvdAffichees.OrderBy(o => o.NbExemplaire).ToList(); break;
                case "Suivi":
                    sorted = lesCommandesDvdAffichees.OrderBy(o => o.LibelleSuivi).ToList(); break;
                default:
                    return;
            }
            RemplirCommandesDvdListe(sorted);
        }

        private void BtnCommandeDvdEnregistrer_Click(object sender, EventArgs e)
        {
            if (dvdCourantCommande == null)
            {
                MessageBox.Show("Veuillez d'abord rechercher un DVD.", "Aucun DVD sélectionné",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nextId = controller.GetNextCommandeId();
            CommandeDocument nouvelleCommande = new CommandeDocument(
                nextId,
                dtpCommandeDvdDate.Value.Date,
                (double)nudCommandeDvdMontant.Value,
                (int)nudCommandeDvdNbEx.Value,
                dvdCourantCommande.Id,
                "00001",
                "en cours"
            );

            bool succes = controller.CreerCommande(nouvelleCommande);
            if (succes)
            {
                MessageBox.Show("Commande enregistrée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerCommandesDvd(dvdCourantCommande.Id);
                nudCommandeDvdMontant.Value = 0;
                nudCommandeDvdNbEx.Value = 1;
                dtpCommandeDvdDate.Value = DateTime.Today;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommandeDvdModifierSuivi_Click(object sender, EventArgs e)
        {
            if (dgvCommandesDvd.CurrentCell == null || bdgCommandesDvd.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande.", "Aucune commande sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbxCommandeDvdSuivi.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner une étape de suivi.", "Étape non sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommandeDocument cmd = (CommandeDocument)bdgCommandesDvd.List[bdgCommandesDvd.Position];
            Categorie nouveauSuivi = (Categorie)cbxCommandeDvdSuivi.SelectedItem;

            if ((cmd.IdSuivi == "00003" || cmd.IdSuivi == "00004") &&
                (nouveauSuivi.Id == "00001" || nouveauSuivi.Id == "00002"))
            {
                MessageBox.Show("Une commande livrée ou réglée ne peut pas revenir à une étape précédente.",
                    "Transition interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nouveauSuivi.Id == "00004" && cmd.IdSuivi != "00003")
            {
                MessageBox.Show("Une commande ne peut être réglée que si elle est livrée.",
                    "Transition interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool succes = controller.ModifierSuiviCommande(cmd.Id, nouveauSuivi.Id);
            if (succes)
            {
                MessageBox.Show("Suivi mis à jour avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RechargerCommandesDvd();
            }
            else
            {
                MessageBox.Show("Erreur lors de la mise à jour du suivi.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommandeDvdSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvCommandesDvd.CurrentCell == null || bdgCommandesDvd.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.", "Aucune commande sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommandeDocument cmd = (CommandeDocument)bdgCommandesDvd.List[bdgCommandesDvd.Position];

            if (cmd.IdSuivi == "00003" || cmd.IdSuivi == "00004")
            {
                MessageBox.Show("Impossible de supprimer une commande livrée ou réglée.",
                    "Suppression interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                string.Format("Supprimer la commande n°{0} du {1} ?", cmd.Id, cmd.DateCommande.ToString("dd/MM/yyyy")),
                "Confirmation de suppression",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool succes = controller.SupprimerCommande(cmd.Id);
                if (succes)
                {
                    MessageBox.Show("Commande supprimée avec succès.", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RechargerCommandesDvd();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de la commande.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Onglet Commandes Revues
        private readonly BindingSource bdgCommandesRevues = new BindingSource();
        private List<Revue> lesRevuesPourAbonnement = new List<Revue>();
        private List<CommandeAbonnement> lesAbonnementsAffiches = new List<CommandeAbonnement>();
        private Revue revueCouranteAbonnement = null;

        private void TabCommandeRevues_Enter(object sender, EventArgs e)
        {
            lesRevuesPourAbonnement = controller.GetAllRevues();
            VideCommandeRevueInfos();
            lesAbonnementsAffiches = controller.GetAllCommandesAbonnement();
            RemplirAbonnementsListe(lesAbonnementsAffiches);
        }

        private void BtnCommandeRevueRechercher_Click(object sender, EventArgs e)
        {
            string numRecherche = txbCommandeRevueNumRecherche.Text.Trim();
            if (string.IsNullOrEmpty(numRecherche))
            {
                MessageBox.Show("Veuillez saisir un numéro de revue.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Revue revue = lesRevuesPourAbonnement.Find(x => x.Id.Equals(numRecherche));
            if (revue == null)
            {
                MessageBox.Show("Numéro de revue introuvable.", "Revue non trouvée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VideCommandeRevueInfos();
                return;
            }

            revueCouranteAbonnement = revue;
            AfficheCommandeRevueInfos(revue);
            ChargerAbonnementsRevue(revue.Id);
        }

        private void AfficheCommandeRevueInfos(Revue revue)
        {
            txbCommandeRevueTitre.Text = revue.Titre;
            txbCommandeRevuePeriodicite.Text = revue.Periodicite;
            txbCommandeRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbCommandeRevueGenre.Text = revue.Genre;
            txbCommandeRevuePublic.Text = revue.Public;
            txbCommandeRevueRayon.Text = revue.Rayon;
            txbCommandeRevueImage.Text = revue.Image;
            try { pcbCommandeRevueImage.Image = Image.FromFile(revue.Image); }
            catch (Exception) { pcbCommandeRevueImage.Image = null; }
        }

        private void VideCommandeRevueInfos()
        {
            revueCouranteAbonnement = null;
            txbCommandeRevueTitre.Text = "";
            txbCommandeRevuePeriodicite.Text = "";
            txbCommandeRevueDelaiMiseADispo.Text = "";
            txbCommandeRevueGenre.Text = "";
            txbCommandeRevuePublic.Text = "";
            txbCommandeRevueRayon.Text = "";
            txbCommandeRevueImage.Text = "";
            pcbCommandeRevueImage.Image = null;
            lesAbonnementsAffiches.Clear();
            dgvCommandesRevues.DataSource = null;
        }

        private void ChargerAbonnementsRevue(string idRevue)
        {
            lesAbonnementsAffiches = controller.GetCommandesRevue(idRevue);
            RemplirAbonnementsListe(lesAbonnementsAffiches);
        }

        private void RechargerAbonnements()
        {
            if (revueCouranteAbonnement != null)
                ChargerAbonnementsRevue(revueCouranteAbonnement.Id);
            else
            {
                lesAbonnementsAffiches = controller.GetAllCommandesAbonnement();
                RemplirAbonnementsListe(lesAbonnementsAffiches);
            }
        }


        private void RemplirAbonnementsListe(List<CommandeAbonnement> abonnements)
        {
            bdgCommandesRevues.DataSource = abonnements;
            dgvCommandesRevues.DataSource = bdgCommandesRevues;
            if (dgvCommandesRevues.Columns.Count > 0)
            {
                if (dgvCommandesRevues.Columns.Contains("IdRevue"))
                    dgvCommandesRevues.Columns["IdRevue"].Visible = false;
                if (dgvCommandesRevues.Columns.Contains("Id"))
                    dgvCommandesRevues.Columns["Id"].HeaderText = "N° commande";
                if (dgvCommandesRevues.Columns.Contains("DateCommande"))
                    dgvCommandesRevues.Columns["DateCommande"].HeaderText = "Date commande";
                if (dgvCommandesRevues.Columns.Contains("Montant"))
                    dgvCommandesRevues.Columns["Montant"].HeaderText = "Montant (€)";
                if (dgvCommandesRevues.Columns.Contains("DateFinAbonnement"))
                    dgvCommandesRevues.Columns["DateFinAbonnement"].HeaderText = "Fin abonnement";
                dgvCommandesRevues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void DgvCommandesRevues_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void BtnCommandeRevueEnregistrer_Click(object sender, EventArgs e)
        {
            if (revueCouranteAbonnement == null)
            {
                MessageBox.Show("Veuillez d'abord rechercher une revue.", "Aucune revue sélectionnée",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpCommandeRevueDateFin.Value.Date <= dtpCommandeRevueDateCommande.Value.Date)
            {
                MessageBox.Show("La date de fin d'abonnement doit être postérieure à la date de commande.",
                    "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nextId = controller.GetNextCommandeId();
            CommandeAbonnement nouvelAbonnement = new CommandeAbonnement(
                nextId,
                dtpCommandeRevueDateCommande.Value.Date,
                (double)nudCommandeRevueMontant.Value,
                dtpCommandeRevueDateFin.Value.Date,
                revueCouranteAbonnement.Id
            );

            bool succes = controller.CreerAbonnement(nouvelAbonnement);
            if (succes)
            {
                MessageBox.Show("Abonnement enregistré avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerAbonnementsRevue(revueCouranteAbonnement.Id);
                nudCommandeRevueMontant.Value = 0;
                dtpCommandeRevueDateCommande.Value = DateTime.Today;
                dtpCommandeRevueDateFin.Value = DateTime.Today;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de l'abonnement.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommandeRevueSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvCommandesRevues.CurrentCell == null || bdgCommandesRevues.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un abonnement à supprimer.", "Aucun abonnement sélectionné",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommandeAbonnement abonnement = (CommandeAbonnement)bdgCommandesRevues.List[bdgCommandesRevues.Position];

            string idRevuePourCheck = revueCouranteAbonnement != null ? revueCouranteAbonnement.Id : abonnement.IdRevue;
            List<Exemplaire> exemplaires = controller.GetExemplairesRevue(idRevuePourCheck);
            bool exemplaireDansAbonnement = exemplaires.Exists(ex =>
                FrmMediatekController.ParutionDansAbonnement(
                    abonnement.DateCommande,
                    abonnement.DateFinAbonnement,
                    ex.DateAchat));

            if (exemplaireDansAbonnement)
            {
                MessageBox.Show(
                    "Impossible de supprimer cet abonnement : un ou plusieurs exemplaires ont été reçus pendant la période d'abonnement.",
                    "Suppression interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Supprimer l'abonnement n°{abonnement.Id} (fin le {abonnement.DateFinAbonnement:dd/MM/yyyy}) ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool succes = controller.SupprimerAbonnement(abonnement.Id);
                if (succes)
                {
                    MessageBox.Show("Abonnement supprimé avec succès.", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RechargerAbonnements();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de l'abonnement.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
