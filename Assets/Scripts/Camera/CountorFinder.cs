using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

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

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
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
                // Calcular o centro
                Point center = new Point(0, 0);

                // Iteramos por todos os pontos
                foreach (var point in points) {
                    // Somamos todos os pontos ao centro
                    center.X += point.X;
                    center.Y += point.Y;
                }

                // Dividimos pelo numero de pontos para obter o centro
                center.X /= points.Length;
                center.Y /= points.Length;

                Cv2.Line(processImage, center, center, new Scalar(127, 127, 127), 3);

                testeY = center.Y;
            }
        }


        if (output == null) {
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);
        } else {
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);
        }

        return true;
    }

    private void DrawCountor(Mat image, Scalar color, int thickness, Point[] points)
    {
        for (int i = 1; i < points.Length; i++) {
            Cv2.Line(image, points[i - 1], points[i], color, thickness);
        }
        Cv2.Line(image, points[points.Length - 1], points[0], color, thickness);

    }

    //WebCamTexture _webCamTexture;
    //CascadeClassifier _cascadeClassifier;

    //private void Start()
    //{
    //    WebCamDevice[] devices = WebCamTexture.devices;

    //    _webCamTexture = new WebCamTexture(devices[0].name);
    //    _webCamTexture.Play();
    //    _cascadeClassifier = new CascadeClassifier(Application.dataPath + @"haarcascade_frontalface_default.xml");
    //}

    //private void Update()
    //{
    //    GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    //    Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

    //    FindFace(frame);
    //}

    //void FindFace(Mat frame)
    //{
    //    var faces = _cascadeClassifier.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

    //    if (faces.Length > 1) {
    //        Debug.Log(faces[0].Location);
    //    }
    //}
}

