
const int LDR_PIN = A0;
const int PIR_PIN = 2;

void setup() {
  Serial.begin(9600);
  pinMode(PIR_PIN, INPUT);

  Serial.println("PIR warming up...");
  delay(1000);
  Serial.println("Ready");
}

void loop() {
  int rawLight = analogRead(LDR_PIN);   // 0 - 1023
  int light = 1023 - rawLight;          // flipped: higher = brighter
  int motion = digitalRead(PIR_PIN);    // HIGH = motion

  Serial.print("Light: ");
  Serial.print(light);
  Serial.print("  |  ");

  if (motion == HIGH) {
    Serial.println("Detected motion");
  } else {
    Serial.println("Motion not detected");
  }

  delay(500);
}
