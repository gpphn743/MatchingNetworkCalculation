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

namespace MatchingNetworkCalculation
{
    /// <summary>
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>

    using System.IO;

    public partial class DisplayWindow : Window
    {
        public DisplayWindow()
        {   
            InitializeComponent();
        }

        public void DisplayImage(string imagePath)
        {
            string absoluteImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            this.MyImage.Source = new BitmapImage(new Uri(absoluteImagePath));
        }

        //public void SetResult(double result)
        //{
        //    ResultTextBlock.Text = "Result: " + result.ToString();
        //}
        public void SetResults(Dictionary<string, double> results)
        {
            ResultsStackPanel.Children.Clear(); // xoa label cu 

            foreach (var pair in results)
            {
                Label resultLabel = new Label();
                resultLabel.Content = $"{pair.Key}= {pair.Value}";
                ResultsStackPanel.Children.Add(resultLabel);
            }
        }


    }
}
