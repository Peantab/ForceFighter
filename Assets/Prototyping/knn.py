# import the necessary packages
from time import time

from sklearn.neighbors import KNeighborsClassifier
from sklearn.model_selection import train_test_split
import matplotlib.pyplot as plt
from mlxtend.plotting import plot_decision_regions

from imutils import paths
import numpy as np
import argparse
import imutils
import cv2
import os

from dataset import EyeDataset


def image_to_feature_vector(image, size=(32, 32)):
    return cv2.resize(image, size).flatten()


def histogram(image, bins=(8,)):
    hist = cv2.calcHist(image, [0], None, bins, [0, 1])
    cv2.normalize(hist, hist)
    # return the flattened histogram as the feature vector
    return hist.flatten()


imgs = EyeDataset().pictures

rawImgs = []
features = []
labels = []

preprocessing_sum = 0

for (i, (imagePath, label)) in enumerate(imgs):

    image = cv2.imread(imagePath)
    t0 = time()
    pixels = image_to_feature_vector(image)
    hist = histogram(image)
    t1 = time()
    preprocessing_sum += (t1 - t0)
    rawImgs.append(pixels)
    features.append(hist)
    labels.append(label)
    if i > 0 and i % 100 == 0:
        print("[INFO] processed {}/{}".format(i, len(imgs)))

print("Preprocessing time per img [s] =", preprocessing_sum / len(imgs))

(trainRI, testRI, trainRL, testRL) = train_test_split(
    rawImgs, labels, test_size=0.25, random_state=42)
(trainFeat, testFeat, trainLabels, testLabels) = train_test_split(
    features, labels, test_size=0.25, random_state=42)

model = KNeighborsClassifier(n_neighbors=20, n_jobs=2)
model.fit(trainRI, trainRL)
acc = model.score(testRI, testRL)

print("Raw img accuracy: {:.2f}%".format(acc * 100))

model = KNeighborsClassifier(n_neighbors=20, n_jobs=2)
model.fit(trainFeat, trainLabels)

t0 = time()
acc = model.score(testFeat, testLabels)
t1 = time()
print("Histogram prediction time per image [s] = {}".format(t1 -t0))
print("Histogram accuracy: {:.2f}%".format(acc * 100))

