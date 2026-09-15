using ObserverLernprojekt;

// Ohne Argument werden beide Varianten nacheinander ausgeführt.
string modus = args.Length == 0 ? "beide" : args[0].ToLowerInvariant();

switch (modus)
{
    case "klassisch":
        Demo.Klassisch();
        break;
    case "events":
        Demo.MitEvents();
        break;
    case "beide":
        Demo.Klassisch();
        Console.WriteLine();
        Demo.MitEvents();
        break;
    default:
        Console.WriteLine("Verwendung: dotnet run -- [beide|klassisch|events]");
        Environment.ExitCode = 1;
        break;
}
