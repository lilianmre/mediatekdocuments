DROP TRIGGER IF EXISTS after_update_suivi_livraison;
DELIMITER //
CREATE TRIGGER after_update_suivi_livraison
AFTER UPDATE ON commandedocument
FOR EACH ROW
BEGIN
    DECLARE v_idSuiviLivree VARCHAR(5) DEFAULT NULL;
    DECLARE v_idEtatNeuf   VARCHAR(5) DEFAULT NULL;
    DECLARE v_dateCommande DATE;
    DECLARE v_maxNumero    INT DEFAULT 0;
    DECLARE v_i            INT DEFAULT 1;
    DECLARE CONTINUE HANDLER FOR NOT FOUND BEGIN END;

    -- Récupère l'id de l'étape "livrée" et de l'état "neuf"
    SELECT id INTO v_idSuiviLivree FROM suivi WHERE LOWER(libelle) = 'livrée' LIMIT 1;
    SELECT id INTO v_idEtatNeuf    FROM etat   WHERE LOWER(libelle) = 'neuf'   LIMIT 1;

    -- Ne s'exécute que lors du passage à "livrée"
    IF NEW.idSuivi = v_idSuiviLivree AND OLD.idSuivi != v_idSuiviLivree THEN
        SELECT dateCommande INTO v_dateCommande FROM commande WHERE id = NEW.id;
        SELECT COALESCE(MAX(numero), 0) INTO v_maxNumero
            FROM exemplaire WHERE id = NEW.idLivreDvd;

        WHILE v_i <= NEW.nbExemplaire DO
            INSERT INTO exemplaire (id, numero, dateAchat, photo, idEtat)
            VALUES (NEW.idLivreDvd, v_maxNumero + v_i, v_dateCommande, '', v_idEtatNeuf);
            SET v_i = v_i + 1;
        END WHILE;
    END IF;
END//
DELIMITER ;
