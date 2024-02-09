using CA2402052;
using System.Threading.Channels;

static void Beolvasas(List<Emelet> lista)
{
    try
    {
        StreamReader sr = new(@"..\..\..\src\parkolohaz.txt");
        while (!sr.EndOfStream) lista.Add(new Emelet(sr.ReadLine()));
        sr.Close();
        Console.WriteLine("Sikeres beolvasás!");
    }
    catch
    {
        Console.WriteLine("Hiba a fájl beolvasása során!");
    }
}
static void Feladat7(List<Emelet> lista)
{
    Console.WriteLine("\n7.feladat: ");
    Console.WriteLine("\tSzint neve\t1. szektor\t2. szektor\t3. szektor\t4. szektor\t5. szektor\t6. szektor");
    lista.Select((elem, index) => $"{index + 1}.szint {elem}").ToList().ForEach(x => Console.WriteLine(x));
}
static void Feladat8(List<Emelet> lista) 
{
    string neve = lista.MinBy(x => x.Szektorok.Sum()).Neve;

    Console.WriteLine($"\n8.feladat: \n\t{neve} nevű emeleten parkol a legkevesebb autó!");
}
static void Feladat9(List<Emelet> lista) 
{
    try
    {
        Console.WriteLine("\n9.feladat: ");
        lista
            .SelectMany(x => x.Szektorok, (sz, db) => new { Szektor = sz.Neve, Darab = db })
            .GroupBy(x => x.Szektor)
            .SelectMany(x => x.Select((sz,i) => new {SzektorSorszama=i+1, SzektorNeve = sz.Szektor, SzektorDB = sz.Darab}))
            .Where(x => x.SzektorDB == 0)
            .Select(x => $"{lista.IndexOf(lista.Where(e => e.Neve == x.SzektorNeve).First())+1}.emelet | {x.SzektorSorszama}.szektor")
            .ToList()
            .ForEach(x => Console.WriteLine($"\t{x}"));

    }
    catch
    {
        Console.WriteLine("\tNincs ilyen szektor!");
    }
}
static void Feladat10(List<Emelet> lista) 
{
    double atlag = Convert.ToDouble(lista.SelectMany(x => x.Szektorok).Sum()) / Convert.ToDouble(lista.SelectMany(x => x.Szektorok).Count());

    Console.WriteLine("\n10.feladat:");

    int feladat10a = lista.SelectMany(x => x.Szektorok).Count(x => x == Math.Round(atlag));
    Console.WriteLine($"\t{feladat10a} db szektorban van jelenleg átlagos mennyiségű autó!");

    int feladat10b = lista.SelectMany(x => x.Szektorok).Count(x => x < Math.Round(atlag));
    Console.WriteLine($"\t{feladat10b} db szektorban van jelenleg átlag alatti mennyiségű autó!");

    int feladat10c = lista.SelectMany(x => x.Szektorok).Count(x => x > Math.Round(atlag));
    Console.WriteLine($"\t{feladat10c} db szektorban van jelenleg átlag feletti mennyiségű autó!");

}
static void Feladat11(List<Emelet> lista)
{
    var feladat11 = lista
         .SelectMany(x => x.Szektorok, (sz, db) => new { SzektorNeve = sz.Neve, Darab = db })
         .GroupBy(x => x.SzektorNeve)
         .SelectMany(x => x.Select((sz, i) => new { SzektorSorszama = i + 1, SzektorNeve = sz.SzektorNeve, SzektorDB = sz.Darab }))
         .Where(x => x.SzektorDB == 1)
         .Select(x => $"{x.SzektorNeve}-{x.SzektorSorszama}")
         .ToList();

    Dictionary<string, string> feladat11Dic = new();

    for (int i = 0; i < feladat11.Count; i++)
    {
        var atmenetiTomb = feladat11[i].Split('-');
        feladat11Dic.Add(atmenetiTomb[0], atmenetiTomb[1]);
    }

    try
    {
        StreamWriter sw = new(@"..\..\..\src\parkolohazUj.txt");
        for (int i = 0; i < feladat11Dic.Count; i++)
        {
            sw.WriteLine($"{feladat11Dic.ElementAt(i).Key}-{string.Join("-",feladat11Dic.ElementAt(i).Value)}");
        }
        sw.Close();

    }
    catch
    {
        Console.WriteLine("Hiba a fájlba való kiírás során!");
    }
}
static void Feladat12(List<Emelet> lista) 
{
    var feladat12 = lista
        .Where(x => x.Szektorok.Sum() == lista.Max(x => x.Szektorok.Sum())).FirstOrDefault();

    Console.WriteLine("\n12.feladat: ");
    Console.WriteLine($"{(feladat12.Neve == lista[lista.Count-1].Neve ? "\tIgaz hogy a legfelső emeleten van a legtöbb autó!":$"\tNem igaz hogy a legfelső emeleten van a legtöbb autó, a legtöbb autó a {lista.IndexOf(feladat12)}. emeleten van!")}");
}
static void Feladat13(List<Emelet> lista) 
{
    var feladat13 = lista.Select((x,i) => $"{i+1}.emelet, szabadhelyek:{90 - x.Szektorok.Sum()}").ToList();
    try
    {
        StreamWriter sw = new(@"..\..\..\src\parkolohazUj.txt",append:true);
        sw.WriteLine("13.feladat:");
        feladat13.ForEach(x => sw.WriteLine(x));
        sw.Close();
    }
    catch
    {
        Console.WriteLine("Hiba a kiírás során!");
    }
}
static void Feladat14(List<Emelet> lista) 
{
    var feladat14 = lista.Select(x => x.Szektorok.Sum()).ToList().Sum();
    Console.WriteLine($"\n14.feladat: \n\t{feladat14}db szabadhely van összesen!");
}

//Main
List<Emelet> emeletek = new();
Beolvasas(emeletek);
Feladat7(emeletek);
Feladat8(emeletek);
Feladat9(emeletek);
Feladat10(emeletek);
Feladat11(emeletek);
Feladat12(emeletek);
Feladat13(emeletek);
Feladat14(emeletek);