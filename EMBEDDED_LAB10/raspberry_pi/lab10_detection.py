"""Lab 10 Parts B/C: camera red detection -> Arduino D8 LED.
Run: python3 lab10_detection.py --camera usb  (or csi)
Press ESC to exit.
"""
import argparse
import time
import cv2
import serial

DEFAULT_PORT = '/dev/serial/by-id/usb-Arduino_Srl_Arduino_Uno_75439333235351B040E0-if00'
p = argparse.ArgumentParser()
p.add_argument('--camera', choices=['usb', 'csi'], default='usb')
p.add_argument('--index', type=int, default=0)
p.add_argument('--port', default=DEFAULT_PORT)
p.add_argument('--threshold', type=int, default=500)
a = p.parse_args()

cam = None
pi_cam = None
ser = None
try:
    if a.camera == 'csi':
        from picamera2 import Picamera2
        pi_cam = Picamera2()
        pi_cam.configure(pi_cam.create_preview_configuration(
            main={'size': (640, 480), 'format': 'RGB888'}))
        pi_cam.start()
        time.sleep(1)
    else:
        cam = cv2.VideoCapture(a.index, cv2.CAP_V4L2)
        if not cam.isOpened():
            raise RuntimeError('USB camera not detected')
    ser = serial.Serial(a.port, 9600, timeout=1)
    time.sleep(2)  # Uno may reset when the serial port opens.
    last_command = None
    while True:
        if pi_cam:
            frame = pi_cam.capture_array()
        else:
            ok, frame = cam.read()
            if not ok:
                print('Camera read failed')
                break
        hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
        mask = cv2.bitwise_or(
            cv2.inRange(hsv, (0, 120, 70), (10, 255, 255)),
            cv2.inRange(hsv, (170, 120, 70), (179, 255, 255)))
        red_pixels = cv2.countNonZero(mask)
        detected = red_pixels > a.threshold
        command = b'1' if detected else b'0'
        if command != last_command:
            ser.write(command)
            ser.flush()
            print(('RED DETECTED / LED ON' if detected else 'NO RED / LED OFF'),
                  '| red pixels:', red_pixels)
            last_command = command
        cv2.putText(frame, 'RED DETECTED' if detected else 'NO RED',
                    (20, 40), cv2.FONT_HERSHEY_SIMPLEX, .9, (0, 255, 0), 2)
        cv2.imshow('Camera', frame)
        cv2.imshow('Red Mask', mask)
        if cv2.waitKey(1) & 0xFF == 27:
            break
except KeyboardInterrupt:
    print('Stopped')
finally:
    if ser:
        try:
            ser.write(b'0')
            ser.flush()
        except serial.SerialException:
            pass
        ser.close()
    if cam:
        cam.release()
    if pi_cam:
        pi_cam.stop()
        pi_cam.close()
    cv2.destroyAllWindows()
