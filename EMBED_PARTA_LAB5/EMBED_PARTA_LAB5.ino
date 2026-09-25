# Lab 5 - GPIO blink test (gpiozero)
from gpiozero import LED
from time import sleep
led = LED(17) # BCM pin 17
for _ in range(10):
led.on(); sleep(0.5)
led.off(); sleep(0.5)