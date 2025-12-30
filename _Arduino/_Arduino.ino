#include <Servo.h>
#include <FastLED.h>
#include <Adafruit_TiCoServo.h>
#include "Adafruit_Keypad.h"

// Button pins
const int buttonPinsA[] = {24, 44, 42, 40, 38}; // Pins for buttons that print "A"
const int buttonPinsB[] = {25, 45, 43, 41, 39}; // Pins for buttons that print "B"

const int numButtonsA = sizeof(buttonPinsA) / sizeof(buttonPinsA[0]); // Number of "A" buttons
const int numButtonsB = sizeof(buttonPinsB) / sizeof(buttonPinsB[0]); // Number of "B" buttons

int buttonStatesA[numButtonsA] = {HIGH}; // Current state of "A" buttons
int lastButtonStatesA[numButtonsA] = {HIGH}; // Last state of "A" buttons
unsigned long lastDebounceTimesA[numButtonsA] = {0}; // Debounce timers for "A" buttons

int buttonStatesB[numButtonsB] = {HIGH}; // Current state of "B" buttons
int lastButtonStatesB[numButtonsB] = {HIGH}; // Last state of "B" buttons
unsigned long lastDebounceTimesB[numButtonsB] = {0}; // Debounce timers for "B" buttons

const unsigned long debounceDelay = 50; // Debounce delay in milliseconds

// joystick pins
const int VRx[] = {A0, A2, A4, A6};
const int VRy[] = {A1, A3, A5, A7};
const int SW = 26;

// Servo
Adafruit_TiCoServo servo1, servo2, servo3, servo4;
int pos = 0;
int isUp = 0;

// Keypad
const byte ROWSx = 1, COLSx = 1, ROWSx2 = 1, COLSx2 = 1, ROWSx3 = 1, COLSx3 = 1, ROWSx4 = 1, COLSx4 = 1;
char keysx[ROWSx][COLSx] = {{'6'}};
char keysx2[ROWSx2][COLSx2] = {{'7'}};
char keysx3[ROWSx3][COLSx3] = {{'8'}};
char keysx4[ROWSx4][COLSx4] = {{'9'}};
byte rowPinsx[ROWSx] = {44}, colPinsx[COLSx] = {45};
byte rowPinsx2[ROWSx2] = {46}, colPinsx2[COLSx2] = {47};
byte rowPinsx3[ROWSx3] = {48}, colPinsx3[COLSx3] = {49};
byte rowPinsx4[ROWSx4] = {50}, colPinsx4[COLSx4] = {51};
Adafruit_Keypad customKeypadx = Adafruit_Keypad(makeKeymap(keysx), rowPinsx, colPinsx, ROWSx, COLSx);
Adafruit_Keypad customKeypadx2 = Adafruit_Keypad(makeKeymap(keysx2), rowPinsx2, colPinsx2, ROWSx2, COLSx2);
Adafruit_Keypad customKeypadx3 = Adafruit_Keypad(makeKeymap(keysx3), rowPinsx3, colPinsx3, ROWSx3, COLSx3);
Adafruit_Keypad customKeypadx4 = Adafruit_Keypad(makeKeymap(keysx4), rowPinsx4, colPinsx4, ROWSx4, COLSx4);

// LED
#define DATA_PIN 7
#define NUM_LEDS 64
#define BRIGHTNESS 5
CRGB leds[NUM_LEDS];
byte rng = 0;
byte lastRng = 0;
byte shuffleCounter = 0;
bool  shuffle = true;
int Larray[11][64] = {
  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 1, 1, 1, 1, 1, 1, 0,
  0, 1, 1, 1, 1, 1, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 1, 0, 0, 1, 1, 0,
  0, 1, 0, 1, 0, 0, 1, 0,
  0, 1, 0, 0, 1, 0, 1, 0,
  0, 1, 0, 0, 1, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 1, 0, 0, 0, 0, 1, 0,
  0, 1, 0, 0, 1, 0, 1, 0,
  0, 1, 0, 1, 0, 0, 1, 0,
  0, 0, 1, 1, 0, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 1, 1, 1, 1, 0, 0, 0,
  0, 0, 0, 1, 0, 0, 0, 0,
  0, 0, 0, 0, 1, 0, 0, 0,
  0, 1, 1, 1, 1, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 1, 1, 1, 0, 0, 1, 0,
  0, 1, 0, 0, 1, 0, 1, 0,
  0, 1, 0, 1, 0, 0, 1, 0,
  0, 0, 1, 1, 0, 0, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 1, 1, 1, 1, 0, 0,
  0, 1, 0, 0, 1, 0, 1, 0,
  0, 1, 0, 1, 0, 0, 1, 0,
  0, 0, 1, 1, 0, 0, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 1, 1, 0,
  0, 0, 0, 0, 0, 0, 1, 0,
  0, 0, 0, 0, 0, 0, 1, 0,
  0, 1, 1, 1, 1, 1, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 1, 1, 0, 1, 1, 0,
  0, 1, 0, 0, 1, 0, 0, 1,
  0, 1, 0, 0, 1, 0, 0, 1,
  0, 0, 1, 1, 0, 1, 1, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 1, 1, 0, 0,
  0, 0, 0, 1, 0, 0, 1, 0,
  0, 0, 0, 1, 0, 0, 1, 0,
  0, 1, 1, 1, 1, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  },

  {
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 1, 1, 1, 1, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  0, 0, 0, 1, 1, 0, 0, 0,
  0, 0, 1, 0, 0, 1, 0, 0,
  0, 0, 1, 0, 0, 1, 0, 0,
  0, 0, 0, 1, 1, 0, 0, 0,
  0, 0, 0, 0, 0, 0, 0, 0,
  }
};

