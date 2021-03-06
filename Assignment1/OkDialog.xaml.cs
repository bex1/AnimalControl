﻿using System;
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
    /// Simple one buttom ok dialog.
    /// </summary>
    public partial class OkDialog : Window
    {
        /// <summary>
        /// Initializes an dialog with info and ok button.
        /// </summary>
        /// <param name="msg">The info to show in the dialog.</param>
        public OkDialog(string msg)
        {
            InitializeComponent();

            txtblckError.Text = msg; 

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
