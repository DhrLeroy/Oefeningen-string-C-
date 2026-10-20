Console.Write("Geef een woord in: ");

string woord = Console.ReadLine();

List<string> letters = new List<string>();
for (int i = 0; i < woord.Length; i++)
{
    letters.Add(woord.Substring(i, 1).ToUpper());
}

string van_elkaar = string.Join(" ", letters);

Console.WriteLine(van_elkaar);