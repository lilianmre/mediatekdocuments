
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document)
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Identifiant de l'état
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Libellé de l'état
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Crée un nouvel état
        /// </summary>
        /// <param name="id">identifiant de l'état</param>
        /// <param name="libelle">libellé de l'état</param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
