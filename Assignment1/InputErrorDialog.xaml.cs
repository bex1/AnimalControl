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

namespace Assignment3
{
    /// <summary>
    /// Error dialog for invalid input.
    /// </summary>
    public partial class InputErrorDialog : Window
    {
        public InputErrorDialog(string errorMsg)
        {
            InitializeComponent();

            txtblckError.Text = errorMsg;

            btnOk.Focus();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
