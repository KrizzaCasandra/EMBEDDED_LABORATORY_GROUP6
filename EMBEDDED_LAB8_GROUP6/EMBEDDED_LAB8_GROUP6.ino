const int PIR_sensor = 2;
const int ldr = A0;

int pirValue = 0;
int ldrValue = 0;

void setup()
{
  pinMode(PIR_sensor, INPUT);

  Serial.begin(9600);
}

void loop()
{
  // PIR SENSOR
  pirValue = digitalRead(PIR_sensor);

  if (pirValue == HIGH)
  {
    Serial.println("MOTION DETECTED");
  }
  else
  {
    Serial.println("NO MOTION DETECTED");
  }

  // LDR SENSOR (1023 = light, lower value = dark/dim)
  ldrValue = analogRead(ldr);

  Serial.print("LDR Value: ");
  Serial.println(ldrValue);

  delay(200);
}