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

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// Error dialog for invalid input.
    /// </summary>
    public partial class InputErrorDialog : Window
    {
        /// <summary>
        /// Initializes a InputErrorDialog.
        /// </summary>
        /// <param name="errorMsg">The error message to show in the dialog.</param>
        public InputErrorDialog(string errorMsg)
        {
            InitializeComponent();

            txtblckError.Text = errorMsg;

            btnOk.Focus();
        }

        /// <summary>
        /// When ok is clicked we indicate that the dialog result is true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
