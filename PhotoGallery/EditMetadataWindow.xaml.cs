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

namespace PhotoGallery
{
    /// <summary>
    /// Interaction logic for EditMetadataWindow.xaml
    /// </summary>
    public partial class EditMetadataWindow : Window
    {
        public ImageMetadata data;
        public EditMetadataWindow(ImageMetadata data)
        {
            InitializeComponent();
            this.data = data;
            TxtFilePath.Text = this.data.FilePath?.LocalPath;
            TxtTitle.Text = this.data.Title;
            TxtDescription.Text = this.data.Description;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            data.Title = TxtTitle.Text;
            data.Description = TxtDescription.Text;
            DialogResult = true;
        }
    }
}
