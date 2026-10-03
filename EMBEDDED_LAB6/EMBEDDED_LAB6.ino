#define TRIG_PIN 9
#define ECHO_PIN 10
#define PIR_PIN 3

void setup() {
  Serial.begin(9600);

  pinMode(TRIG_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);
  pinMode(PIR_PIN, INPUT);
}

void loop() {
  // Ultrasonic sensor
  digitalWrite(TRIG_PIN, LOW);
  delayMicroseconds(2);

  digitalWrite(TRIG_PIN, HIGH);
  delayMicroseconds(10);

  digitalWrite(TRIG_PIN, LOW);

  long duration = pulseIn(ECHO_PIN, HIGH);

  float distance = duration * 0.0343 / 2.0;

  // PIR sensor
  int motion = digitalRead(PIR_PIN);

  // Send data to Raspberry Pi
  Serial.print(distance);
  Serial.print(",");
  Serial.println(motion);

  delay(500);
}