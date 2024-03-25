using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS
{
    public class Champion
    {
        public int price;
        public string championName;
        public DateTime date;
        public string image;
        public string rtfFile;

        public Champion(int p, string cn, DateTime d, string im, string rtff)
        {
            this.price = p;
            this.championName = cn;
            this.date = d;
            this.image = im;
            this.rtfFile = rtff;
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public string ChampionName
        {
            get { return championName; }
            set { championName = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public string RtfFile
        {
            get { return rtfFile; }
            set { rtfFile = value; }
        }
    }
}
