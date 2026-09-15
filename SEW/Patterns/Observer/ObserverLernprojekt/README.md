# Observer Pattern in C#: eine Wetterstation, zwei Umsetzungen

Eine Wetterstation informiert eine Temperaturanzeige und einen Hitzealarm.
Beide melden sich an, reagieren unterschiedlich und können sich abmelden.
Die klassischen Interfaces und C#-Events setzen dieselbe Entwurfsidee um.

## Starten

1. ZIP vollständig entpacken.
2. Das .NET 8 SDK installieren, falls es fehlt (SDK, nicht nur Runtime):
   https://dotnet.microsoft.com/download/dotnet/8.0
3. Ein Terminal im Ordner mit `ObserverLernprojekt.csproj` öffnen.
4. Ausführen:

```sh
dotnet run
dotnet run -- klassisch
dotnet run -- events
```

Das Projekt zielt auf .NET 8. Mit einem neueren SDK brauchst du für dieses
Ziel auch die passenden .NET-8-Komponenten. Alternativ ändere in der csproj
`net8.0` auf das zu deinem installierten SDK passende Ziel, z. B. `net10.0`.
Es gibt keine externen NuGet-Paketabhängigkeiten.
In Visual Studio: `ObserverLernprojekt.csproj` öffnen und mit Strg+F5 starten.
Ein fehlender `dotnet`-Befehl bedeutet meist: SDK fehlt oder Terminal nach
der Installation noch nicht neu geöffnet.

## Empfohlene Lesereihenfolge

1. `Klassisch/Vertraege.cs`: Was versprechen Quelle und Beobachter?
2. `Klassisch/Wetterstation.cs`: Liste pflegen und Beobachter aufrufen.
3. `Klassisch/Beobachter.cs`: Zwei unterschiedliche Reaktionen.
4. `Demo.cs`, Methode `Klassisch`: Objekte miteinander verbinden.
5. Dateien in `MitEvents`: dieselbe Idee mit `event` und `EventHandler<T>`.
6. `Demo.cs`, Methode `MitEvents`: mit `+=` anmelden und `-=` abmelden.
7. Die beiliegende PDF erklärt alle Schritte und enthält Übungen mit Lösungen.

`Program.cs` wählt nur die Demo aus. Die Namespaces trennen gleichnamige
Klassen der beiden Varianten; `K` und `E` in Demo.cs sind kurze Aliasnamen.

## Erwartetes Verhalten

Bei 22 °C zeigt nur die Anzeige etwas an; der Alarm erhält die Meldung,
bleibt aber unterhalb von 30 °C still. Bei 31 °C reagieren beide sichtbar.
Nach Abmeldung der Anzeige gibt bei 35 °C nur noch der Alarm etwas aus.
Ein zweites Setzen von 35 °C bewirkt nichts. Nach Abmeldung beider
Beobachter meldet die Station 36 °C weiterhin selbst, aber niemand reagiert.
`Erwartete-Ausgabe.txt` zeigt die aus dem Code abgeleitete vollständige Ausgabe.

## Ausprobieren

- Ändere 31 auf 29 und beobachte den Alarm.
- Kommentiere die Abmeldung der Anzeige aus: Sie zeigt dann auch 35 und 36 an.
- Ergänze einen Frostalarm, der bei Temperaturen unter 0 reagiert.
- Melde denselben Beobachter zweimal an: Die klassische Variante verhindert
  doppelte Einträge; zweimal `+=` registriert den Event-Handler zweimal.
- Melde einen Beobachter erst nach der ersten Messung an: Die Anmeldung
  selbst liefert in diesem Projekt keinen alten Messwert nach.

## Bewusste Vereinfachungen

Ganzzahlige Temperaturen und ein festes Demo-Skript halten den Ablauf sichtbar.
`int?` enthält vor der ersten Messung `null`, damit auch 0 als erste Messung
gemeldet wird. Gleiche Folgemessungen werden in beiden Varianten unterdrückt.
Alle Aufrufe laufen synchron auf einem Thread. Die Listenkopie ist kein
vollständiger Schutz für nebenläufigen Zugriff. Nicht behandelte Exceptions
eines Beobachters können weitere Benachrichtigungen verhindern.

Events existieren bereits seit C# 1.0. „Klassisch“ bezeichnet hier die
manuelle Implementierung des Entwurfsmusters und keine alte C#-Version.
Die eingebauten .NET-Interfaces `IObservable<T>` und `IObserver<T>` sind eine
weitere Möglichkeit und nicht die eigenen Interfaces dieses Projekts.

## Prüfung dieses Downloads

Quellcode und erwartete Ausgabe wurden statisch auf Konsistenz geprüft.
In der Erstellungsumgebung war kein C#/.NET-SDK verfügbar; das Projekt
wurde dort deshalb nicht kompiliert oder ausgeführt. Lokal prüfen:

```sh
dotnet build
dotnet run
```

Vergleiche die Ausgabe mit `Erwartete-Ausgabe.txt` (Zeilenenden können je
nach Betriebssystem abweichen). Zum Debuggen einen Haltepunkt in
`Aktualisieren` bzw. `BeiTemperaturAenderung` setzen und die Aufrufliste ansehen.
