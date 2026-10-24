Console.Write("Zin: ");
var zin = Console.ReadLine();

var woorden = zin.Split(" ");

var langste_woord = "";

var leestekens = new string[] { ",", ".", ";", "!", "?", "'", "\"", ":" };

foreach(var woord in woorden)
{
    var woord_zonder_leestekens = woord;
    foreach (var leesteken in leestekens)
        woord_zonder_leestekens = woord_zonder_leestekens.Replace(leesteken, "");
    if (woord_zonder_leestekens.Length > langste_woord.Length)
        langste_woord = woord_zonder_leestekens.ToUpper();
}

Console.WriteLine($"Langste woord: {langste_woord}");