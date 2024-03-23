using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS
{
    public class Hero
    {
        public int price;
        public string heroName;
        public DateTime date;
        public string image;
        public string rtfFile;

        public Hero(int p, string hn, DateTime d, string im, string rtff)
        {
            this.price = p;
            this.heroName = hn;
            this.date = d;
            this.image = im;
            this.rtfFile = rtff;
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public string HeroName
        {
            get { return heroName; }
            set { heroName = value; }
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
