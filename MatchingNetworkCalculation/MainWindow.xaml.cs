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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchingNetworkCalculation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DisplayWindow displayWindow;
        private Dictionary<string, string> imagePaths;
        // Dictionary để lưu trữ đường dẫn của các hình ảnh

        public MainWindow()
        {
            InitializeComponent();
            AddTypeComboBox();
            //AddFuncComboBox();
            displayWindow = new DisplayWindow();
        }

        private void AddTypeComboBox()
        {
            


            // Khởi tạo Dictionary và ánh xạ các mục trong TypeComboBox với đường dẫn của hình ảnh tương ứng
            imagePaths = new Dictionary<string, string>
            {
                { "L Network, DC Feed", "l_network_dc_feed.png" },
                { "L Network, DC Block", "l_network_dc_block.png" },
                { "Pi Network, DC Feed", "pi_network_dc_feed.png" },
                { "Pi Network, DC Block", "pi_network_dc_block.png" },
                { "T Network, DC Feed", "t_network_dc_feed.png" },
                { "T Network, DC Block", "t_network_dc_block.png" }
            };

            TypeComboBox.Items.Add("L Network, DC Feed");
            TypeComboBox.Items.Add("L Network, DC Block");
            TypeComboBox.Items.Add("Pi Network, DC Feed");
            TypeComboBox.Items.Add("Pi Network, DC Block");
            TypeComboBox.Items.Add("T Network, DC Feed");
            TypeComboBox.Items.Add("T Network, DC Block");

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            

        }
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        //private void AddFuncComboBox()
        //{
        //    // Create a list of items
        //    List<string> items = new List<string>
        //    {
        //        "DC Feed",
        //        "DC Block",
        //    };

        //    // Assign the list of items to the ComboBox
        //    FuncComboBox.ItemsSource = items;

        //}

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = TypeComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedItem) && imagePaths.ContainsKey(selectedItem))
            {
                string imagePath = imagePaths[selectedItem];

               
                if (displayWindow != null && !displayWindow.IsVisible)
                {
                    
                    displayWindow = new DisplayWindow();
                }

               
                displayWindow.DisplayImage(imagePath);
                
            }
            else
            {
                
            }

            double zin = int.Parse(ZinTextBox.Text);
            double rl = int.Parse(RLTextBox.Text);
            double f = int.Parse(FTextBox.Text);
            //double q = double.Parse(QTextBox.Text);
            
            Dictionary<string, double> results = new Dictionary<string, double>();
            
            double zc;
            double q;
            double q1, q2, c1, c2, l1, l2, c, l;
            
            switch (selectedItem)
            {
                case "L Network, DC Feed":
                    q = Math.Sqrt((rl / zin) - 1);
                    c = (Math.Pow(q, 2) + 1) / (Math.Pow(q, 2)) * (q / (2 * Math.PI * rl * f));
                    l = 1 / (Math.Pow((2 * Math.PI * f), 2) * c);
                    //result = c;
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance ", c);
                    results.Add("Inductance ", l);

                    break;


                case "L Network, DC Block":
                    q = Math.Sqrt((rl / zin) - 1);
                    l = (Math.Pow(q, 2)) / (Math.Pow(q, 2) + 1) * (rl / (2 * Math.PI * q * f));
                    c = 1 / (Math.Pow((2 * Math.PI * f), 2) * l);
                    //result = l;
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance ", c);
                    results.Add("Inductance ", l);

                    
                    break;


                case "Pi Network, DC Feed":
                    
                    zc = Math.Min(zin, rl) / 10; //given z center
                    
                    q1 = Math.Sqrt((zin / zc) - 1);
                    c1 = 1 / (2 * Math.PI * f * (zin / q1));
                    l1 = (zc * q1) / (2 * Math.PI * f);

                    q2 = Math.Sqrt((rl / zc) - 1);
                    c2 = 1 / (2 * Math.PI * f * (rl / q2));
                    l2 = (zc * q2) / (2 * Math.PI * f);
                    l = l1 + l2;

                    //result = l; 
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance 1 ", c1);
                    results.Add("Capacitance 2 ", c2);
                    results.Add("Inductance ", l);

                    
                    break;


                case "Pi Network, DC Block":

                    zc = Math.Min(zin, rl) / 10; //given z center

                    q1 = Math.Sqrt((zin / zc) - 1);
                    c1 = 1 / (2 * Math.PI * f * (zc * q1));
                    l1 = zin / (2 * Math.PI * f * q1);

                    q2 = Math.Sqrt((rl / zc) - 1);
                    c2 = 1 / (2 * Math.PI * f * (zc * q2));
                    l2 = rl / (2 * Math.PI * f * q2);
                    c = c1 + c2;

                    //result = c;
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance ", c);
                    results.Add("Inductance 1 = ", l1);
                    results.Add("Inductance 2 = ", l2);

                    
                    break;

                case "T Network, DC Feed":
                     
                    zc = Math.Max(zin, rl) * 10; //given z center

                    q1 = Math.Sqrt((zc / zin) - 1);
                    c1 = 1 / (2 * Math.PI * f * (zc / q1));
                    l1 = (zin * q1) / (2 * Math.PI * f);

                    q2 = Math.Sqrt((zc / rl) - 1);
                    c2 = 1 / (2 * Math.PI * f * (zc / q2));
                    l2 = (zin * q2) / (2 * Math.PI * f);
                    c = c1 + c2;

                    //result = c;
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance ", c);
                    results.Add("Inductance 1 ", l1);
                    results.Add("Inductance 2 ", l2);

                    
                    break;


                case "T Network, DC Block":
                    
                    zc = Math.Max(zin, rl) * 10; //given z center

                    q1 = Math.Sqrt((zc / zin) - 1);
                    c1 = 1 / (2 * Math.PI * f * (zin * q1));
                    l1 = zc / (2 * Math.PI * f * q1);

                    q2 = Math.Sqrt((zc / rl) - 1);
                    c2 = 1 / (2 * Math.PI * f * (rl * q2));
                    l2 = zc / (2 * Math.PI * f * q2);
                    l = l1 + l2;

                    //result = l;
                    //displayWindow.SetResult(result); break;
                    results.Add("Capacitance 1 ", c1);
                    results.Add("Capacitance 2 ", c2);
                    results.Add("Inductance ", l);

                    
                    break;


                default:
                    MessageBox.Show("Invalid, try again.");
                    break;
            }

            displayWindow.SetResults(results);
            displayWindow.Show();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        

       
    }
}
