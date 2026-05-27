-- Mission 4 : Authentification
-- Ajout des tables service et utilisateur dans la base mediatek86

USE mediatek86;

-- Table service
CREATE TABLE IF NOT EXISTS service (
    id VARCHAR(5) NOT NULL,
    libelle VARCHAR(50) NOT NULL,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Table utilisateur
CREATE TABLE IF NOT EXISTS utilisateur (
    login VARCHAR(50) NOT NULL,
    pwd VARCHAR(50) NOT NULL,
    nom VARCHAR(50) NOT NULL,
    prenom VARCHAR(50) NOT NULL,
    idService VARCHAR(5) NOT NULL,
    PRIMARY KEY (login),
    CONSTRAINT fk_utilisateur_service FOREIGN KEY (idService) REFERENCES service(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Données de test : services
INSERT INTO service (id, libelle) VALUES
('00001', 'Diffusion'),
('00002', 'Prêt'),
('00003', 'Culture');

-- Données de test : utilisateurs
-- Service Diffusion (accès complet : documents + commandes + alertes abonnements)
INSERT INTO utilisateur (login, pwd, nom, prenom, idService) VALUES
('admin', 'adminpwd', 'Admin', 'Super', '00001');

-- Service Prêt (accès documents uniquement : Livres, DVD, Revues, Parutions)
INSERT INTO utilisateur (login, pwd, nom, prenom, idService) VALUES
('dupont', 'dupont', 'Dupont', 'Marie', '00002');

-- Service Culture (aucun accès : message d'erreur + fermeture)
INSERT INTO utilisateur (login, pwd, nom, prenom, idService) VALUES
('martin', 'martin', 'Martin', 'Jean', '00003');