void setup() {
  Serial.begin(9600);

  // Buttons
  for (int i = 0; i < numButtonsA; i++) {
    pinMode(buttonPinsA[i], INPUT_PULLUP);
  }

  for (int i = 0; i < numButtonsB; i++) {
    pinMode(buttonPinsB[i], INPUT_PULLUP);
  }

  // joystick
  pinMode(SW, INPUT_PULLUP);

  // Servo
  servo1.attach(11); servo2.attach(8); servo3.attach(6); servo4.attach(5);
  servo1.write(170); servo2.write(170); servo3.write(170); servo4.write(170);

  // Keypad
  customKeypadx.begin(); customKeypadx2.begin(); customKeypadx3.begin(); customKeypadx4.begin();

  // LED
  FastLED.addLeds<NEOPIXEL, DATA_PIN>(leds, NUM_LEDS);
  FastLED.setBrightness(BRIGHTNESS);
}

void loop() {
  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n');
    command.trim();

    if (command == "DOBBEL") {
      shuffle = true;
    }
  }

  // Handle "A" buttons
  for (int i = 0; i < numButtonsA; i++) {
    int reading = digitalRead(buttonPinsA[i]);

    // Check if the button state has changed
    if (reading != lastButtonStatesA[i]) {
      lastDebounceTimesA[i] = millis();
    }

    // If debounce delay has passed, update button state
    if ((millis() - lastDebounceTimesA[i]) > debounceDelay) {
      if (reading != buttonStatesA[i]) {
        buttonStatesA[i] = reading;
        if (buttonStatesA[i] == LOW) {
          Serial.println("A");
        }
      }
    }

    // Save the reading as the last button state
    lastButtonStatesA[i] = reading;
  }

  // Handle "B" buttons
  for (int i = 0; i < numButtonsB; i++) {
    int reading = digitalRead(buttonPinsB[i]);

    // Check if the button state has changed
    if (reading != lastButtonStatesB[i]) {
      lastDebounceTimesB[i] = millis();
    }

    // If debounce delay has passed, update button state
    if ((millis() - lastDebounceTimesB[i]) > debounceDelay) {
      if (reading != buttonStatesB[i]) {
        buttonStatesB[i] = reading;
        if (buttonStatesB[i] == LOW) {
          Serial.println("B");
        }
      }
    }

    // Save the reading as the last button state
    lastButtonStatesB[i] = reading;
  }

  String joystickMsg = ""; // To hold the message of the first active joystick

  for (int i = 0; i < 4; i++) { // Loop through all 4 joysticks
    int xValue = analogRead(VRx[i]);
    int yValue = analogRead(VRy[i]);

    // int xValue = analogRead(A0);
    // int yValue = analogRead(A1);

    if (xValue < 400) joystickMsg = "Left";
    else if (xValue > 600) joystickMsg = "Right";

    if (yValue < 400) joystickMsg += "Up";
    else if (yValue > 600) joystickMsg += "Down";

    // If a direction is detected, break the loop
    if (joystickMsg != "") break;
  }

  // Print the result (empty string if all joysticks are idle)
  if (joystickMsg != "") Serial.println(joystickMsg);
  else Serial.println("");

  // Keypad and LEDs
  customKeypadx.tick(); customKeypadx2.tick(); customKeypadx3.tick(); customKeypadx4.tick();
  while (customKeypadx.available() || customKeypadx2.available() || customKeypadx3.available() || customKeypadx4.available()) {
    keypadEvent z = customKeypadx.read();
    if (z.bit.EVENT == KEY_JUST_PRESSED) {
      Serial.println("D");
    }
    keypadEvent y = customKeypadx2.read();
    if (y.bit.EVENT == KEY_JUST_PRESSED) {
      Serial.println("C");
    }
    keypadEvent x = customKeypadx3.read();
    if (x.bit.EVENT == KEY_JUST_PRESSED) {
      Serial.println("E");
    }
    keypadEvent w = customKeypadx4.read();
    if (w.bit.EVENT == KEY_JUST_PRESSED) {
      Serial.println(w.bit.KEY - '0');
      RunMotor();
    }
  }

  if (shuffle) {
    Serial.println("Shuffle");
    delay(500);
    RandomLed();
    FastLED.show();
    shuffleCounter++;
    if (shuffleCounter > 3) { shuffle = false; shuffleCounter = 0; }
    else { delay(700); }
  } else {
    ShowCurrentLed();
    FastLED.show();
  }
  delay(10);
}

void RunMotor() {
  if (isUp == 0) {
    for (pos = 170; pos >= 60; pos--) {
      servo1.write(pos); servo2.write(pos); servo3.write(pos); servo4.write(pos);
      delay(20);
    }
    isUp = 1;
  } else if (isUp == 1) {
    for (pos = 60; pos <= 170; pos++) {
      servo1.write(pos); servo2.write(pos); servo3.write(pos); servo4.write(pos);
      delay(20);
    }
    isUp = 0;
  }
}

void ShowCurrentLed() {
  for (int j = 0; j < 64; j++) {
    leds[j] = (Larray[rng][j] == 1) ? CRGB::Red : CRGB::White;
  }
}

void GetRandomNumber() {
  while (true) {
    rng = random(1, 11);
    if (rng != lastRng) {
      lastRng = rng;
      break;
    }
  }
}

void RandomLed() {
  GetRandomNumber();
  for (int j = 0; j < 64; j++) {
    leds[j] = (Larray[rng][j] == 1) ? CRGB::Red : CRGB::White;
  }
}

