using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for InputFilterDialog.xaml
    /// </summary>
    public partial class InputFilterDialog
    {
        public InputFilterDialog()
        {
            Divisor = 1;            
            InitializeComponent();
        }

        public System.Drawing.Point TargetCellIndex { get; set; }

        public double[,] FilterMatrix { get; set; }
        private FilterCell[,] FilterCellMatrix { get; set; }

        public double Divisor { get; set; }

        public int SelectedSize { get; private set; }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tag = e.AddedItems.OfType<ComboBoxItem>().Single().Tag.ToString();

            if (FilterGrid == null) return;

            FilterGrid.Children.Clear();

            FilterGrid.RowDefinitions.Clear();
            FilterGrid.ColumnDefinitions.Clear();

            SelectedSize = int.Parse(tag);

            for (int i = 0; i < SelectedSize; i++)
            {
                FilterGrid.RowDefinitions.Add(new RowDefinition());
                FilterGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            FilterCellMatrix = new FilterCell[SelectedSize, SelectedSize];

            for (int i = 0; i < SelectedSize; i++)
            {
                for (int j = 0; j < SelectedSize; j++)
                {
                    var dataContext = new FilterCell {Value = 1};

                    var cell = new RadioButton
                    {
                        Style = Resources["ToggleRadio"] as Style,
                        DataContext = dataContext
                    };

                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);

                    FilterCellMatrix[i, j] = dataContext;
                    FilterGrid.Children.Add(cell);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;


            FilterMatrix = new double[SelectedSize, SelectedSize];
            for (int i = 0; i < SelectedSize; i++)
            {
                for (int j = 0; j < SelectedSize; j++)
                {
                    FilterMatrix[i, j] = FilterCellMatrix[i, j].Value/Divisor;
                    if (FilterCellMatrix[i, j].IsSelected)
                    {
                        TargetCellIndex = new System.Drawing.Point(j, i);
                    }
                }

            }
        }

        internal class FilterCell
        {
            public bool IsSelected { get; set; }
            public double Value { get; set; }
        }
    }
}