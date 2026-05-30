
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Livre hérite de LivreDvd : contient des propriétés spécifiques aux livres
    /// </summary>
    public class Livre : LivreDvd
    {
        /// <summary>Numéro ISBN du livre</summary>
        public string Isbn { get; }
        /// <summary>Nom de l'auteur</summary>
        public string Auteur { get; }
        /// <summary>Nom de la collection</summary>
        public string Collection { get; }

        /// <summary>
        /// Crée un nouveau livre
        /// </summary>
        /// <param name="id">identifiant</param>
        /// <param name="titre">titre</param>
        /// <param name="image">chemin de l'image</param>
        /// <param name="isbn">numéro ISBN</param>
        /// <param name="auteur">auteur</param>
        /// <param name="collection">collection</param>
        /// <param name="idGenre">identifiant du genre</param>
        /// <param name="genre">libellé du genre</param>
        /// <param name="idPublic">identifiant du public</param>
        /// <param name="lePublic">libellé du public</param>
        /// <param name="idRayon">identifiant du rayon</param>
        /// <param name="rayon">libellé du rayon</param>
        public Livre(string id, string titre, string image, string isbn, string auteur, string collection,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Isbn = isbn;
            this.Auteur = auteur;
            this.Collection = collection;
        }
    }
}
