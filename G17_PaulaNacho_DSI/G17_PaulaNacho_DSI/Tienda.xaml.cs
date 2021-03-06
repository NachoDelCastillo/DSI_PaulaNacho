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
            UpdateLanguageTexts();
        }

        // Botones
        private void Go_SelectScreen(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            CambioPag.Play();
            Frame.Navigate(typeof(MainPage), currentMoney);
        }

        private void Go_DeckCreator(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            CambioPag.Play();
            Frame.Navigate(typeof(CreadorMazo), currentMoney);
        }

        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            CambioPag.Play();
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(Ajustes), currentMoney);
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        {
            // Acceder al valor del texto
            Button buttonClicked = e.OriginalSource as Button;
            StackPanel stackPanel_0 = buttonClicked.Content as StackPanel;
            StackPanel stackPanel_1 = stackPanel_0.Children[1] as StackPanel;
            TextBlock cardPriceText = stackPanel_1.Children[0] as TextBlock;

            int value;
            // Si no se encuentra el texto detener el metodo
            if (!int.TryParse(cardPriceText.Text, out value))
                return;
            // Si el texto existe, se almacena en value

            // Restar dinero
            if (App.dinero - value >= 0)
            {
                App.dinero -= value;
                Dinero.Text = App.dinero.ToString();

                // El StackPanel que contiene este boton
                StackPanel stackParent = buttonClicked.Parent as StackPanel;

                stackParent.Children.Remove(buttonClicked);
            }
        }


        void UpdateLanguageTexts()
        {
            if (App.idioma == 0)
                Tienda_Texto.Text = "TIENDA";
            else
                Tienda_Texto.Text = "SHOP";
        }
    }
}