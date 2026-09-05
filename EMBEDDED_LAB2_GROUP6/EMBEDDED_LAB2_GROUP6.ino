const int LDR = A0; // analog sensor
const int PIR = 2; // digital sensor

void setup() {
	Serial.begin(115200);
	pinMode(PIR, INPUT);
}

void loop() {
	int light = analogRead(LDR); // 0..1023
	int motion = digitalRead(PIR); // 0 or 1
		Serial.print(light);
		Serial.print(",");
		Serial.println(motion); // newline ends the record
  
delay(200); // ~5 samples/second
}
