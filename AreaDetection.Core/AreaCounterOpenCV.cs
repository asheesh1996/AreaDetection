using OpenCvSharp;
using System.Collections.Generic;

namespace AreaDetection.Core
{
    public class AreaCounterOpenCV
    {
        public static DetectionResults DetectAreasOpenCV(byte[] image)
        {
            // Load the input image
            using (var src = Mat.FromImageData(image, ImreadModes.Color))
            {
                Mat resultImage = src.Clone();

                using (var gray = new Mat())
                {
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                    // Apply Canny edge detection
                    Mat edges = new Mat();
                    Cv2.Canny(gray, edges, 30, 100);

                    // Create a kernel for dilation (5x5 rectangular shape)
                    Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(5, 5));

                    // Dilate the edges to close small gaps
                    Mat dilated = new Mat();
                    Cv2.Dilate(edges, dilated, kernel, iterations: 2);

                    // Find contours and hierarchy from the dilated edge image
                    Point[][] contours;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(dilated, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);


                    List<Point[]> rectangles = new List<Point[]>();

                    if (hierarchy != null)
                    {
                        for (int i = 0; i < contours.Length; i++)
                        {
                            // Only consider external contours (contours with no child)
                            if (hierarchy[i].Child == -1)
                            {
                                // Approximate the contour to a polygon
                                var approx = Cv2.ApproxPolyDP(contours[i], 0.01 * Cv2.ArcLength(contours[i], true), true);

                                // Calculate area of the approximated polygon
                                double area = Cv2.ContourArea(approx);

                                // Check if it is a convex quadrilateral with sufficient area
                                if (approx.Length == 4 && Cv2.IsContourConvex(approx) && area > 1000)
                                {
                                    rectangles.Add(approx); // Store the approx polygon

                                    // Draw the rectangle on the result image
                                    Cv2.Polylines(resultImage, new[] { approx }, true, Scalar.LimeGreen, 3);
                                }
                            }
                        }
                        return new DetectionResults() { Image = resultImage.ToBytes(".png"), Count = rectangles.Count };
                    }
                    else return null;
                }
            }
        }
    
    }
}

