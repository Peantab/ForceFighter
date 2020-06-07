# import the necessary packages
from imutils import face_utils
import numpy as np
import argparse
import imutils
import dlib
import cv2
import matplotlib.pyplot as plt

# Load an color image in grayscale
img = cv2.imread('images/LD/55_l.png')
images = [cv2.imread('images/{}/55_l.png'.format(f)) for f in ["LU", "RU", "LD", "RD"]]

gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
hsv = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
luv = cv2.cvtColor(img, cv2.COLOR_BGR2LUV)


def binary_threshold(img):
    return cv2.threshold(img, 1, 1, cv2.THRESH_BINARY)[1]


def blur(img):
    return cv2.blur(img, (3, 3))


def hough(img):
    return cv2.HoughCircles(
        img, cv2.HOUGH_GRADIENT, 1, 50, param1=100,
        param2=0.9, minRadius=0, maxRadius=0
    )


def normalize(img):
    return img / 255


def laplacian(img):
    return cv2.Laplacian(img, cv2.CV_64F)


def gaussianBlur(img):
    return cv2.GaussianBlur(img, (21, 21), 0)


def sobelGradients(img):
    return (cv2.Sobel(img, cv2.CV_64F, 1, 0, ksize=11) + cv2.Sobel(img, cv2.CV_64F, 0, 1, ksize=11))[:, :, 0] / 2


f, axarr = plt.subplots(2, 4)
for i, photo in enumerate(images):
    g = gaussianBlur(
        cv2.cvtColor(photo, cv2.COLOR_BGR2GRAY)
    )
    # circles = np.uint16(np.around(img))
    # graycp = gray
    # for k in circles[0, :]:
    #     # draw the outer circle
    #     cv2.circle(graycp, (k[0], k[1]), k[2], (0, 255, 0), 2)
    #     # draw the center of the circle
    #     cv2.circle(graycp, (k[0], k[1]), 2, (0, 0, 255), 3)
    axarr[0][i].imshow(
        cv2.cvtColor(photo, cv2.COLOR_BGR2GRAY)
        , cmap='gray'
    )
    axarr[1][i].imshow(
      g,
    cmap='gray'
)
plt.show()
