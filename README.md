# MediatekDocuments
Ce dépôt contient une version enrichie de l'application MediaTekDocuments.<br>
Le dépôt d'origine, qui contient dans son readme la présentation de l'application d'origine, se trouve ici :<br>
https://github.com/CNED-SLAM/MediaTekDocuments
## Présentation
Cette version ajoute à l'application d'origine la gestion complète des commandes de livres, de DVD et des abonnements aux revues, ainsi qu'un système d'authentification avec contrôle d'accès par service. Des améliorations de qualité ont également été apportées : sécurisation des identifiants API, intégration des logs Serilog et corrections SonarLint.<br>
<img width="1107" height="863" alt="Capture d&#39;écran 2026-06-09 130058" src="https://github.com/user-attachments/assets/7a7c3e86-2d43-444a-97e9-4c6d841491e2" />
<br>L'application comporte toujours une seule fenêtre divisée en plusieurs onglets. Trois nouveaux onglets ont été ajoutés : "Commandes Livres", "Commandes DVD" et "Commandes Revues".
## Authentification
Au démarrage, une fenêtre d'authentification est affichée avant l'accès à l'application principale. L'utilisateur doit saisir son login et son mot de passe.<br>
<img width="459" height="301" alt="Capture d&#39;écran 2026-06-09 130033" src="https://github.com/user-attachments/assets/b340ce7f-915a-42e5-b374-5fb2913af0e4" />
<br>Selon le service auquel appartient l'utilisateur, les accès sont restreints :<br>
<strong>Service Diffusion :</strong> accès complet à tous les onglets.<br>
<strong>Service Prêt :</strong> accès aux onglets documents uniquement (Livres, DVD, Revues, Parutions). Les onglets de commandes sont masqués.<br>
<strong>Autres services :</strong> accès refusé, l'application se ferme après affichage d'un message d'erreur.
## Les fonctionnalités ajoutées
### Onglet 5 : Commandes Livres
Cet onglet permet de gérer les commandes de livres.<br>
Il se décompose en 2 parties (groupbox).
#### Partie "Recherche livre"
Cette partie permet, à partir de la saisie d'un numéro de livre (puis en cliquant sur "Rechercher"), d'afficher les informations du livre (titre, auteur, image) ainsi que la liste des commandes déjà passées pour ce livre (numéro de commande, date, montant, nombre d'exemplaires, état du suivi).
#### Partie "Commandes du livre"
Cette partie affiche le détail de la commande sélectionnée dans la liste et permet trois actions :<br>
<strong>Ajouter une commande :</strong> saisie du numéro de commande, de la date, du montant et du nombre d'exemplaires commandés, puis validation.<br>
<strong>Modifier le suivi :</strong> le combo "Suivi" permet de faire progresser une commande selon les étapes : En cours → Livrée → Réglée. Les transitions sont contrôlées : on ne peut avancer que d'une étape à la fois et on ne peut pas revenir en arrière.<br>
<strong>Supprimer une commande :</strong> uniquement possible si la commande est encore "En cours". La suppression est interdite si la commande est à l'état Livrée ou Réglée.<br>
<img width="1101" height="861" alt="Capture d&#39;écran 2026-06-09 130121" src="https://github.com/user-attachments/assets/01523b40-7f5f-465b-94c1-fa00b508f5cd" />
### Onglet 6 : Commandes DVD
Le fonctionnement est identique à l'onglet "Commandes Livres", mais appliqué aux DVD.
### Onglet 7 : Commandes Revues
Cet onglet permet de gérer les abonnements aux revues.<br>
Il se décompose en 2 parties (groupbox).
#### Partie "Recherche revue"
Cette partie permet, à partir de la saisie d'un numéro de revue (puis en cliquant sur "Rechercher"), d'afficher les informations de la revue et la liste des abonnements existants pour cette revue (numéro, date de commande, montant, date de fin d'abonnement).
#### Partie "Abonnements de la revue"
<strong>Ajouter un abonnement :</strong> saisie du numéro de commande, de la date, du montant et de la date de fin d'abonnement, puis validation.<br>
<strong>Supprimer un abonnement :</strong> la suppression est impossible si des parutions reçues appartiennent à la période couverte par l'abonnement (contrôle "ParutionDansAbonnement").<br>
<img width="1103" height="859" alt="Capture d&#39;écran 2026-06-09 130139" src="https://github.com/user-attachments/assets/dd6be62c-3abf-4b5f-a447-7f34d8cbc920" />
### Alerte abonnements expirants
Au démarrage de l'application, et uniquement pour les utilisateurs du service Diffusion, une fenêtre d'alerte s'affiche automatiquement si des abonnements arrivent à expiration dans moins de 30 jours. Elle liste les revues concernées avec leur date de fin d'abonnement.<br>
<img width="613" height="407" alt="Capture d&#39;écran 2026-06-09 130051" src="https://github.com/user-attachments/assets/0ab2bb8d-d3c3-4b16-a9ed-7b5ab030e2db" />
## La base de données
La base de données s'appuie sur la structure existante de mediatek86, à laquelle deux ensembles de tables ont été ajoutés.<br>
<img width="255" height="424" alt="Capture d&#39;écran 2026-06-09 130212" src="https://github.com/user-attachments/assets/5dfa92a5-2129-4843-ae9c-150a3b4ec432" />
<br>
<strong>Tables pour l'authentification :</strong><br>
. La table <strong>service</strong> contient les différents services de la médiathèque (ex : Diffusion, Prêt).<br>
. La table <strong>utilisateur</strong> contient les comptes utilisateurs : login, mot de passe (hashé) et service associé.<br>
<br>
<strong>Tables pour les commandes :</strong><br>
. La table <strong>commande</strong> regroupe les informations communes à toute commande : numéro, date et montant.<br>
. La table <strong>commandedocument</strong> spécialise une commande pour les livres et DVD : nombre d'exemplaires commandés, document concerné et état de suivi.<br>
. La table <strong>abonnement</strong> spécialise une commande pour les revues : date de fin d'abonnement et revue concernée.<br>
. La table <strong>suivi</strong> contient les différents états d'avancement d'une commande de document (En cours, Livrée, Réglée).<br>
<br>
Les scripts SQL nécessaires se trouvent à la racine du dépôt : <strong>bdd_authentification.sql</strong> (création des tables service et utilisateur avec données de test) ainsi que les scripts de triggers assurant le contrôle des transitions de suivi.
## L'API REST
L'accès à la BDD se fait à travers une API REST protégée par une authentification basique.<br>
Le code de l'API se trouve ici :<br>
https://github.com/lilianmre/rest_mediatekdocuments<br>
avec toutes les explications pour l'utiliser (dans le readme).
## Installation de l'application
Ce mode opératoire permet d'installer l'application pour pouvoir travailler dessus.
- Installer Visual Studio 2019 et les packages NuGet nécessaires : <strong>Newtonsoft.Json</strong> (accès à l'API REST) et <strong>Serilog</strong> avec ses sinks Console et File pour la gestion des logs. Les packages peuvent être restaurés automatiquement via NuGet au premier build.
- Télécharger le code et le dézipper, puis renommer le dossier en "mediatekdocuments".
- Récupérer et installer l'API REST nécessaire (https://github.com/lilianmre/rest_mediatekdocuments) ainsi que la base de données (les explications sont données dans le readme correspondant).
- Dans phpMyAdmin, après avoir créé et rempli la base mediatek86, exécuter le script <strong>bdd_authentification.sql</strong> pour créer les tables service et utilisateur et y ajouter les comptes de test.
- Exécuter également les scripts de triggers SQL présents à la racine du dépôt pour activer les contrôles métier sur les transitions de suivi des commandes.
- Ouvrir la solution dans Visual Studio. Vérifier que le fichier <strong>App.config</strong> contient les bons paramètres de connexion à l'API (baseUrl, login, password) et les adapter si nécessaire.
- Lancer l'application. La fenêtre d'authentification s'affiche au démarrage. Utiliser un des comptes présents dans la table utilisateur (par exemple login : <strong>admin</strong>, mot de passe : <strong>admin</strong> pour accéder en tant que service Diffusion).
