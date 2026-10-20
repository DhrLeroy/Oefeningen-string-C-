string input = Console.ReadLine();

string spel = input.Substring(0, input.LastIndexOf("(") - 1);

string console = input.Substring(input.LastIndexOf("(") + 1, input.LastIndexOf(")") - input.LastIndexOf("(") - 1);

string p = input.Substring(input.LastIndexOf("–") + 1).Replace("€", "").Trim();

float prijs = Convert.ToSingle(p);

Console.WriteLine($"Titel: {spel}");
Console.WriteLine($"Console: {console}");
Console.WriteLine($"Prijs: {prijs}");
