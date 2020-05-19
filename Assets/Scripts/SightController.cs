using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;
using UnityEngine.UI;

namespace OpenCvSharp
{


    public class SightController : WebCamera
    {
        public TextAsset faces;
        public TextAsset eyes;
        public TextAsset shapes;

        private ForceFighterFaceProcessor<WebCamTexture> processor;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
        }

        protected override void Awake()
        {
            base.Awake();
            base.forceFrontalCamera =
                true; // we work with frontal cams here, let's force it for macOS s MacBook doesn't state frontal cam correctly

            byte[] shapeDat = shapes.bytes;
            if (shapeDat.Length == 0)
            {
                string errorMessage =
                    "In order to have Face Landmarks working you must download special pre-trained shape predictor " +
                    "available for free via DLib library website and replace a placeholder file located at " +
                    "\"OpenCV+Unity/Assets/Resources/shape_predictor_68_face_landmarks.bytes\"\n\n" +
                    "Without shape predictor demo will only detect face rects.";

#if UNITY_EDITOR
                // query user to download the proper shape predictor
                if (UnityEditor.EditorUtility.DisplayDialog("Shape predictor data missing", errorMessage, "Download",
                    "OK, process with face rects only"))
                    Application.OpenURL("http://dlib.net/files/shape_predictor_68_face_landmarks.dat.bz2");
#else
             UnityEngine.Debug.Log(errorMessage);
#endif
            }

            processor = new ForceFighterFaceProcessor<WebCamTexture>();
            processor.Initialize(faces.text, eyes.text, shapes.bytes);

            // data stabilizer - affects face rects, face landmarks etc.
            processor.DataStabilizer.Enabled = true; // enable stabilizer
            processor.DataStabilizer.Threshold = 2.0; // threshold value in pixels
            processor.DataStabilizer.SamplesCount = 2; // how many samples do we need to compute stable data
        }


        private Rect getEyeBoundingBox(IList<Point> face, int offset, int margin = 3)
        {
            var x1 = face[36 + offset].X;
            var x2 = face[39 + offset].X;
            var y1 = face[38 + offset].Y;
            var y2 = face[42 + offset].Y;
            return new Rect(x1 - margin, y1 - margin, x2 - x1 + margin, y2 - y1 + margin);
        }

        private Point getEyePosition(Mat eye)
        {
            Point result;
            Cv2.Blur(eye, eye, new Size(5, 5));
            Cv2.Laplacian(eye, eye, MatType.CV_64F);
            Cv2.MinMaxLoc(eye,out _ , out _,  out result, out _ );
            return result;
        }

        private string getCoordsQuater(Point p)
        {
            var result = "";
            result += p.X < 0 ? "L" : "R";
            result += p.Y < 0 ? "D" : "U";
            return result;
        }
        
        
        
        protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
        {
            Point[] face = processor.ProcessTexture(input, TextureParameters);
            if (!(face == null))
            {

                Mat gray = new Mat();
                Cv2.CvtColor(processor.Image, gray, ColorConversionCodes.BGR2GRAY);

                var c = new Point(Screen.width / 2, Screen.height / 2);
                
                var leftEye = new Mat(gray, getEyeBoundingBox(face, 0)); // x,y,w,h
                var rightEye = new Mat(gray, getEyeBoundingBox(face, 6));
                
                var leftEyeCenter = new Point(leftEye.Width / 2, leftEye.Height / 2);
                var rightEyeCenter = new Point(rightEye.Width / 2, rightEye.Height / 2);

                Point leftPupilPosition = getEyePosition(leftEye);
                Point rightPupilPosition = getEyePosition(rightEye);
                
                Point leftVector = new Point(leftPupilPosition.X - leftEyeCenter.X, rightPupilPosition.Y - rightEyeCenter.Y);
                Point rightVector = new Point(rightPupilPosition.X - rightEyeCenter.X, leftPupilPosition.Y - leftEyeCenter.Y);

                var pos = 50;
                Debug.Log(getCoordsQuater(leftVector + rightVector));
                switch (getCoordsQuater(leftVector + rightVector))
                {
                    case "LU":
                        this.transform.position = new Vector3(c.X + pos, c.Y + pos, 0);
                        break;
                    case "RU":
                        this.transform.position = new Vector3(c.X - pos, c.Y + pos, 0);
                        break;
                    case "LD":
                        this.transform.position = new Vector3(c.X + pos, c.Y - pos, 0);
                        break;
                    case "RD":
                        this.transform.position = new Vector3(c.X - pos, c.Y - pos, 0);
                        break;
                    default:
                        break;
                }
                
            }

            return false;
        }
    }
}