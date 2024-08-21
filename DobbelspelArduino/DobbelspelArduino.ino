const int buttonPin1 = 2;
int buttonState1 = 0;
int lastButtonState1 = HIGH;  // previous state of the button
unsigned long lastDebounceTime1 = 0;
unsigned long debounceDelay = 50; // the debounce time; increase if the output flickers

const int buttonPin2 = 3;
int buttonState2 = 0;
int lastButtonState2 = HIGH;
unsigned long lastDebounceTime2 = 0;

const int VRx = A0; // Joystick X-axis pin
const int VRy = A1; // Joystick Y-axis pin
const int SW = 4;   // Joystick button pin

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);
  pinMode(buttonPin2, INPUT_PULLUP);
  pinMode(SW, INPUT_PULLUP); // Joystick button

  Serial.begin(9600);
}

void loop() {
  // Debounce button 1
  int reading1 = digitalRead(buttonPin1);
  if (reading1 != lastButtonState1) {
    lastDebounceTime1 = millis();
  }

  if ((millis() - lastDebounceTime1) > debounceDelay) {
    if (reading1 != buttonState1) {
      buttonState1 = reading1;
      if (buttonState1 == LOW) {
        Serial.println("A");
      }
    }
  }
  lastButtonState1 = reading1;

  // Debounce button 2
  int reading2 = digitalRead(buttonPin2);
  if (reading2 != lastButtonState2) {
    lastDebounceTime2 = millis();
  }

  if ((millis() - lastDebounceTime2) > debounceDelay) {
    if (reading2 != buttonState2) {
      buttonState2 = reading2;
      if (buttonState2 == LOW) {
        Serial.println("B");
      }
    }
  }
  lastButtonState2 = reading2;

  // Handle joystick inputs as before...
  int joyButtonState = digitalRead(SW);
  if (joyButtonState == LOW) {
    Serial.println("Joystick Button Pressed");
  }

  int xValue = analogRead(VRx);
  int yValue = analogRead(VRy);

  if (xValue < 400) {
    Serial.println("Left");
  } else if (xValue > 600) {
    Serial.println("Right");
  }

  if (yValue < 400) {
    Serial.println("Up");
  } else if (yValue > 600) {
    Serial.println("Down");
  }
}
