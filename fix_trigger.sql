DROP TRIGGER IF EXISTS before_delete_commande;
DELIMITER //
CREATE TRIGGER before_delete_commande
BEFORE DELETE ON commande
FOR EACH ROW
BEGIN
    DECLARE v_idSuivi VARCHAR(5) DEFAULT NULL;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_idSuivi = NULL;
    SELECT idSuivi INTO v_idSuivi FROM commandedocument WHERE id = OLD.id;
    IF v_idSuivi IN ('00003', '00004') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Impossible de supprimer une commande livree ou reglee';
    ELSE
        DELETE FROM commandedocument WHERE id = OLD.id;
    END IF;
END//
DELIMITER ;
