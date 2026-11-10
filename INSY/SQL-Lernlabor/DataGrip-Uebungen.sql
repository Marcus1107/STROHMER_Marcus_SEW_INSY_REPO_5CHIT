-- MySQL: Öffne zwei Query Consoles für @localhost, jede mit eigener Session.
-- Lass beide auf Tx: Auto. Führe jede Anweisung einzeln mit Strg+Enter aus.
-- Die Buchstaben geben an, in welche Konsole du den Befehl tippst.

-- Vorbereitung: Diesen Block einmal in Konsole A ausführen.
-- Vorher offene Übungen mit COMMIT oder ROLLBACK abschließen.
CREATE DATABASE IF NOT EXISTS transaktionen_lernlabor;
USE transaktionen_lernlabor;

CREATE TABLE IF NOT EXISTS accounts (
    id INT PRIMARY KEY,
    amount INT NOT NULL
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS saldata (
    id INT PRIMARY KEY,
    sal INT NOT NULL
) ENGINE = InnoDB;

INSERT IGNORE INTO accounts (id, amount) VALUES (1, 100), (2, 50);
INSERT IGNORE INTO saldata (id, sal) VALUES (1, 100);

-- Auch in Konsole B vor der ersten Übung ausführen:
USE transaktionen_lernlabor;

-- Übung 1: Was sieht Lisa vor dem Commit?
-- A (du)
START TRANSACTION;
UPDATE accounts SET amount = amount - 20 WHERE id = 1;
SELECT * FROM accounts ORDER BY id; -- 80 und 50
-- B (Lisa)
SELECT * FROM accounts ORDER BY id; -- 100 und 50
-- A
ROLLBACK;
SELECT * FROM accounts ORDER BY id; -- wieder 100 und 50

-- Übung 2: Überweisung bestätigen
-- A
START TRANSACTION;
UPDATE accounts SET amount = amount - 20 WHERE id = 1;
UPDATE accounts SET amount = amount + 20 WHERE id = 2;
COMMIT;
-- B
SELECT * FROM accounts ORDER BY id; -- 80 und 70

-- Übung 3: Lost Update. A will 10 addieren, B will 5 addieren.
-- A
SELECT sal FROM saldata WHERE id = 1;
-- B
SELECT sal FROM saldata WHERE id = 1;
-- A
UPDATE saldata SET sal = 110 WHERE id = 1;
-- B
UPDATE saldata SET sal = 105 WHERE id = 1;
SELECT sal FROM saldata WHERE id = 1; -- 105: Die erste Erhöhung ging verloren.

-- Noch einmal, diesmal rechnet die Datenbank mit dem aktuellen Wert.
-- A
UPDATE saldata SET sal = 100 WHERE id = 1;
UPDATE saldata SET sal = sal + 10 WHERE id = 1;
-- B
UPDATE saldata SET sal = sal + 5 WHERE id = 1;
SELECT sal FROM saldata WHERE id = 1; -- 115

-- Zurück zum Start (nach Abschluss der Übungen in A ausführen)
UPDATE accounts SET amount = 100 WHERE id = 1;
UPDATE accounts SET amount = 50 WHERE id = 2;
UPDATE saldata SET sal = 100 WHERE id = 1;


USE transaktionen_lernlabor;

START TRANSACTION;
UPDATE accounts SET amount = 100 WHERE id = 1;
UPDATE accounts SET amount = 50 WHERE id = 2;
UPDATE saldata SET sal = 100 WHERE id = 1;
COMMIT;

