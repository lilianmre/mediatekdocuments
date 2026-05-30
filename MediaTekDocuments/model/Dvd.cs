
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Dvd hérite de LivreDvd : contient des propriétés spécifiques aux dvd
    /// </summary>
    public class Dvd : LivreDvd
    {
        /// <summary>Durée du dvd en minutes</summary>
        public int Duree { get; }
        /// <summary>Nom du réalisateur</summary>
        public string Realisateur { get; }
        /// <summary>Synopsis du dvd</summary>
        public string Synopsis { get; }

        /// <summary>
        /// Crée un nouveau dvd
        /// </summary>
        /// <param name="id">identifiant</param>
        /// <param name="titre">titre</param>
        /// <param name="image">chemin de l'image</param>
        /// <param name="duree">durée en minutes</param>
        /// <param name="realisateur">réalisateur</param>
        /// <param name="synopsis">synopsis</param>
        /// <param name="idGenre">identifiant du genre</param>
        /// <param name="genre">libellé du genre</param>
        /// <param name="idPublic">identifiant du public</param>
        /// <param name="lePublic">libellé du public</param>
        /// <param name="idRayon">identifiant du rayon</param>
        /// <param name="rayon">libellé du rayon</param>
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Duree = duree;
            this.Realisateur = realisateur;
            this.Synopsis = synopsis;
        }

    }
}
