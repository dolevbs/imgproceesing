using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for InputFilterDialog.xaml
    /// </summary>
    public partial class InputFilterDialog
    {
        public InputFilterDialog()
        {
            InitializeComponent();
        }

        public double[,] FilterMatrix { get; set; }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tag = e.AddedItems.OfType<ComboBoxItem>().Single().Tag.ToString();

            var selectedSize = int.Parse(tag);

            if (FilterGrid == null) return;

            FilterGrid.Children.Clear();

            FilterGrid.RowDefinitions.Clear();
            FilterGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < selectedSize; i++)
            {
                FilterGrid.RowDefinitions.Add(new RowDefinition());
                FilterGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < selectedSize; i++)
            {
                for (int j = 0; j < selectedSize; j++)
                {
                    var cell = new RadioButton
                    {
                        Style = Resources["ToggleRadio"] as Style
                    };

                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);

                    FilterGrid.Children.Add(cell);
                }
            }
        }
    }
}