
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi : étapes de suivi d'une commande de livre ou dvd
    /// Hérite de Categorie pour être utilisable dans les ComboBox
    /// </summary>
    public class Suivi : Categorie
    {
        /// <summary>
        /// Crée une nouvelle étape de suivi
        /// </summary>
        /// <param name="id">identifiant de l'étape</param>
        /// <param name="libelle">libellé de l'étape</param>
        public Suivi(string id, string libelle) : base(id, libelle)
        {
        }
    }
}
