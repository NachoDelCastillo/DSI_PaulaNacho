using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public CreadorMazo()
        {
            this.InitializeComponent();

            CursorHand = new CoreCursor(CoreCursorType.Hand, 0);
            CursorPin = new CoreCursor(CoreCursorType.Pin, 0);
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
        private void Go_Shop(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(Tienda), currentMoney);
        }

        private void Go_SelectScreen(object sender, RoutedEventArgs e)
        {
            string currentMoney = Dinero.Text;
            Frame.Navigate(typeof(MainPage), currentMoney);
        }

        private void Go_Settings(object sender, RoutedEventArgs e)
        {
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
    }
}
