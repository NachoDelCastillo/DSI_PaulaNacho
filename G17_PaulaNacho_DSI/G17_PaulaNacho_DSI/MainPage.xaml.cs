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
using System.Windows;

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
            UpdateLanguageTexts();
        }

        private void Go_Shop(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            CambioPag.Play();
            Frame.Navigate(typeof(Tienda), currentMoney);
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
            Frame.Navigate(typeof(Ajustes));
        }


        private void StartBattleButton_Click(object sender, RoutedEventArgs e)
        {
            CambioPag.Play();
            Frame.Navigate(typeof(Combate));
        }


        void UpdateLanguageTexts()
        {
            if (App.idioma == 0)
            {
                Historia_Texto.Text = "Historia";
                ExplicacionHistoria_Texto.Text = "Embárcate en esta aventura para salvar el reino de Oniria";
                Lucha_Texto.Text = "Lucha";
                ExplicacionLucha_Texto.Text = "¡Aguanta luchando contra los enemigos más fuertes!";
            }
            else
            {
                Historia_Texto.Text = "Story";
                ExplicacionHistoria_Texto.Text = "Embark on this adventure to save the kingdom of Oniria";
                Lucha_Texto.Text = "knockdown";
                ExplicacionLucha_Texto.Text = "Enduring fighting against the strongest enemies!";
            }
        }
    }
}
