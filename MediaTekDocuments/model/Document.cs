
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Document (réunit les infomations communes à tous les documents : Livre, Revue, Dvd)
    /// </summary>
    public class Document
    {
        /// <summary>Identifiant du document</summary>
        public string Id { get; }
        /// <summary>Titre du document</summary>
        public string Titre { get; }
        /// <summary>Chemin vers l'image du document</summary>
        public string Image { get; }
        /// <summary>Identifiant du genre</summary>
        public string IdGenre { get; }
        /// <summary>Libellé du genre</summary>
        public string Genre { get; }
        /// <summary>Identifiant du public cible</summary>
        public string IdPublic { get; }
        /// <summary>Libellé du public cible</summary>
        public string Public { get; }
        /// <summary>Identifiant du rayon</summary>
        public string IdRayon { get; }
        /// <summary>Libellé du rayon</summary>
        public string Rayon { get; }

        /// <summary>
        /// Crée un nouveau document
        /// </summary>
        /// <param name="id">identifiant</param>
        /// <param name="titre">titre</param>
        /// <param name="image">chemin de l'image</param>
        /// <param name="idGenre">identifiant du genre</param>
        /// <param name="genre">libellé du genre</param>
        /// <param name="idPublic">identifiant du public</param>
        /// <param name="lePublic">libellé du public</param>
        /// <param name="idRayon">identifiant du rayon</param>
        /// <param name="rayon">libellé du rayon</param>
        public Document(string id, string titre, string image, string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            Id = id;
            Titre = titre;
            Image = image;
            IdGenre = idGenre;
            Genre = genre;
            IdPublic = idPublic;
            Public = lePublic;
            IdRayon = idRayon;
            Rayon = rayon;
        }
    }
}
