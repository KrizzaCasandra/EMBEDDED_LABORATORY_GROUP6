// Lab 10: Arduino Uno receives 1/0 from Raspberry Pi USB serial.
// D8 -> 330-ohm resistor -> LED anode; LED cathode -> GND.
// Existing PIR on D2 and LDR on A0 may remain connected.
const int LED_PIN = 8;
void setup() {
  Serial.begin(9600);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW);
}
void loop() {
  if (Serial.available()) {
    char command = Serial.read();
    if (command == '1') digitalWrite(LED_PIN, HIGH);
    else if (command == '0') digitalWrite(LED_PIN, LOW);
  }
}
