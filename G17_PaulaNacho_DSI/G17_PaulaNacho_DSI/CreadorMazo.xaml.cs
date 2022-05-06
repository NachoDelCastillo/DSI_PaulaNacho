using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
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
    public sealed partial class CreadorMazo : Page
    {
        bool ClickPulsado = false;
        CoreCursor CursorHand = null;
        CoreCursor CursorPin = null;


        // Carta seleccionada
        ContentControl cardSelected;


        public CreadorMazo()
        {
            this.InitializeComponent();

            CursorHand = new CoreCursor(CoreCursorType.Hand, 0);
            CursorPin = new CoreCursor(CoreCursorType.Pin, 0);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateLanguageTexts();
        }


        // Botones
        private void Go_Shop(object sender, RoutedEventArgs e)
        {
            CambioPag.Play();
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(Tienda), currentMoney);
        }

        private void Go_SelectScreen(object sender, RoutedEventArgs e)
        {
            CambioPag.Play();
            Frame.Navigate(typeof(MainPage));
        }

        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            CambioPag.Play();
            Frame.Navigate(typeof(Ajustes));
        }


        // Drag and Drop
        private void Drag_Starting(UIElement sender, DragStartingEventArgs args)
        {
            ContentControl O = sender as ContentControl;
            args.Data.SetText(O.Name);
            args.Data.RequestedOperation = DataPackageOperation.Move;

            O.PointerReleased += Pointer_Released;
            O.PointerPressed += Pointer_Pressed;
        }

        private void Drag_Over(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void Drop_Item(object sender, DragEventArgs e)
        {
            var Oname = await e.DataView.GetTextAsync();
            ContentControl O = FindName(Oname.ToString()) as ContentControl;

            Point pos = e.GetPosition(MiCanvas);
            O.CanDrag = false;
            O.SetValue(Canvas.LeftProperty, pos.X - 75);
            O.SetValue(Canvas.TopProperty, pos.Y - 90);

            Window.Current.CoreWindow.PointerCursor = CursorPin;
        }

        private void Pointer_Pressed(object sender, PointerRoutedEventArgs e)
        {
            ClickPulsado = true;
            Window.Current.CoreWindow.PointerCursor = CursorHand;
        }

        private void Pointer_Released(object sender, PointerRoutedEventArgs e)
        {
            ClickPulsado = false;
            Window.Current.CoreWindow.PointerCursor = CursorPin;
        }



        // METODOS PARA CAMBIAR LAS CARTAS DE SITIO

        private async void CardDrop(object sender, DragEventArgs e)
        {
            // Encontrar la carta que se ha dropeado
            var name = await e.DataView.GetTextAsync();
            ContentControl cardDrag = FindName(name) as ContentControl;
            // Carta en la que se ha depositado una carta
            // (objeto que ha generado el evento drop)
            if (e.OriginalSource == null) return;
            ContentControl cardDrop = e.OriginalSource as ContentControl;

            // Si se ha dropeado una carta sobre si misma
            if (cardDrag == cardDrop) return;

            changeCardsPositions(cardDrop, cardDrag);
        }

        private void changeCardsPositions(ContentControl cardDrop, ContentControl cardDrag)
        {
            StackPanel mazoParent = cardDrop.Parent as StackPanel;
            StackPanel cartaParent = cardDrag.Parent as StackPanel;

            mazoParent.Children.Remove(cardDrop);
            cartaParent.Children.Remove(cardDrag);

            mazoParent.Children.Add(cardDrag);
            cartaParent.Children.Add(cardDrop);
        }

        private void CardClicked(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            StackPanel sp = button.Content as StackPanel;
            ContentControl cc = sp.Children[0] as ContentControl;

            if (cardSelected == null)
                cardSelected = cc;
            else
            {
                // Si se ha clickado sobre la misma carta otra vez
                // Ignorar el metodo
                if (cardSelected == cc)
                    return;

                // Si por el otro lado habia carta seleccionada
                // cambiarla de lugar por la que se acaba de seleccionar
                changeCardsPositions(cc, cardSelected);

                // Cuando se cambien las cartas
                // Resetear la informacion de carta seleccionada
                cardSelected = null;
            }
        }

        private void Key_Down(object sender, KeyRoutedEventArgs e)
        {
            //StackPanel sp = FocusManager.GetFocusedElement() as StackPanel;
            //ContentControl cc = sp.Children[0] as ContentControl;

            //ContentControl cc = FocusManager.GetFocusedElement() as ContentControl;

            ContentControl cc = e.OriginalSource as ContentControl;


            // Si se ha hecho click sobre un objeto que no es una carta, ignorar el input
            if (cc == null) return;

            switch (e.Key)
            {
                case VirtualKey.GamepadX:
                case VirtualKey.Enter:
                    e.Handled = true;

                    // Si no habia carta seleccionada, elegir esta
                    if (cardSelected == null)
                        cardSelected = cc;
                    else
                    {
                        // Si por el otro lado habia carta seleccionada
                        // cambiarla de lugar por la que se acaba de seleccionar
                        changeCardsPositions(cc, cardSelected);
                    }
                    break;
            }
        }

        void UpdateLanguageTexts()
        {
            if (App.idioma == 0)
            {
                CreadorMazo_Texto.Text = "CREADOR DE MAZO";
                MazoActual_Texto.Text = "Mazo Actual";
            }
            else
            {
                CreadorMazo_Texto.Text = "DECK BUILDER";
                MazoActual_Texto.Text = "Current deck";
            }
        }
    }
}
