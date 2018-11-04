/*
 * Bogdan's scanner code.
 * Known to work with this specific version of the Unistep2 library:
 * https://github.com/Gutza/Unistep2/tree/24e5ea5262c817c9a3cc612f17d4df5ee34ef862
 * 
 * Good directions for my scanner: x -> -; y -> +
 * 
 * All datagrams (incoming and outgoing) MUST be terminated with a newline (\n or \r\n; the scanner always uses \r\n in its output).
 * All valid datagrams contain at least one non-empty character; empty datagrams must be discarded
 * All valid datagrams start with a letter indicating the datagram type
 * Incoming and outgoing 
 * 
 * Incoming commands:
 * Cabc -- comment
 * M123,456 -- move relative to the current position by 123 steps on X, and 456 steps on Y
 * S -- stop moving
 * 
 * All commands MUST be terminated by a newline character.
 *
 * Output:
 * I -- information; notably, it sends "IStarted" in setup()
 * D -- debug
 * K -- OK, command accepted
 * E -- error, command not parsed correctly
 * S100,300 -- stopped after 100 steps on X and 300 steps on Y
 * Cabc -- comment, as a response to Cabc
 * Mxy -- moving at least on one direction.
 *  x and y are actually placeholders for any of the following:
 *  + -- moving positive
 *  - -- moving negative
 *  . -- stopped
 * For instance M+. means the X motor is moving positive and the Y motor is stopped.
 * 
 * Copyright October 2018
 */

// include the library
#include <Unistep2.h>

const String MESSAGE_OK = "K";
const String MESSAGE_ERROR = "E";
const String MESSAGE_MOVING = "M";
const String MESSAGE_STOPPED = "S";
const String PREFIX_REMARK = "C";
const String PREFIX_DEBUG = "D";

unsigned long stepDelay = 1200;
byte currMoveState = 0;
byte prevMoveState = 255; // Force it to output the state when it starts

long orderedX, orderedY;
long actualX, actualY;

// Define some steppers and the pins they will use
Unistep2 stepperY(9, 10, 11, 12, 4096, stepDelay); // pins for IN1, IN2, IN3, IN4, steps per rev, step delay(in micros)
Unistep2 stepperX(5, 6, 7, 8, 4096, stepDelay); // pins for IN1, IN2, IN3, IN4, steps per rev, step delay(in micros)

void setup()
{
  Serial.begin(115200);
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.println("IStarted");
}

void loop()
{
  // We need to call run() frequently during loop()
  stepperX.run();
  stepperY.run();

  currMoveState = 0;
  
  long stepsToGoX = stepperX.stepsToGo();
  if (stepsToGoX > 0) {
    currMoveState |= 1 << 0;
  } else if (stepsToGoX < 0) {
    currMoveState |= 1 << 1;
  }

  long stepsToGoY = stepperY.stepsToGo();
  if (stepsToGoY > 0) {
    currMoveState |= 1 << 2;
  } else if (stepsToGoY < 0) {
    currMoveState |= 1 << 3;
  }
  
  digitalWrite(LED_BUILTIN, currMoveState);
  if (currMoveState != prevMoveState) {
    Serial.print(PREFIX_DEBUG);
    Serial.print(stepsToGoX);
    Serial.print(",");
    Serial.println(stepsToGoY);
    PrintMoveState();

    if (!currMoveState) {
      Serial.print("S");
      if (actualX == 0 && actualY == 0) {
        Serial.print(orderedX);
        Serial.print(",");
        Serial.print(orderedY);
      } else {
        Serial.print(actualX);
        Serial.print(",");
        Serial.print(actualY);
      }
      Serial.println();
    }
    prevMoveState = currMoveState;
  }

  ExecuteSerialCommands();
}

//const int MAX_X_BACKLASH = +600;
//const int MIN_Y_BACKLASH = -960;

void PrintMoveState()
{
  Serial.print(MESSAGE_MOVING);
  
  if (currMoveState & 1 << 0) {
    Serial.print("+");
  } else if (currMoveState & 1 << 1) {
    Serial.print("-");
  } else {
    Serial.print(".");
  }
  
  if (currMoveState & 1 << 2) {
    Serial.print("+");
  } else if (currMoveState & 1 << 3) {
    Serial.print("-");
  } else {
    Serial.print(".");
  }

  Serial.println();
}

void StopMoving()
{
  actualX = orderedX - stepperX.stepsToGo();
  actualY = orderedY - stepperY.stepsToGo();
  stepperX.stop();
  stepperY.stop();
}

void MoveRelative(long x, long y)
{
  if (x == 0 && y == 0) {
    // Oh, really?
    return;
  }

  actualX = 0;
  actualY = 0;
  orderedX = x;
  orderedY = y;
  
  if (x!=0) {
    stepperX.move(x);
  }

  if (y!=0) {
    stepperY.move(y);
  }
}

void ExecuteSerialCommands()
{
  if (Serial.available() == 0) {
    return;
  }

  long x_move;
  long y_move;
  String comment = "";

  char chr = Serial.read();
  switch(chr) {
    case 'M':
      x_move = Serial.parseInt();
      y_move = Serial.parseInt();
      Serial.println(MESSAGE_OK);
      MoveRelative(x_move, y_move);
      break;
      
    case 'S':
      Serial.println(MESSAGE_OK);
      StopMoving();
      break;
/*
    case 'X':
      Serial.print(MESSAGE_OK);
      currentScanStep = 0;
      MoveRelative(-stepX - 4000, -stepY * 4 - 1000);
      MoveRelative(0, 1000);
      break;
*/

    case 'C':
      while (true) {
        char chr = Serial.read();
        if (chr == -1) {
          continue;
        }
        if (chr == 10 || chr==13) {
          break;
        }
        comment += chr;
      }
      Serial.print(PREFIX_REMARK);
      Serial.println(comment);
      break;
      
    case '\r':
    case '\n':
      // Ignore the newlines
      break;
      
    default:
      Serial.println(MESSAGE_ERROR);
      break;
  }
}

