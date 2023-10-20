using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery
{
    public class ImageMetadata
    {
        public string FilePath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string> Tags { get; set; }

        public ImageMetadata(string _FilePath) {
            FilePath = _FilePath;
            Tags = new();
        }
    }
}
