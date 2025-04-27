using SkiaSharp;
using System;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Extensions;
using YoloDotNet.Models;

namespace AreaDetection.Core
{
    public class AreaCounterYolo : IDisposable
    {
        private Yolo yolo;

        public AreaCounterYolo(string modelPath)
        {
            yolo = new Yolo(new YoloOptions
            {
                OnnxModel = modelPath,
                ModelType = ModelType.Segmentation,
                Cuda = false,
            });
        }

        public DetectionResults DetectArea(byte[] imageData)
        {
            // Load the input image
            using (var image = SKImage.FromEncodedData(imageData))
            {
                // Perform detection
                var results = yolo.RunSegmentation(image);
                return new DetectionResults
                {
                    Image = image.Draw(results).Encode(SKEncodedImageFormat.Png, 70).ToArray(),
                    Count = results.Count
                };
            }
        }

        public void Dispose()
        {
            yolo?.Dispose();
        }
    }
}

