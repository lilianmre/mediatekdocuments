using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAuthentification
    /// </summary>
    public class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Retourne l'utilisateur correspondant au login et mot de passe, ou null si introuvable
        /// </summary>
        /// <param name="login">login saisi</param>
        /// <param name="pwd">mot de passe saisi</param>
        /// <returns>Objet Utilisateur ou null</returns>
        public Utilisateur GetUtilisateur(string login, string pwd)
        {
            return access.GetUtilisateur(login, pwd);
        }
    }
}
