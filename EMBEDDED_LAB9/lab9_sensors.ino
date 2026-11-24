const int LDR_PIN = A0;
const int PIR_PIN = 2;

void setup() {
  Serial.begin(9600);
  pinMode(PIR_PIN, INPUT);
}

void loop() {
  int light = 1023 - analogRead(LDR_PIN);
  int motion = digitalRead(PIR_PIN);

  // Send light,motion to Raspberry Pi
  Serial.print(light);
  Serial.print(",");
  Serial.println(motion);

  delay(500);
}
