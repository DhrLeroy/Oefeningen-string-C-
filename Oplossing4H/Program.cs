Console.Write("Zin: ");
var zin = Console.ReadLine();
var woorden = zin.Split(" ");
string[] omgekeerdeWoorden = new string[woorden.Length];

for (int i = 0; i < woorden.Length; i++)
{
    omgekeerdeWoorden[omgekeerdeWoorden.Length - 1 - i] = woorden[i];
}

var woordenOmgedraaid = string.Join(" ", omgekeerdeWoorden);

Console.WriteLine(omgekeerdeWoorden);