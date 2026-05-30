
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur : contient les informations de l'utilisateur connecté
    /// </summary>
    public class Utilisateur
    {
        /// <summary>Login de l'utilisateur</summary>
        public string Login { get; set; }
        /// <summary>Nom de l'utilisateur</summary>
        public string Nom { get; set; }
        /// <summary>Prénom de l'utilisateur</summary>
        public string Prenom { get; set; }
        /// <summary>Identifiant du service de l'utilisateur</summary>
        public string IdService { get; set; }
        /// <summary>Libellé du service de l'utilisateur</summary>
        public string Service { get; set; }

        /// <summary>
        /// Crée un nouvel utilisateur
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="nom">nom</param>
        /// <param name="prenom">prénom</param>
        /// <param name="idService">identifiant du service</param>
        /// <param name="service">libellé du service</param>
        public Utilisateur(string login, string nom, string prenom, string idService, string service)
        {
            Login = login;
            Nom = nom;
            Prenom = prenom;
            IdService = idService;
            Service = service;
        }
    }
}
