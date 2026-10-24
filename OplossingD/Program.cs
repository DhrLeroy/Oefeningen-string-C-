Console.Write("Woord: ");

var woord = Console.ReadLine();

var gemaskeerd = woord.Substring(0, 1);
for(int i = 0; i < woord.Length - 2; i++)
{
    gemaskeerd += "*";
}
gemaskeerd += woord.Substring(woord.Length - 1);
Console.WriteLine(gemaskeerd);