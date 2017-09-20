using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Mosaic.UI.Controls
{
    public class MyTextBox : System.Windows.Controls.TextBox
    {
        public MyTextBox()
        {
            //AddHandler(TextChangedEvent, new RoutedEventHandler(OnMyTextChanged));
        }

        //private void OnMyTextChanged(object sender, RoutedEventArgs e)
        //{
        //    var box = (MyTextBox)sender;
        //    if (box == null) return;
        //}

        public InputMode InputMode
        {
            get { return (InputMode)GetValue(InputModeProperty); }
            set { SetValue(InputModeProperty, value); }
        }

        public static readonly DependencyProperty InputModeProperty =
            DependencyProperty.Register("InputMode", 
                typeof(InputMode), typeof(MyTextBox), 
                new PropertyMetadata(InputMode.Numeric, OnInputModePropertyChanged));

        private static void OnInputModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var box = (MyTextBox)d;
            if (box == null) return;

            var mode = (InputMode)d.GetValue(InputModeProperty);
            switch (mode)
            {
                case InputMode.Numeric:
                    box.TextChanged += (s, e) =>
                    {
                        if (box.Text.All(c => !Char.IsNumber(c))) box.Text = string.Empty;
                    };
                    break;
                default:
                    break;
            }
        }

        public string Range
        {
            get { return (string)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }

        public static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range", 
                typeof(string), typeof(MyTextBox), 
                new PropertyMetadata(string.Empty, OnRangePropertyChanged));

        private static void OnRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var box = (MyTextBox)d;
            if (box == null) return;

            var value = d.GetValue(RangeProperty);
            if (value.GetType() != typeof(string)) return;
            var rangeValue = (string)value;
            if (rangeValue == string.Empty) return;

            if (!Regex.IsMatch(rangeValue, _rangeRegex)) throw new FormatException("Range='(int)minValue (int)maxValue'");

            string minValue = string.Empty; ;
            int startIndex = 0;

            foreach (var c in rangeValue)
            {
                if (!Equals(c.ToString(), " ")) minValue += c;
                else
                {
                    startIndex = rangeValue.IndexOf(c) + 1;
                    break;
                }
            }
            int min = int.Parse(minValue);

            var length = rangeValue.Count() - startIndex;
            var maxValue = rangeValue.Substring(startIndex, length);
            int max = int.Parse(maxValue);

            if (min >= max) throw new FormatException($"{typeof(MyTextBox)}. min can't be higher or equals to max!");

            box.TextChanged += (s, e) =>
            {
                int input;
                int.TryParse(box.Text, out input);

                if (input == 1) return;

                if (input < min || input > max) box.Text = string.Empty;
            };
        }

        private static string _rangeRegex
        {
            get { return "([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-8][0-9]{4}|9[0-8][0-9]{3}|99[0-8][0-9]{2}|999[0-8][0-9]|9999[0-9]|[1-8][0-9]{5}|9[0-8][0-9]{4}|99[0-8][0-9]{3}|999[0-8][0-9]{2}|9999[0-8][0-9]|99999[0-9]|[1-8][0-9]{6}|9[0-8][0-9]{5}|99[0-8][0-9]{4}|999[0-8][0-9]{3}|9999[0-8][0-9]{2}|99999[0-8][0-9]|999999[0-9]|[1-8][0-9]{7}|9[0-8][0-9]{6}|99[0-8][0-9]{5}|999[0-8][0-9]{4}|9999[0-8][0-9]{3}|99999[0-8][0-9]{2}|999999[0-8][0-9]|9999999[0-9]|[1-8][0-9]{8}|9[0-8][0-9]{7}|99[0-8][0-9]{6}|999[0-8][0-9]{5}|9999[0-8][0-9]{4}|99999[0-8][0-9]{3}|999999[0-8][0-9]{2}|9999999[0-8][0-9]|99999999[0-9]|1[0-9]{9}|20[0-9]{8}|21[0-3][0-9]{7}|214[0-6][0-9]{6}|2147[0-3][0-9]{5}|21474[0-7][0-9]{4}|214748[0-2][0-9]{3}|2147483[0-5][0-9]{2}|21474836[0-3][0-9]|214748364[0-7])"; }
        }
    }

    public enum InputMode
    {
        Numeric,
    }
}
