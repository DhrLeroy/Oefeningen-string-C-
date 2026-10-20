Console.Write("Geef een jaartal in: ");

var jaartal = Convert.ToInt32(Console.ReadLine());

List<DateTime> feestdagen = new List<DateTime>();
List<string> feestdagNamen = new List<string>();

feestdagNamen.Add("Nieuwjaarsdag");
feestdagen.Add(new DateTime(jaartal, 1, 1));

int a = jaartal % 19;
int b = jaartal / 100;
int c = jaartal % 100;
int d = b / 4;
int e = b % 4;
int f = (b + 8) / 25;
int g = (b - f + 1) / 3;
int h = (19 * a + b - d - g + 15) % 30;
int i = c / 4;
int k = c % 4;
int l = (32 + 2 * e + 2 * i - h - k) % 7;
int m = (a + 11 * h + 22 * l) / 451;
int maand = (h + l - 7 * m + 114) / 31;
int dag = ((h + l - 7 * m + 114) % 31) + 1;

feestdagNamen.Add("Pasen");
var pasen = new DateTime(jaartal, maand, dag);
feestdagen.Add(pasen);

feestdagNamen.Add("Paasmaandag");
feestdagen.Add(pasen.AddDays(1));

feestdagNamen.Add("Feest van de Arbeid");
feestdagen.Add(new DateTime(jaartal, 5, 1));

feestdagNamen.Add("Onze-Lieve-Heer Hemelvaart");
feestdagen.Add(pasen.AddDays(40));

feestdagNamen.Add("Pinkstermaandag");
feestdagen.Add(pasen.AddDays(50));

feestdagNamen.Add("Feestdag van de Vlaamse Gemeenschap");
feestdagen.Add(new DateTime(jaartal, 7, 11));

feestdagNamen.Add("Nationale feestdag van België");
feestdagen.Add(new DateTime(jaartal, 7, 21));

feestdagNamen.Add("Onze-Lieve-Vrouw Hemelvaart");
feestdagen.Add(new DateTime(jaartal, 8, 15));

feestdagNamen.Add("Allerheiligen");
feestdagen.Add(new DateTime(jaartal, 11, 1));

feestdagNamen.Add("Wapenstilstand");
feestdagen.Add(new DateTime(jaartal, 11, 11));

feestdagNamen.Add("Kerstmis");
feestdagen.Add(new DateTime(jaartal, 12, 25));

Console.WriteLine($"Feestdagen in {jaartal}:");
for (int index = 0; index < feestdagen.Count; index++)
{
    Console.WriteLine($"{feestdagNamen[index]}: {feestdagen[index].ToString("d MMMM")}");
}