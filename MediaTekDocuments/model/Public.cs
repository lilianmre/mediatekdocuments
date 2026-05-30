
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Public (public concerné par le document) hérite de Categorie
    /// </summary>
    public class Public : Categorie
    {
        /// <summary>
        /// Crée un nouveau public cible
        /// </summary>
        /// <param name="id">identifiant du public</param>
        /// <param name="libelle">libellé du public</param>
        public Public(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
