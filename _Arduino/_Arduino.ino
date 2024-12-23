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

const int lampPin = 14;
bool lampState = false;

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);
  pinMode(buttonPin2, INPUT_PULLUP);
  pinMode(buttonPin3, INPUT_PULLUP); // New button on pin 5
  pinMode(SW, INPUT_PULLUP); // Joystick button

  // lamp
  pinMode(lampPin, OUTPUT); // Set lamp pin as OUTPUT
  digitalWrite(lampPin, LOW); // Turn the lamp off initially

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
        Serial.println("C");
      }
    }
  }
  lastButtonState3 = reading3;

  // Handle joystick inputs
  int xValue = analogRead(VRx);
  int yValue = analogRead(VRy);
  String joystickMsg = "";

  if (xValue < 400) {
    joystickMsg = "Left";
  } 
  else if (xValue > 600) {
    joystickMsg = "Right";
  } else {
    joystickMsg = "";
  }

  if (yValue < 400) {
    joystickMsg += "Up";
  } 
  else if (yValue > 600) {
    joystickMsg += "Down";
  }

  if (joystickMsg != "") {
    Serial.println(joystickMsg);
  } else {
    Serial.println("");
  }

  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n');
    command.trim();

    if (command == "DOBBEL") {
      lampState = true;
      LightUp();
    }
  }
}

void LightUp() {
  digitalWrite(lampPin, lampState ? HIGH : LOW);
}
