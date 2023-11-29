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

    OpenCvSharp.Rect myFist;
    CascadeClassifier _cascadeClassifier;

    private void Start() {
        _cascadeClassifier = new CascadeClassifier();
        string cascade = Application.dataPath + "/Resources/" + "fist.xml";
        _cascadeClassifier.Load(cascade);
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output) {
        image = OpenCvSharp.Unity.TextureToMat(input);

        // Image Filtering 
        Cv2.Flip(image, image, flipMode);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);

        //  Cascade Detection
        var fist = _cascadeClassifier.DetectMultiScale(image, 1.1, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());

        // Fire Events
        if (fist.Length >= 1) {
            myFist = fist[0];
            if (myFist != null) {
                // Fire Event
                processImage.Rectangle(myFist, new Scalar(250, 0, 0), 2);
                testeY = myFist.Y;
            }
        }


        if (output == null) {
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);
        } else {
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);
        }

        return true;
    }


    //WebCamTexture _webCamTexture;
    //CascadeClassifier _cascadeClassifier;

    //[HideInInspector]
    //public float testeY;

    //private void Start() {
    //    WebCamDevice[] devices = WebCamTexture.devices;

    //    _webCamTexture = new WebCamTexture(devices[0].name);
    //    _webCamTexture.Play();
    //    _cascadeClassifier = new CascadeClassifier();
    //    string cascade = Application.dataPath + "/Resources/" + "fist.xml";
    //    _cascadeClassifier.Load(cascade);
    //}

    //private void Update() {
    //    GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    //    Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

    //    FindFace(frame);
    //}

    //void FindFace(Mat frame) {
    //    var faces = _cascadeClassifier.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

    //    if (faces.Length > 1) {
    //        Debug.Log(faces[0].Location);

    //        testeY = faces[0].Y;
    //    }
    //}
}

