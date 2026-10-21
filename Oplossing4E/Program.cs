var invoer = Console.ReadLine();

string zonder_dubbels = invoer.Substring(0,1);

for (int i = 1; i < invoer.Length; i++)
{
    if (!invoer.Substring(i, 1).Equals(invoer.Substring(i - 1, 1), StringComparison.CurrentCultureIgnoreCase))
        zonder_dubbels += invoer.Substring(i, 1);
}

Console.WriteLine(zonder_dubbels);                     