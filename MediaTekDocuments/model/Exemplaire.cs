using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Exemplaire (exemplaire d'une revue)
    /// </summary>
    public class Exemplaire
    {
        /// <summary>Numéro de l'exemplaire</summary>
        public int Numero { get; set; }
        /// <summary>Chemin vers la photo de l'exemplaire</summary>
        public string Photo { get; set; }
        /// <summary>Date d'achat de l'exemplaire</summary>
        public DateTime DateAchat { get; set; }
        /// <summary>Identifiant de l'état de l'exemplaire</summary>
        public string IdEtat { get; set; }
        /// <summary>Identifiant du document auquel appartient l'exemplaire</summary>
        public string Id { get; set; }

        /// <summary>
        /// Crée un nouvel exemplaire
        /// </summary>
        /// <param name="numero">numéro de l'exemplaire</param>
        /// <param name="dateAchat">date d'achat</param>
        /// <param name="photo">chemin de la photo</param>
        /// <param name="idEtat">identifiant de l'état</param>
        /// <param name="idDocument">identifiant du document</param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.Id = idDocument;
        }

    }
}
