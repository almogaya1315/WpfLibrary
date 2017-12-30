using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TeamKits.Controls
{
    public class DsTextBox : System.Windows.Controls.TextBox
    {
        public InputType Input
        {
            get { return (InputType)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input",
                typeof(InputType), typeof(DsTextBox),
                new PropertyMetadata(InputType.All, InputPropertyChanged));

        private static void InputPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var box = (DsTextBox)d;
            if (box == null) return;

            var inputType = (InputType)d.GetValue(InputProperty);

            switch (inputType)
            {
                case InputType.All:
                    box.TextChanged += (s, e) => { };
                    break;
                case InputType.Numeric:
                    box.TextChanged += (s, e) =>
                    {
                        foreach (var c in box.Text)
                        {
                            if (!Char.IsNumber(c))
                            {
                                box.Text = string.Empty;
                                return;
                            }
                        }
                    };
                    break;
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (Text.Length > 2)
            {
                e.Handled = true;
            }

            base.OnPreviewTextInput(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {


            base.OnPreviewKeyDown(e);
        }
    }

    public enum InputType
    {
        All,
        Numeric,
    }
}
