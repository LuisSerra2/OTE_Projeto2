using OpenCvSharp;
using OpenCvSharp.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandsGesture : WebCamera
{
    [SerializeField] private FlipMode flipMode;
    [SerializeField] private bool showProcessingImage = true;

    private Mat image;
    private Mat processImage = new Mat();

    OpenCvSharp.Rect myFist;
    CascadeClassifier _fist;
    CascadeClassifier _rPalm;
    CascadeClassifier _right;

    public Action ON_FIST_EVENT;
    public Action ON_RPALM_EVENT;
    public Action ON_RIGHT_EVENT;

    private void Start() {
        _fist = new CascadeClassifier();
        _rPalm = new CascadeClassifier();
        _right = new CascadeClassifier();
        var fist = Application.dataPath + "/Resources/" + "fist.xml";
        var rPlam = Application.dataPath + "/Resources/" + "rPalm.xml";
        var right = Application.dataPath + "/Resources/" + "right.xml";
        _fist.Load(fist);
        _rPalm.Load(rPlam);
        _right.Load(right);
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output) {
        image = OpenCvSharp.Unity.TextureToMat(input);

        // Image Filtering 
        Cv2.Flip(image, image, flipMode);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);

        //  Cascade Detection
        var fist = _fist.DetectMultiScale(image, 1.2, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var rPlam = _rPalm.DetectMultiScale(image, 1.2, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());
        var right = _rPalm.DetectMultiScale(image, 1.2, 2, HaarDetectionType.ScaleImage, new Size(10, 10), new Size());

        // Fire Events
        if (fist.Length >= 1) {
            myFist = fist[0];
            if (myFist != null) {
                // Fire Event
                ON_FIST_EVENT?.Invoke();
                processImage.Rectangle(myFist, new Scalar(250, 0, 0), 2);
            }
        }
        if (rPlam.Length >= 1) {
            myFist = rPlam[0];
            if (myFist != null) {
                // Fire Event
                ON_RPALM_EVENT?.Invoke();
                processImage.Rectangle(myFist, new Scalar(250, 0, 0), 2);
            }
        }
        if (right.Length >= 1) {
            myFist = right[0];
            if (myFist != null) {
                // Fire Event
                ON_RIGHT_EVENT?.Invoke();
                processImage.Rectangle(myFist, new Scalar(250, 0, 0), 2);
            }
        }


        if (output == null) {
            output = OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image);
        } else {
            OpenCvSharp.Unity.MatToTexture(showProcessingImage ? processImage : image, output);
        }

        return true;
    }
}
