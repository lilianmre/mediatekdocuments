using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    public class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }


        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Retourne toutes les étapes de suivi
        /// </summary>
        public List<Categorie> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        /// <summary>
        /// Retourne toutes les commandes de livres/DVD (sans filtre)
        /// </summary>
        public List<CommandeDocument> GetAllCommandesDocument()
        {
            return access.GetAllCommandesDocument();
        }

        /// <summary>
        /// Retourne toutes les commandes d'un livre ou DVD
        /// </summary>
        public List<CommandeDocument> GetCommandesDocument(string idLivreDvd)
        {
            return access.GetCommandesDocument(idLivreDvd);
        }

        /// <summary>
        /// Retourne le prochain identifiant disponible pour une commande
        /// </summary>
        public string GetNextCommandeId()
        {
            return access.GetNextCommandeId();
        }

        /// <summary>
        /// Crée une commande dans la BDD
        /// </summary>
        public bool CreerCommande(CommandeDocument commande)
        {
            return access.CreerCommande(commande);
        }

        /// <summary>
        /// Modifie l'étape de suivi d'une commande
        /// </summary>
        public bool ModifierSuiviCommande(string idCommande, string idSuivi)
        {
            return access.ModifierSuiviCommande(idCommande, idSuivi);
        }

        /// <summary>
        /// Supprime une commande (non encore livrée)
        /// </summary>
        public bool SupprimerCommande(string idCommande)
        {
            return access.SupprimerCommande(idCommande);
        }

        /// <summary>
        /// Retourne tous les abonnements de revues (sans filtre)
        /// </summary>
        public List<CommandeAbonnement> GetAllCommandesAbonnement()
        {
            return access.GetAllCommandesAbonnement();
        }

        /// <summary>
        /// Retourne toutes les commandes d'abonnement d'une revue
        /// </summary>
        public List<CommandeAbonnement> GetCommandesRevue(string idRevue)
        {
            return access.GetCommandesRevue(idRevue);
        }

        /// <summary>
        /// Crée un abonnement dans la BDD
        /// </summary>
        public bool CreerAbonnement(CommandeAbonnement abonnement)
        {
            return access.CreerAbonnement(abonnement);
        }

        /// <summary>
        /// Supprime un abonnement si aucun exemplaire ne lui est rattaché
        /// </summary>
        public bool SupprimerAbonnement(string idAbonnement)
        {
            return access.SupprimerAbonnement(idAbonnement);
        }

        /// <summary>
        /// Retourne les revues dont l'abonnement se termine dans moins de 30 jours
        /// </summary>
        public List<RevueEnAlerte> GetRevuesAbonnementExpirant()
        {
            return access.GetRevuesAbonnementExpirant();
        }

        /// <summary>
        /// Retourne vrai si la date de parution est comprise entre la date de commande
        /// et la date de fin d'abonnement (bornes incluses)
        /// </summary>
        public static bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return dateParution >= dateCommande && dateParution <= dateFinAbonnement;
        }
    }
}
