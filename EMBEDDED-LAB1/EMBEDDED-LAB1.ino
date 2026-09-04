const int led1 = 13;
const int led2 = 12;

const int PIR_sensor = 2;
const int ldr = A0;

const int THRESHOLD = 200;

int pirValue = 0;
int ldrValue = 0;

void setup()
{
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);

  pinMode(PIR_sensor, INPUT);

  Serial.begin(9600);
}

void loop()
{
  // PIR SENSOR
  pirValue = digitalRead(PIR_sensor);

  if (pirValue == HIGH)
  {
    digitalWrite(led1, HIGH);
    Serial.println("MOTION DETECTED");
  }
  else
  {
    digitalWrite(led1, LOW);
    Serial.println("NO MOTION DETECTED");
  }

  // LDR SENSOR
  ldrValue = analogRead(ldr);

  Serial.print("LDR Value: ");
  Serial.println(ldrValue);

  // MOTION SENSED = LED2 ON and NO MOTION SENSED = LED2 OFF
  if (ldrValue > THRESHOLD)
  {
    digitalWrite(led2, LOW);
  }
  else
  {
    digitalWrite(led2, HIGH);
  }

  delay(100);
}
