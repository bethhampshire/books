using BookAppUI.Models;
using Newtonsoft.Json;
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

namespace BookAppUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            if (_isPanelVisible == false)
            {
                Panel.Visibility = Visibility.Collapsed;
                Overlay.Visibility = Visibility.Collapsed;
            }

            authTokens = new AuthTokenModel();
            AssignAuthTokens();

        }

        private AuthTokenModel authTokens;
        private bool _isPanelVisible = false;

        private void ClosePanel_Click(object sender, RoutedEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void OpenPanel_Click(object sender, RoutedEventArgs e)
        {
            Panel.Visibility = Visibility.Visible;
            Overlay.Visibility = Visibility.Visible;
        }

        public void GetAuthTokens()
        {
            using (StreamReader r = new StreamReader("../../Tokens/AuthTokens.json"))
            {
                string json = r.ReadToEnd();
                authTokens = JsonConvert.DeserializeObject<AuthTokenModel>(json);
            }
        }

        private void AssignAuthTokens()
        {
            GetAuthTokens();
            ZFAuthToken.Text = authTokens.ziffit_token;
            WBBAuthToken.Text = authTokens.weBuyBooks_token;
        }

        private void SaveAuthTokens()
        {

        }
    }
}
