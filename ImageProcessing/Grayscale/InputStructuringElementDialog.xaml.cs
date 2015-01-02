using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Grayscale.Annotations;

namespace Grayscale
{
    /// <summary>
    /// Interaction logic for InputStructuringElementDialog.xaml
    /// </summary>
    public partial class InputStructuringElementDialog
    {
        public CheckBoxCell[,] Result { get; set; }

        public InputStructuringElementDialog()
        {
            InitializeComponent();

            RangeBase_OnValueChanged(null, null);
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FilterGrid == null) return;

            FilterGrid.Children.Clear();

            FilterGrid.RowDefinitions.Clear();
            FilterGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < HeightSlider.Value; i++)
            {
                FilterGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < WidthSlider.Value; i++)
            {
                FilterGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Result = new CheckBoxCell[(int) HeightSlider.Value, (int) WidthSlider.Value];

            for (int i = 0; i < HeightSlider.Value; i++)
            {
                for (int j = 0; j < WidthSlider.Value; j++)
                {
                    var dataContext = new CheckBoxCell();
                    var cell = new RadioButton
                    {
                        Style = Resources["ToggleRadio"] as Style,
                        DataContext = dataContext
                    };

                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);

                    Result[i, j] = dataContext;
                    FilterGrid.Children.Add(cell);
                }
            }

            Result[(int) (HeightSlider.Value/2), (int) (WidthSlider.Value/2)].IsSelected = true;
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }

    public class CheckBoxCell : INotifyPropertyChanged
    {
        private bool? _isChecked;
        private bool _isSelected;

        public CheckBoxCell()
        {
            IsChecked = true;
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value.Equals(_isSelected)) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value.Equals(_isChecked)) return;
                _isChecked = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}