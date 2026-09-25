 Lab 5 - read Arduino serial on the Pi (pyserial)
import serial, time
ser = serial.Serial('/dev/ttyACM0', 9600, timeout=1)
time.sleep(2) # allow the Arduino to reset
for _ in range(20):
line = ser.readline().decode('utf-8', 'ignore').strip()
if line:
print('Arduino:', line)
ser.close()