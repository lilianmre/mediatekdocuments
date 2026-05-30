
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier LivreDvd hérite de Document
    /// </summary>
    public abstract class LivreDvd : Document
    {
        /// <summary>
        /// Initialise les propriétés communes aux livres et dvd
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
        protected LivreDvd(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
        }

    }
}
