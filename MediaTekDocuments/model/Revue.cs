
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Revue hérite de Document : contient des propriétés spécifiques aux revues
    /// </summary>
    public class Revue : Document
    {
        /// <summary>Périodicité de la revue (ex. : Mensuelle, Hebdomadaire)</summary>
        public string Periodicite { get; set; }
        /// <summary>Délai de mise à disposition en jours</summary>
        public int DelaiMiseADispo { get; set; }

        /// <summary>
        /// Crée une nouvelle revue
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
        /// <param name="periodicite">périodicité</param>
        /// <param name="delaiMiseADispo">délai de mise à disposition en jours</param>
        public Revue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon,
            string periodicite, int delaiMiseADispo)
             : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            Periodicite = periodicite;
            DelaiMiseADispo = delaiMiseADispo;
        }

    }
}
