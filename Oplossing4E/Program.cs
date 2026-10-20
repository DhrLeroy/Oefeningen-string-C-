var invoer = Console.ReadLine();

string zonder_dubbels = invoer;

for (int i = 0; i < invoer.Length; i++)
{
    var teken = invoer.Substring(i, 1);
    zonder_dubbels = zonder_dubbels.Replace(teken + teken, teken, StringComparison.CurrentCultureIgnoreCase);
}

Console.WriteLine(zonder_dubbels);                     