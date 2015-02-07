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
using System.Windows.Shapes;

namespace Grayscale.Dialogs
{
    /// <summary>
    /// Interaction logic for InputResizeDialog.xaml
    /// </summary>
    public partial class InputResizeDialog : Window
    {
        
        public InputResizeDialog()
        {
            WidthPrecentage = 100;
            HeightPrecentage = 100;
            InitializeComponent();
        }

        public float WidthPrecentage { get; set; }
        public double HeightPrecentage { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
