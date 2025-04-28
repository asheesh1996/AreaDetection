# AreaDetection

AreaDetection is a .NET 8-based program for detecting and counting areas in images using OpenCV and Machine Learning using Yolov11 model trained on [test images](https://github.com/asheesh1996/AreaDetection/tree/main/AreaDetection.Console/test_Images) .

## Features

- **OpenCV**: Provides an Classis opencv approach for area detection.
- **YOLO**: Leverages the YOLO model for segmentation-based area detection.

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/your-repo/AreaDetection.git 
   cd AreaDetection
   ```


2. Ensure you have the following `.NET 8 SDK` installed.

3. Build the project:
   ```
   dotnet build
   ```


## Usage


### OpenCV Backend

For OpenCV-based detection, refer to the `AreaCounterOpenCV` class in the project.


### YOLO Backend

The `AreaCounterYolo` class is used for area detection with YOLO.


## Project Structure

- **AreaDetection.Core**: Contains the core logic for area detection, including YOLO and OpenCV implementations.
- **AreaDetection.Console**: A console application demonstrating how to use the library.

## Dependencies

- [SkiaSharp](https://github.com/mono/SkiaSharp): For image processing.
- [YoloDotNet](https://github.com/your-yolo-library): For YOLO model integration.



