
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Genre : hérite de Categorie
    /// </summary>
    public class Genre : Categorie
    {
        /// <summary>
        /// Crée un nouveau genre
        /// </summary>
        /// <param name="id">identifiant du genre</param>
        /// <param name="libelle">libellé du genre</param>
        public Genre(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
