using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication37
{
    /// <summary>
    /// Interaction logic for NewFolder.xaml
    /// </summary>
    public partial class NewFolder : Window
    {
        public NewFolder()
        {
            InitializeComponent();
            
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            string path = label_path.Content.ToString() + folder_name.Text.ToString();
            if (System.IO.Directory.Exists(path))
            {
                MessageBox.Show("This folder already exists. Please enter another name!");
                return;
            }
            MessageBox.Show("Folder Created!");
            System.IO.Directory.CreateDirectory(path);
            this.Close();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
