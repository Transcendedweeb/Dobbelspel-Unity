const int buttonPin1 = 2;
int buttonState1 = 0;
int lastButtonState1 = HIGH;  // previous state of button 1
unsigned long lastDebounceTime1 = 0;
unsigned long debounceDelay = 50; // debounce time

const int buttonPin2 = 3;
int buttonState2 = 0;
int lastButtonState2 = HIGH;  // previous state of button 2
unsigned long lastDebounceTime2 = 0;

const int buttonPin3 = 5;     // New button on pin 5
int buttonState3 = 0;
int lastButtonState3 = HIGH;  // previous state of button 3
unsigned long lastDebounceTime3 = 0;

const int VRx = A0; // Joystick X-axis pin
const int VRy = A1; // Joystick Y-axis pin
const int SW = 4;   // Joystick button pin

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);
  pinMode(buttonPin2, INPUT_PULLUP);
  pinMode(buttonPin3, INPUT_PULLUP); // New button on pin 5
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

  // Debounce button 3 (new button)
  int reading3 = digitalRead(buttonPin3);
  if (reading3 != lastButtonState3) {
    lastDebounceTime3 = millis();
  }

  if ((millis() - lastDebounceTime3) > debounceDelay) {
    if (reading3 != buttonState3) {
      buttonState3 = reading3;
      if (buttonState3 == LOW) {
        Serial.println("E");
      }
    }
  }
  lastButtonState3 = reading3;

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
