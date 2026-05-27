
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur : contient les informations de l'utilisateur connecté
    /// </summary>
    public class Utilisateur
    {
        public string Login { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string IdService { get; set; }
        public string Service { get; set; }

        public Utilisateur(string login, string nom, string prenom, string idService, string service)
        {
            this.Login = login;
            this.Nom = nom;
            this.Prenom = prenom;
            this.IdService = idService;
            this.Service = service;
        }
    }
}
