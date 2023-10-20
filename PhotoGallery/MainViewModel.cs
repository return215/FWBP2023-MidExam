using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery
{
    public class MainViewModel
    {
        public ObservableCollection<ImageMetadata> imagesCollection { get; set; } = new();
    }
}
