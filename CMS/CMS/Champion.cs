using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CMS
{
    [Serializable]
    public class Champion
    {
        public int Price { get; set; }
        public string ChampionName { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string RtfFile { get; set; }

        public Champion() { }

        public Champion(int p, string cn, DateTime d, string im, string rtff)
        {
            Price = p;
            ChampionName = cn;
            Date = d;
            Image = im;
            RtfFile = rtff;
        }
    }
}
