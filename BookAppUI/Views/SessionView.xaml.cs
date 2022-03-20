using BookAppUI.Service;
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

namespace BookAppUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SessionView.xaml
    /// </summary>
    public partial class SessionView : UserControl
    {
        public SessionView()
        {
            InitializeComponent();
        }
        private string barcode = "9780099448822";
        decimal ziffitPrice = 0;
        decimal musicMagpiePrice = 0;
        decimal sellItBackPrice = 0;

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            new ZiffitService().GetPrice(barcode).ContinueWith((task) =>
            {
                ziffitPrice = task.Result.Price;
            });
            new MusicMagpieService().GetPrice(barcode).ContinueWith((task) =>
            {
                musicMagpiePrice = task.Result.Price;
            });
            new SellItBackService().GetPrice(barcode).ContinueWith((task) =>
            {
                sellItBackPrice = task.Result.Price;
            });
            Console.WriteLine(ziffitPrice);
            Console.WriteLine(musicMagpiePrice);
            Console.WriteLine(sellItBackPrice);
        }
    }
}
