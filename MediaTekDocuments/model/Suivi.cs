
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi : étapes de suivi d'une commande de livre ou dvd
    /// Hérite de Categorie pour être utilisable dans les ComboBox
    /// </summary>
    public class Suivi : Categorie
    {
        public Suivi(string id, string libelle) : base(id, libelle)
        {
        }
    }
}
