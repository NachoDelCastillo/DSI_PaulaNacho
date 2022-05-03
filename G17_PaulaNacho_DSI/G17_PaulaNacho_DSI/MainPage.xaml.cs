using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace G17_PaulaNacho_DSI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Cambiar el numero de monedas al actual
            string currentMoney = (string)e.Parameter;
            // En el caso en el que se inicie la aplicacion por primera vez
            if (currentMoney != null) 
            {
                if (currentMoney == "")
                    Dinero.Text = "1200";
                else
                    Dinero.Text = (string)e.Parameter;
            }
            
        }

        private void Go_Shop(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(Tienda), currentMoney);
        }

        private void Go_DeckCreator(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(CreadorMazo), currentMoney);
        }

        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ajustes));
        }


        private void StartBattleButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Combate));
        }

        private void bigButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter) 
            {
                Frame.Navigate(typeof(Combate));
            }
        }

        private void tiendaButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Frame.Navigate(typeof(Tienda));
            }
        }

        private void mazoButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Frame.Navigate(typeof(CreadorMazo));
            }
        }

        private void ajustesButton_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Frame.Navigate(typeof(Ajustes));
            }
        }
    }
}
