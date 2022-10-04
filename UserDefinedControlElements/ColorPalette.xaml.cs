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

namespace UserDefinedControlElements
{
    /// <summary>
    /// Логика взаимодействия для ColorPalette.xaml
    /// </summary>
    public partial class ColorPalette : UserControl
    {
        public ColorPalette()
        {
            InitializeComponent();
        }

        public static DependencyProperty ColorProperty;

        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        public static readonly RoutedEvent ColorChangedEvent;

        static ColorPalette()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPalette),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPalette),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPalette),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPalette),
                 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));


            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged",
                                                                 RoutingStrategy.Bubble,
                                                                 typeof(RoutedPropertyChangedEventHandler<Color>),
                                                                 typeof(ColorPalette));
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPalette colorPalette = (ColorPalette)sender;
            colorPalette.Red = newColor.R;
            colorPalette.Green = newColor.G;
            colorPalette.Blue = newColor.B;

            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(colorPalette.Color, newColor);
            args.RoutedEvent = ColorChangedEvent;
            colorPalette.RaiseEvent(args);
        }

        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPalette ColorPalette = (ColorPalette)sender;
            Color color = ColorPalette.Color;
            if (e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
            }
            else if (e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
            }
            else if (e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
            }

            ColorPalette.Color = color;
        }


        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }



    }
}
