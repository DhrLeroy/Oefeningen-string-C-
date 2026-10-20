Console.Write("Voornaam: ");
string vn = Console.ReadLine();
Console.Write("Naam: ");
string n = Console.ReadLine();
Console.Write($"{vn.ToUpper().Substring(0, 1)}. {n.ToUpper().Substring(0, 1)}.");