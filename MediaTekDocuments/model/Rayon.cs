
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Rayon (rayon de classement du document) hérite de Categorie
    /// </summary>
    public class Rayon : Categorie
    {
        /// <summary>
        /// Crée un nouveau rayon
        /// </summary>
        /// <param name="id">identifiant du rayon</param>
        /// <param name="libelle">libellé du rayon</param>
        public Rayon(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
