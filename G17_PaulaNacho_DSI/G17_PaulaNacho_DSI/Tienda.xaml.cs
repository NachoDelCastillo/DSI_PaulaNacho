using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace G17_PaulaNacho_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Tienda : Page
    {
        public Tienda()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Cambiar el numero de monedas al actual
            string currentMoney = (string)e.Parameter;
            // En el caso en el que se inicie la aplicacion por primera vez
            if (currentMoney == "")
                Dinero.Text = "1200";
            else
                Dinero.Text = (string)e.Parameter;
        }

        // Botones
        private void Go_SelectScreen(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(MainPage), currentMoney);
        }

        private void Go_DeckCreator(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(CreadorMazo), currentMoney);
        }

        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(Ajustes), currentMoney);
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        {
            // Restar dinero
            int currentMoney = int.Parse(Dinero.Text);

            if (currentMoney - 100 >= 0)
            {
                Dinero.Text = (currentMoney - 100).ToString();
            }
        }

    }
}