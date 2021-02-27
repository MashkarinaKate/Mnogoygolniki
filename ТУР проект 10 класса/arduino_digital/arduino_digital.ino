byte relayPin = 2; // пин реле
byte buzzerPin = 3; // пин зуммера
byte anglePin = A0;//8; // пин датчика наклона
byte diodePin = 5; // пин светодиода
long startChange;
long finishChange;
int angleInput; //сигнал с датчика наклона
boolean openDoor = false; // открывать дверь или нет
int codeLib[5][5] = { {6000, 1000, 6000, 1000, 6000}, {100, 1000, 5000, 1000, 1000}, {6000, 1000, 6000, 6000, 6000}, {1000, 6000, 1000, 6000, 1000}, {3000, 1000, 3000, 1000, 3000} }; // база данных
int inaccuracy = 1000; // погрешность пользователя
int code[5]; // массив для записи вводимой кодовой последовательности

void setup() {
  Serial.begin(9600);
  pinMode(anglePin, INPUT);
  pinMode(buzzerPin, OUTPUT);
}

void loop() {
  int k = 0;
  angleInput = analogRead(anglePin);//digitalRead(anglePin);
  Serial.println(angleInput);
  if (angleInput == HIGH) {
    RecordingCode();
    for (int i = 0; i < 5; i++) {
      for (int j = 0; j < 5; j++) {
        if (codeLib[i][j] - inaccuracy <= code[j] && code[j] <= codeLib[i][j] + inaccuracy) {
          k++;
        }
        else {
          k = 0;
          i++;
          j = 5;
        }
      }
      if (k == 5) openDoor = true;
      else openDoor = false;
    }
    if (openDoor) {
      //tone(buzzerPin, 1000, 1000);
      //digitalWrite(relayPin, HIGH);
      Serial.println("Right");
    }
    else Serial.println("False");//tone(buzzerPin, 700, 500);
  }
}

//                  ||
//                  \/
// ФОРМИРОВАНИЕ МАССИВА длительностей пауз между изменениями положения датчика наклона
void RecordingCode() {
  int n = 0;
  while (n < 5) {                                 // sizeof(rightCode) / sizeof(int)
    while (angleInput == HIGH) {
      angleInput = analogRead(anglePin);//digitalRead(anglePin);
    }
    startChange = millis();
    //Serial.println("START ");
    //Serial.println(startChange);
    while (angleInput == LOW) {
      angleInput = analogRead(anglePin);//digitalRead(anglePin);
    }
    finishChange = millis();
    code[n] = finishChange - startChange;
    //Serial.println("Code n ");
    //Serial.println(code[n]);
    Serial.println(n);
    n++;
  }
}
