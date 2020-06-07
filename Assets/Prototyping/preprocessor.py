import os

import cv2
import dlib
import numpy
from imutils import face_utils

labels = ['LU', 'RU', 'RD', 'lD']
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')
eye_cascade = cv2.CascadeClassifier('haarcascade_eye.xml')
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor("shape_predictor_68_face_landmarks.bytes")


def get_eyes(shape, offset):
    x1, x2, y1, y2 = shape.part(36 + offset).x, shape.part(39 + offset).x, shape.part(38 + offset).y, shape.part(
        41 + offset).y
    return (x1, y1, x2 - x1, y2 - y1)


image_num = 1
frame_c = 0


def make_images(label):
    global frame_c, image_num
    os.makedirs("images/{}".format(label), exist_ok=True)
    cap = cv2.VideoCapture('videos/{}.mp4'.format(label))
    while cap.isOpened():
        frame_c = frame_c + 1
        ret, frame = cap.read()
        if ret:
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            detect = detector(gray, 1)
            for (i, rect) in enumerate(detect):
                shape = predictor(gray, rect)
                lx, ly, lw, lh = get_eyes(shape, 6)
                rx, ry, rw, rh = get_eyes(shape, 0)

                offset = 30
                left_eye = gray[ly - offset:ly + lh + offset, lx - offset:lx + lw + offset]
                right_eye = gray[ry - offset:ry + rh + offset, rx - offset:rx + rw + offset]
                ler = cv2.resize(left_eye, (100, 50))
                rer = cv2.resize(right_eye, (100, 50))
                cv2.imwrite("images/{}/{}_l.png".format(label, image_num), ler)
                cv2.imwrite("images/{}/{}_r.png".format(label, image_num), rer)
                print('[{}]({}/{})'.format(label, image_num, int(cap.get(cv2.CAP_PROP_FRAME_COUNT))))
                image_num += 1


if __name__ == '__main__':
    make_images('LD')
