using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CMS
{
    [Serializable]
    
    public class Champion : INotifyPropertyChanged
    {
        public int Price { get; set; }
        public string ChampionName { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string RtfFile { get; set; }

        public bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Champion() { }

        public Champion(int p, string cn, DateTime d, string im, string rtff, bool ic)
        {
            Price = p;
            ChampionName = cn;
            Date = d;
            Image = im;
            RtfFile = rtff;
            IsChecked = ic;
        }

        
    }
}
