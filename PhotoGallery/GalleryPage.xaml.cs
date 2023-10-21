using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Microsoft.Win32;

namespace PhotoGallery
{
    /// <summary>
    /// Interaction logic for GalleryPage.xaml
    /// </summary>
    public partial class GalleryPage : Page
    {
        readonly MainViewModel viewModel = new();
        readonly MainViewModel vmDataContext;
        readonly ObservableCollection<ImageMetadata> imagesCollection;
        public GalleryPage()
        {
            InitializeComponent();
            DataContext = viewModel;
            vmDataContext = (MainViewModel)DataContext;
            imagesCollection = vmDataContext.ImagesCollection;
        }

        private void BTNRemove_Click(object sender, RoutedEventArgs e)
        {
            if (LSTImageList.SelectedItems.Count > 0)
            {
                List<ImageMetadata> selectedImages = LSTImageList.SelectedItems.Cast<ImageMetadata>().ToList();
                MessageBoxResult confirmDelete = MessageBox.Show(
                    $"Delete {selectedImages.Count} images from the list?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (confirmDelete == MessageBoxResult.No)
                    return;

                foreach (var image in selectedImages)
                    imagesCollection.Remove(image);

            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                AddExtension = true,
                Filter =
                    "Image files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp;*.tif;*.tiff|" +
                    "All files|*.*",
                Multiselect = true
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (var f in dialog.FileNames)
                {
                    ImageMetadata data = new(f) { ThumbnailWidth = 320 };
                    if (imagesCollection
                        .Select((i) => i.FilePath?.AbsolutePath)
                        .Contains(f))
                    {
                        MessageBox.Show(
                            $"File already in the list:\n{f}",
                            "Duplicate files",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation
                        );
                    }
                    else
                    {
                        imagesCollection.Add(data);
                    }
                }
            }
        }

        private void LSTImageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as ImageMetadata;
            if (item == null)
                return;

            //MessageBox.Show($"Path: {item.FilePath?.LocalPath}", "Item Double-Clicked");
            NavigationService.Navigate(new ImageViewPage(item));
        }
    }
}
