USE Transactions;

-- Einmalig in DataGrip ausführen.
-- Bestehende Einträge haben keinen bekannten Zeitpunkt und bleiben NULL.
ALTER TABLE changelog
    ADD COLUMN changedAt TIMESTAMP(6) NULL DEFAULT NULL;

-- Neue Einträge erhalten automatisch den Zeitpunkt ihrer Erstellung.
-- Der vorhandene Trigger muss dafür nicht geändert werden.
ALTER TABLE changelog
    MODIFY COLUMN changedAt TIMESTAMP(6) NULL DEFAULT CURRENT_TIMESTAMP(6);

SELECT updateId, oldValue, newVlaue, accountId, changedAt
FROM changelog
ORDER BY updateId;
