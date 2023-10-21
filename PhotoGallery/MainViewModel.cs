using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PhotoGallery
{
    public class MainViewModel
    {
        public const string COLLECTIONS_FILE = "collections";
        public const string COLLECTIONS_JSON = COLLECTIONS_FILE + ".json";
        public ObservableCollection<ImageMetadata> ImagesCollection { get; set; }
        public int SelectedImageIndex { get;set; }
            

        public MainViewModel()
        {
            ImagesCollection = new(ImageMetadataManager.LoadFromJson(COLLECTIONS_JSON));
            ImagesCollection.CollectionChanged += SaveToFile;
        }

        private void SaveToFile(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ImageMetadataManager.SaveToJson(ImagesCollection.ToList(), COLLECTIONS_JSON);
        }
    }
}
