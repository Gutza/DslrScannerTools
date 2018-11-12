/*
 * Bogdan's scanner code.
 * Known to work with this specific version of the Unistep2 library:
 * https://github.com/Gutza/Unistep2/commit/a1c73690c265072f091d4edd90e655ad77975168
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
 * P1200 -- how many microseconds to wait between two steps. The command is ignored if less than MIN_STEP_DELAY.
 * 
 * All commands MUST be terminated by a newline character.
 *
 * Output:
 * I -- information; notably, it sends "IStarted" in setup()
 * D -- debug
 * K -- OK, command accepted
 * E -- error, command not parsed correctly
 * P100,100 -- current X,Y position in steps, relative to the last Arduino reset
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
const String PREFIX_MOVING = "M";
const String PREFIX_REMARK = "C";
const String PREFIX_DEBUG = "D";
const String PREFIX_POSITION = "P";
const unsigned long POSITION_DUMP_FREQUENCY_MILLIS = 500;
unsigned long lastPositionDumpMillis;

const unsigned long MIN_STEP_DELAY = 1200;
byte currMoveState = 0;
byte prevMoveState = 255; // Force it to output the state when it starts

// Define some steppers and the pins they will use
Unistep2 stepperY(9, 10, 11, 12, 4096, MIN_STEP_DELAY); // pins for IN1, IN2, IN3, IN4, steps per rev, step delay(in micros)
Unistep2 stepperX(5, 6, 7, 8, 4096, MIN_STEP_DELAY); // pins for IN1, IN2, IN3, IN4, steps per rev, step delay(in micros)

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
    PrintPosition();
    PrintMoveState();
    prevMoveState = currMoveState;
  }

  if (currMoveState) {
    HandlePositionDump();
  } else {
    lastPositionDumpMillis = 0;
  }

  ExecuteSerialCommands();
}

void HandlePositionDump()
{
  if (lastPositionDumpMillis == 0) {
    lastPositionDumpMillis = millis();
    return;
  }

  long currMillis = millis();
  if (currMillis - lastPositionDumpMillis < POSITION_DUMP_FREQUENCY_MILLIS) {
    return;
  }

  lastPositionDumpMillis = currMillis;
  PrintPosition();
}

void PrintPosition()
{
  Serial.print(PREFIX_POSITION);
  Serial.print(stepperX.currentPosition());
  Serial.print(",");
  Serial.println(stepperY.currentPosition());
}

//const int MAX_X_BACKLASH = +600;
//const int MIN_Y_BACKLASH = -960;

void PrintMoveState()
{
  Serial.print(PREFIX_MOVING);
  
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
  stepperX.stop();
  stepperY.stop();
}

void MoveRelative(long x, long y)
{
  if (x == 0 && y == 0) {
    // Oh, really?
    return;
  }

  if (currMoveState) {
    PrintPosition();
  }
  
  if (x) {
    stepperX.move(x);
  } else {
    stepperX.stop();
  }

  if (y) {
    stepperY.move(y);
  } else {
    stepperY.stop();
  }
}

void ExecuteSerialCommands()
{
  if (Serial.available() == 0) {
    return;
  }

  String comment = "";
  long tmp1, tmp2;

  char chr = Serial.read();
  switch(chr) {
    case 'M':
      tmp1 = Serial.parseInt();
      tmp2 = Serial.parseInt();
      Serial.println(MESSAGE_OK);
      MoveRelative(tmp1, tmp2);
      break;
      
    case 'S':
      Serial.println(MESSAGE_OK);
      StopMoving();
      break;

    case 'C':
      while (true) {
        chr = Serial.read();
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
      
    case 'P':
      tmp1 = Serial.parseInt();
      if (tmp1 < MIN_STEP_DELAY) {
        Serial.println(MESSAGE_ERROR);
        break;
      }

      Serial.println(MESSAGE_OK);
      stepperX.setDelay(tmp1);
      stepperY.setDelay(tmp1);
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

