const { DatabaseSync } = require('node:sqlite');
const { mkdtempSync, rmSync } = require('node:fs');
const { tmpdir } = require('node:os');
const { join } = require('node:path');
const readline = require('node:readline');

const folder = mkdtempSync(join(tmpdir(), 'sql-lernlabor-'));
const file = join(folder, 'uebung.db');
const sessions = { A: new DatabaseSync(file), B: new DatabaseSync(file) };
sessions.A.exec(`
  PRAGMA journal_mode = WAL;
  CREATE TABLE accounts (id INTEGER PRIMARY KEY, amount INTEGER NOT NULL);
  INSERT INTO accounts VALUES (1, 100), (2, 50);
  CREATE TABLE saldata (id INTEGER PRIMARY KEY, sal INTEGER NOT NULL);
  INSERT INTO saldata VALUES (1, 100);
`);
const lessons = [
  `1. Abbuchung zurücknehmen. Tippe jede Zeile einzeln ein.
BEGIN;
UPDATE accounts SET amount = amount - 20 WHERE id = 1;
SELECT * FROM accounts;
.b
SELECT * FROM accounts;
.a
ROLLBACK;
SELECT * FROM accounts;
Vergleiche den Betrag auf Konto 1 vor und nach dem Fensterwechsel.`,
  `2. Überweisung bestätigen. Beginne nach Übung 1 in Fenster A.
BEGIN;
UPDATE accounts SET amount = amount - 20 WHERE id = 1;
UPDATE accounts SET amount = amount + 20 WHERE id = 2;
COMMIT;
.b
SELECT * FROM accounts;
Prüfe beide Kontostände und ihre Summe.`,
  `3. Lost Update. A will 10 addieren, B will 5 addieren.
.a
SELECT sal FROM saldata WHERE id = 1;
.b
SELECT sal FROM saldata WHERE id = 1;
.a
UPDATE saldata SET sal = 110 WHERE id = 1;
.b
UPDATE saldata SET sal = 105 WHERE id = 1;
SELECT sal FROM saldata WHERE id = 1;
Versuche es erneut: Setze sal zuerst auf 100. Ersetze dann in den beiden
UPDATE-Befehlen die festen Werte durch sal + 10 und sal + 5. Vergleiche.`
];

let active = 'A';
const terminal = readline.createInterface({ input: process.stdin, output: process.stdout });
console.log('SQL-Lernlabor | SQLite | zwei Verbindungen zur selben Übungsdatenbank');
console.log('Ein SQL-Befehl pro Zeile. BEGIN entspricht hier START TRANSACTION in MySQL.');
console.log('.a / .b wechselt das Fenster. .1 / .2 / .3 zeigt die Aufgaben. .ende beendet das Labor.');
console.log('Jeder Neustart beginnt wieder mit den Ausgangswerten.\n');
console.log(lessons[0]);
function prompt() {
  terminal.setPrompt('Fenster ' + active + '> ');
  terminal.prompt();
}
prompt();
terminal.on('line', line => {
  const command = line.trim();
  if (command === '.ende') return terminal.close();
  if (command === '.a' || command === '.b') active = command.slice(1).toUpperCase();
  else if (/^\.[123]$/.test(command)) console.log(lessons[Number(command[1]) - 1]);
  else if (command) {
    try {
      const statement = sessions[active].prepare(command);
      if (statement.columns().length) console.table(statement.all());
      else {
        statement.run();
        console.log('OK');
      }
    } catch (error) {
      console.log('SQL-Fehler: ' + error.message);
    }
  }
  prompt();
});
terminal.on('close', () => {
  sessions.A.close();
  sessions.B.close();
  rmSync(folder, { recursive: true });
});
