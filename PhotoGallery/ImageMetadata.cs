using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoGallery
{
    public class ImageMetadata
    {
        public Uri? FilePath { get; set; }
        [JsonIgnore]
        public string? FileName
        {
            get
            {
                return Path.GetFileName(FilePath?.LocalPath);
            }
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ThumbnailWidth { get; set; } = 0;
        public List<string> Tags { get; set; } = new();
        [JsonIgnore]
        public BitmapImage ThumbnailImage { get; set; } = new();

        public ImageMetadata() {
            FilePath = null;
            GenerateThumbnail();
        }
        public ImageMetadata(string _FilePath)
            : this(new Uri(_FilePath, UriKind.Absolute))
        { }
        public ImageMetadata(Uri _FilePath) {
            FilePath = _FilePath;
            GenerateThumbnail();
        }

        public void GenerateThumbnail()
        {
            if (FilePath is null || !File.Exists(FilePath.LocalPath))
                return;

            ThumbnailImage.BeginInit();
            {
                ThumbnailImage.UriSource = FilePath;
                if (ThumbnailWidth != 0)
                    ThumbnailImage.DecodePixelWidth = ThumbnailWidth;
                ThumbnailImage.CacheOption = BitmapCacheOption.OnLoad;
            }
            ThumbnailImage.EndInit();
        }
    }

    public static class ImageMetadataManager
    {
        public static JsonSerializerOptions jsonSerializerOptions;
        static ImageMetadataManager() {
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            }; 
        }
        public static void SaveToJson(List<ImageMetadata> images, string filePath)
        {
            string jsonText = JsonSerializer.Serialize(images, jsonSerializerOptions);
            File.WriteAllText(filePath, jsonText);
            Console.WriteLine($"Saved to {Path.GetFullPath(filePath)}");
        }

        public static List<ImageMetadata> LoadFromJson(string filePath)
        {
            List<ImageMetadata>? list = null;
            // TODO fix json parsing
            //if (File.Exists(filePath))
            //{
            //    Console.WriteLine($"Loading from {Path.GetFullPath(filePath)}");
            //    string json = File.ReadAllText(filePath);
            //    list = JsonSerializer.Deserialize<List<ImageMetadata>>(json);
            //    if (list is null || list.Count > 0 && list[0].FilePath == null)
            //        throw new NullReferenceException("filePath returns null");
            //}

            Console.WriteLine($"Initialize empty list");
            list ??= new List<ImageMetadata>();
            list.ForEach(image => image.GenerateThumbnail());

            return list;
        }
    }
}
