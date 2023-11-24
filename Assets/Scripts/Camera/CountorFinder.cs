using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountorFinder : WebCamera {

    [SerializeField] private FlipMode flipMode;
    [SerializeField] private float threshold = 96.4f;
    [SerializeField] private bool showProcessingImage = true;
    [SerializeField] private float curveAccuracy = 10f;
    [SerializeField] private float minArea = 5000f;

    [HideInInspector]
    public float testeY;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierarchyIndices;

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output) {
        image = OpenCvSharp.Unity.TextureToMat(input);

        Cv2.Flip(image, image, flipMode);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        Cv2.Threshold(processImage, processImage, threshold, 255, ThresholdTypes.BinaryInv);
        Cv2.FindContours(processImage, out contours, out hierarchyIndices, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);

        foreach (Point[] contour in contours) {
            Point[] points = Cv2.ApproxPolyDP(contour, curveAccuracy, true);
            var area = Cv2.ContourArea(contour);

            if (area > minArea) {
                DrawCountor(processImage, new Scalar(127, 127, 127), 2, points);
            }
        }


        if (output == null) {
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);
        } else {
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);
        }

        return true;
    }

    private void DrawCountor(Mat image, Scalar color, int thickness, Point[] points) {
        for (int i = 1; i < points.Length; i++) {
            Cv2.Circle(image, points[i - 1].X, points[i].Y * 2, 100, color, thickness);
        }

            testeY = points[0].Y * 2;
    }
}

