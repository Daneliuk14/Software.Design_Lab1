using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace Dan1;
class Program
{
   static void Main(string[] args)
  {
    using (var client = new WebClient())
        {
            client.DownloadFile("https://www.registrucentras.lt/aduomenys/?byla=01_gr_open_amzius_lytis_pilietybes_sav_r1.csv", "01_gr_open_amzius_lytis_pilietybes_sav_r1.csv");
        }
    List<string> needed_values = new List<string>();
    List<string> needed_values_licence = new List<string>();

    using(var reader = new StreamReader(@".\01_gr_open_amzius_lytis_pilietybes_sav_r1.csv"))
    {
      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        var values = line.Split('|');
        var without_qoutes = values[7].Trim(' ', ' ', '\t');
        var street = without_qoutes.Split('.');

        needed_values.Add(street[0]);
      }

        var counts = needed_values.GroupBy(w => w).Select(g => new {district = g.Key, Count = g.Count()}).ToList();

        List<int> number_repetitions = new List<int>();
        List<int> usable = new List<int>();

        foreach(var p in counts) 
        {
            number_repetitions.Add(p.Count);
        }

        var descendingOrder = number_repetitions.OrderByDescending(i => i);

        foreach(var p in descendingOrder) 
        {
            usable.Add(p);
        }
        for(int i = 0; i < 5; i++)
        {
            foreach(var p in counts)
            {
            
                if(p.Count == usable[i])
                {
                    Console.WriteLine(p);
                }
            }

        }

    }

  }

}