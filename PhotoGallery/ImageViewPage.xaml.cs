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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoGallery
{
    /// <summary>
    /// Interaction logic for ImageViewPage.xaml
    /// </summary>
    public partial class ImageViewPage : Page
    {
        ImageMetadata imageMetadata;
        
        public ImageViewPage(ImageMetadata imageMetadata)
        {
            InitializeComponent();
            this.imageMetadata = imageMetadata;
            DataContext = this.imageMetadata;
            image.Source = new BitmapImage(this.imageMetadata.FilePath);
            
        }

        private void BTNClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            EditMetadataWindow editMetadataWindow = new(this.imageMetadata);
            editMetadataWindow.ShowDialog();
        }
    }
}
