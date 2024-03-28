using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CMS
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public string imagePath = "";
        public string filePath = "";
        public string temp = "";
        public int index = 0;

        public EditWindow(int tableIndex)
        {
            InitializeComponent();

            Champion changeChampion = AdminWindow.Champions[tableIndex];
            index = tableIndex;

            fontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontSizeComboBox.ItemsSource = new List<double> { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 36, 40 };
            fontSizeComboBox.SelectedIndex = 2;

            nameTextBox.Text = changeChampion.ChampionName;
            priceTextBox.Text = Convert.ToString(changeChampion.Price);
            imagePath = changeChampion.Image;
            Uri uri = new Uri(changeChampion.Image);
            previewImage.Source = new BitmapImage(uri);

            filePath = changeChampion.RtfFile;

            TextRange range;
            FileStream stream;

            if(File.Exists(filePath))
            {
                range = new TextRange(championDescriptionRichTextBox.Document.ContentStart, championDescriptionRichTextBox.Document.ContentEnd);

                using(stream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    range.Load(stream, DataFormats.Rtf);
                }
            }


        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if(validationOfUserInput())
            {
                if(temp == "")
                {
                    temp = imagePath;
                }

                AdminWindow.Champions[index] = new Champion(Int32.Parse(priceTextBox.Text), nameTextBox.Text, DateTime.Now, temp, filePath, false);

                TextRange range;
                FileStream stream;

                range = new TextRange(championDescriptionRichTextBox.Document.ContentStart, championDescriptionRichTextBox.Document.ContentEnd);
                
                using(stream = new FileStream(filePath, FileMode.Open))
                {
                    range.Save(stream, DataFormats.Rtf);
                }

                this.Close();

                MessageBox.Show("You have successfully changed your champion", "Change Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "D:\\Desktop\\Projekat HCI\\CMS\\CMS\\Images\\";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose picture";
            openFileDialog.Filter = "Pictures (*.jpg, *.png)|*.jpg;*.png";
            openFileDialog.InitialDirectory = path;

            if (openFileDialog.ShowDialog() == true)
            {
                temp = openFileDialog.FileName;
                Uri uri = new Uri(temp);
                previewImage.Source = new BitmapImage(uri);
                imageTextBox.Text = "";
                imageTextBox.BorderBrush = Brushes.Transparent;
            }
        }

        private void nameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Trim().Equals("Input champion name here"))
            {
                nameTextBox.Text = "";
                nameTextBox.Foreground = Brushes.Black;
                errorNameLabel.Foreground = Brushes.Transparent;
                errorNameLabel.BorderBrush = Brushes.Transparent;
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Trim().Equals(string.Empty))
            {
                nameTextBox.Text = "Input champion name here";
                nameTextBox.Foreground = Brushes.DarkGray;
            }
        }

        private void priceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (priceTextBox.Text.Trim().Equals("Input price here"))
            {
                priceTextBox.Text = "";
                priceTextBox.Foreground = Brushes.Black;
                errorPriceLabel.Foreground = Brushes.Transparent;
                errorPriceLabel.BorderBrush = Brushes.Transparent;
            }
        }

        private void priceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (priceTextBox.Text.Trim().Equals(string.Empty))
            {
                priceTextBox.Text = "Input price here";
                priceTextBox.Foreground = Brushes.DarkGray;
            }
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null && !championDescriptionRichTextBox.Selection.IsEmpty)
            {
                championDescriptionRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedValue != null && !championDescriptionRichTextBox.Selection.IsEmpty)
            {
                championDescriptionRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.SelectedValue);
            }
        }

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (colorPicker.SelectedColor != null && !championDescriptionRichTextBox.Selection.IsEmpty)
            {
                championDescriptionRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(colorPicker.SelectedColor.Value));
            }
        }

        private int wordCounter()
        {
            FlowDocument document = championDescriptionRichTextBox.Document;

            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;

            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);

            return words.Length;
        }

        private void championDescriptionRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numberOfWords = wordCounter();
            numberOfWordsTextBlock.Text = "Words: " + numberOfWords;
        }

        private void championDescriptionRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontStyle = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldToggleButton.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(FontWeights.Bold));

            fontStyle = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicToggleButton.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(FontStyles.Italic));

            fontStyle = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineToggleButton.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(TextDecorations.Underline));

            fontStyle = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontFamilyComboBox.SelectedItem = fontStyle;

            fontStyle = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            fontSizeComboBox.Text = fontStyle.ToString();

            object foreground = championDescriptionRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty);

            if (foreground is SolidColorBrush)
            {
                SolidColorBrush foregroundBrush = (SolidColorBrush)foreground;

                colorPicker.SelectedColor = foregroundBrush.Color;
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validationOfUserInput()
        {
            bool result = true;

            if (nameTextBox.Text.Trim().Equals("") || nameTextBox.Text.Trim().Equals("Input champion name here"))
            {
                result = false;
                nameTextBox.BorderBrush = Brushes.Red;
                nameTextBox.BorderThickness = new Thickness(2);
                errorNameLabel.Content = "You must enter name!";
                errorNameLabel.Foreground = Brushes.Red;
                errorNameLabel.BorderBrush = Brushes.Red;
                errorNameLabel.BorderThickness = new Thickness(2);
            }
            else
            {
                nameTextBox.Foreground = Brushes.Black;
                nameTextBox.BorderBrush = Brushes.Black;
                nameTextBox.BorderThickness = new Thickness(2);
                errorNameLabel.Content = "";
                errorNameLabel.Foreground = Brushes.Transparent;
            }

            if (priceTextBox.Text.Trim().Equals("") || priceTextBox.Text.Trim().Equals("Input price here"))
            {
                result = false;
                priceTextBox.BorderBrush = Brushes.Red;
                priceTextBox.BorderThickness = new Thickness(2);
                errorPriceLabel.Content = "You must enter price!";
                errorPriceLabel.Foreground = Brushes.Red;
            }
            else
            {
                bool isNumber = int.TryParse(priceTextBox.Text, out _);

                if (isNumber)
                {
                    if (Int32.Parse(priceTextBox.Text) > 0)
                    {
                        priceTextBox.Foreground = Brushes.Black;
                        priceTextBox.BorderBrush = Brushes.Black;
                        priceTextBox.BorderThickness = new Thickness(2);
                        errorPriceLabel.Content = "";
                        errorPriceLabel.Foreground = Brushes.Transparent;
                    }

                    else
                    {
                        result = false;
                        priceTextBox.BorderBrush = Brushes.Red;
                        priceTextBox.BorderThickness = new Thickness(2);
                        errorPriceLabel.Content = "You must enter positive number!";
                        errorPriceLabel.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    result = false;
                    priceTextBox.BorderBrush = Brushes.Red;
                    priceTextBox.BorderThickness = new Thickness(2);
                    errorPriceLabel.Content = "You must enter number!";
                    errorPriceLabel.Foreground = Brushes.Red;
                }
            }

            if (imageTextBox.Text.Trim().Equals("Image Preview"))
            {
                result = false;
                borderImage.BorderBrush = Brushes.Red;
                borderImage.BorderThickness = new Thickness(2);
                imageErrorLabel.Foreground = Brushes.Red;
                imageErrorLabel.Content = "You must select image!";
                errorPriceLabel.BorderBrush = Brushes.Red;
                errorPriceLabel.BorderThickness = new Thickness(2);
                imageBorder.BorderBrush = Brushes.Red;
                imageBorder.BorderThickness = new Thickness(2);
            }
            else
            {
                borderImage.BorderBrush = Brushes.Transparent;
                imageErrorLabel.Content = "";
                imageErrorLabel.Foreground = Brushes.Transparent;
                imageErrorLabel.BorderBrush = Brushes.Transparent;
                imageTextBox.Text = "";
            }

            return result;
        }
    }
}
