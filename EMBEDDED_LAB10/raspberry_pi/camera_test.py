"""Lab 10 Part A: python3 camera_test.py --camera usb [--index 0] (or csi)."""
import argparse
import time
import cv2

p = argparse.ArgumentParser()
p.add_argument('--camera', choices=['usb', 'csi'], default='usb')
p.add_argument('--index', type=int, default=0)
a = p.parse_args()
if a.camera == 'csi':
    from picamera2 import Picamera2
    cam = Picamera2()
    cam.configure(cam.create_still_configuration(main={'size': (640, 480), 'format': 'RGB888'}))
    cam.start()
    try:
        time.sleep(2)
        frame = cam.capture_array()
    finally:
        cam.stop()
        cam.close()
else:
    cam = cv2.VideoCapture(a.index, cv2.CAP_V4L2)
    try:
        if not cam.isOpened():
            raise RuntimeError('USB webcam not detected')
        ok, frame = cam.read()
        if not ok:
            raise RuntimeError('Failed to capture webcam frame')
    finally:
        cam.release()
if not cv2.imwrite('test_image.jpg', frame):
    raise RuntimeError('Could not save test_image.jpg')
print('Camera test successful: test_image.jpg')
