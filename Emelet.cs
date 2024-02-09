using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2402052
{
    internal class Emelet
    {
        public string Neve { get; set; }
        public List<int> Szektorok { get; set; }

        public Emelet(string sor)
        {
            var atmeneti = sor.Split("; ");
            this.Neve = atmeneti[0];
            Szektorok = new();

            for (int i = 1; i < atmeneti.Length; i++)
            {
                this.Szektorok.Add(Convert.ToInt32(atmeneti[i]));
            }
        }

        public override string ToString()
        {
            return $"{this.Neve,10}\t{string.Join($"{"",16}", this.Szektorok)}";
        }
    }
}
