Console.Write("Woorden: ");

var input = Console.ReadLine();

var woorden = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

var output = new string[input.Length];

for (int i = 0; i < woorden.Length; i++)
{
    var woord = woorden[i];
    var omgedraaid = "";
    for (int positie = woord.Length - 1; positie >= 0; positie--)
    {
        omgedraaid += woord[positie];
    }
    output[i] = $"{woord}: {woord.Equals(omgedraaid, StringComparison.CurrentCultureIgnoreCase)}";
}

foreach (var resultaat in output)
{
    Console.WriteLine(resultaat);
}