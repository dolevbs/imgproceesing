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
    /// Interaction logic for InputDegreesDialog.xaml
    /// </summary>
    public partial class InputDegreesDialog
    {
        public InputDegreesDialog()
        {
            Degrees = 0;
            InitializeComponent();
        }

        public float Degrees { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
