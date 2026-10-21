Console.Write("Woord: ");
var woord = Console.ReadLine();
var gemaskeerd = new string('_', woord.Length);
var gevonden_plaatsen = 0;
var letters = new List<string>();


for(int i = 1; i <= 10; i++)
{
    Console.Write("Letter: ");
    var letter = Console.ReadLine().ToUpper();
    if (letters.Contains(letter))
        continue;
    letters.Add(letter);

    var nieuw_gemaskeerd = "";
    for(int j = 0; j < gemaskeerd.Length; j++)
    {
        var letter_in_woord = woord.Substring(j, 1);
        if (letter_in_woord.Equals(letter, StringComparison.CurrentCultureIgnoreCase))
        {
            gevonden_plaatsen++;
            nieuw_gemaskeerd += letter_in_woord;
        }
        else
            nieuw_gemaskeerd += gemaskeerd.Substring(j, 1);
    }
    gemaskeerd = nieuw_gemaskeerd;
    Console.WriteLine(gemaskeerd);
    if (gevonden_plaatsen == woord.Length)
    {
        Console.WriteLine("Proficiat!");
        break;
    }
    Console.WriteLine(string.Join(", ", letters));
    Console.WriteLine($"Nog {10-i} beurten.");
}