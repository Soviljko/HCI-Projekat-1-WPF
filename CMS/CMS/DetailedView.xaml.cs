using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace CMS
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        public DetailedView(int index)
        {
            Champion viewChampion = VisitorWindow.Champions[index];
            InitializeComponent();

            nameTextBox.Text = viewChampion.ChampionName;
            priceTextBox.Text = "Champion Price: " + Convert.ToString(viewChampion.Price) + " Blue Essence";
            dateTextBox.Text = "Date Added: " + viewChampion.Date.ToString("dd/MM/yyyy");

            Uri uri = new Uri(viewChampion.Image);
            largeImage.Source = new BitmapImage(uri);

            TextRange range;
            FileStream stream;

            if(File.Exists(viewChampion.RtfFile))
            {
                range = new TextRange(championDescriptionRichTextBox.Document.ContentStart, championDescriptionRichTextBox.Document.ContentEnd);
                using(stream = new FileStream(viewChampion.RtfFile,FileMode.OpenOrCreate))
                {
                    range.Load(stream, DataFormats.Rtf);
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
