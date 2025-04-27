using AreaDetection.Core;
using System;

namespace AreaDetection.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hi there !!!");

            string outputDirectory = ".\\output\\";
            if (!System.IO.Directory.Exists(outputDirectory))
            {
                System.IO.Directory.CreateDirectory(outputDirectory);
            }


            string sampleImageDirectory = ".\\test_Images\\";
            string[] files = System.IO.Directory.GetFiles(sampleImageDirectory, "*.png");
            Parallel.ForEach(files, file =>
            {
                byte[] image = System.IO.File.ReadAllBytes(file);
                var output = AreaCounterOpenCV.DetectAreasOpenCV(image);
                System.Console.WriteLine($"File: {file} has {output.Count} number of areas detected using OpenCV Logic");
                File.WriteAllBytes(Path.Combine(outputDirectory, $"{Path.GetFileNameWithoutExtension(file)}_opencv.png"), output.Image);
            });



            System.Console.WriteLine("");
            System.Console.WriteLine("-----------------------------------------------------------");
            System.Console.WriteLine("Detecting using Yolo Logic");
            string modelPath = ".\\model\\best.onnx";

            using (var areaCounterYolo = new AreaCounterYolo(modelPath))
            {
                Parallel.ForEach(files, file =>
                {
                    byte[] image = System.IO.File.ReadAllBytes(file);
                    var output = areaCounterYolo.DetectArea(image);
                    System.Console.WriteLine($"File: {file} has {output.Count} number of areas detected using Yolo Logic");
                    File.WriteAllBytes(Path.Combine(outputDirectory, $"{Path.GetFileNameWithoutExtension(file)}_Yolos.png"), output.Image);
                });
            }
        }
    }
}
