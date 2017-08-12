using System.Windows;
using System.Windows.Controls;

namespace WpfLibrary.UI.Behaviors
{
    public class GridBehavior : DependencyObject
    {
        #region ColumnsWidth
        public static readonly DependencyProperty ColumnsWidth =
            DependencyProperty.RegisterAttached("ColumnsWidth",
                typeof(string), typeof(GridBehavior),
                new PropertyMetadata(default(string), ColumnsWidthChanged));

        public string GetColumnsWidth(DependencyObject d)
        {
            return (string)d.GetValue(ColumnsWidth);
        }

        public void SetColumnsWidth(DependencyObject d, string value)
        {
            d.SetValue(ColumnsWidth, value);
        }

        static void ColumnsWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null) return;
            var grid = (Grid)d;

            if (e.NewValue == null) return;
            var definitions = e.NewValue.ToString();

            if (string.IsNullOrEmpty(definitions)) return;
            var widths = definitions.Split(',');

            grid.ColumnDefinitions.Clear();
            foreach (var width in widths)
            {
                if (width == "Auto")
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = GridLength.Auto
                    });
                }
                else if (width.EndsWith("*"))
                {
                    var tempWidth = width.Replace("*", "");
                    if (string.IsNullOrEmpty(tempWidth)) tempWidth = "1";
                    var widthNum = double.Parse(tempWidth);
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(widthNum, GridUnitType.Star)
                    });
                }
                else
                {
                    var widthNum = double.Parse(width);
                    grid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(widthNum, GridUnitType.Pixel)
                    });
                }
            }
        }
        #endregion

        #region RowsHeight
        public static readonly DependencyProperty RowsHeight =
            DependencyProperty.RegisterAttached("RowsHeight",
                typeof(string), typeof(GridBehavior),
                new PropertyMetadata(default(string), RowsHeightChanged));

        public string GetRowsHeight(DependencyObject d)
        {
            return (string)d.GetValue(RowsHeight);
        }

        public void SetRowsHeight(DependencyObject d, string value)
        {
            d.SetValue(RowsHeight, value);
        }

        static void RowsHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null) return;
            var grid = (Grid)d;

            if (e.NewValue == null) return;
            var definitions = e.NewValue.ToString();

            if (string.IsNullOrEmpty(definitions)) return;
            var heights = definitions.Split(',');

            grid.RowDefinitions.Clear();
            foreach (var height in heights)
            {
                if (height == "Auto")
                {
                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });
                }
                else if (height.EndsWith("*"))
                {
                    var tempHeight = height.Replace("*", "");
                    if (string.IsNullOrEmpty(tempHeight)) tempHeight = "1";
                    var heightNum = double.Parse(tempHeight);
                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(heightNum, GridUnitType.Star)
                    });
                }
                else
                {
                    var heightNum = double.Parse(height);
                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(heightNum, GridUnitType.Pixel)
                    });
                }
            }
        }
        #endregion
    }
}
