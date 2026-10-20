/*string tekst = "Dit is een saaie les...";

var woord = "saaie";

tekst = tekst.Remove(tekst.IndexOf(woord), woord.Length+1);

Console.WriteLine(tekst);
*/

//Bv. Ann, Bert, Charlie, David, Evert, Fatima

var namen = new List<string>();

var namen_aan_elkaar = "";

while(true)
{
    Console.Write("Naam: ");
    var naam = Console.ReadLine();
    if (naam.Equals("stop", StringComparison.CurrentCultureIgnoreCase))
        break;
    namen_aan_elkaar += naam + ", ";
    namen.Add(naam);
}
Console.WriteLine(string.Join(", ", namen));
Console.WriteLine(string.Join(", ", namen_aan_elkaar));