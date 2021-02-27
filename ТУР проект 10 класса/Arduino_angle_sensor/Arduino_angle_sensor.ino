byte relayPin = 2; // пин реле
byte buzzerPin = 3; // пин зуммера
byte anglePin = 4; // пин датчика наклона
byte diodePin = 5; // пин светодиода
int startChange;
int finishChange;
int angleInput; //сигнал с датчика наклона
boolean openDoor = false; // открывать дверь или нет

//int Code0[] = {3000, 1000, 5000, 1000, 6000}; // кодовая последовательность пользователя 1
//int Code1[] = {3000, 1000, 5000, 1000, 6000}; // кодовая последовательность пользователя 2
//int Code2[] = {3000, 1000, 5000, 1000, 6000}; // кодовая последовательность пользователя 3
//int Code3[] = {3000, 1000, 5000, 1000, 6000}; // кодовая последовательность пользователя 4
//int Code4[] = {3000, 1000, 5000, 1000, 6000}; // кодовая последовательность пользователя 5
int codeLib[5][5] = { {6000, 1000, 6000, 1000, 6000}, {100, 1000, 5000, 1000, 1000}, {6000, 1000, 6000, 6000, 6000}, {1000, 6000, 1000, 6000, 1000}, {3000, 1000, 3000, 1000, 3000} }; // база данных
int inaccuracy = 1000; // погрешность пользователя
int code[5]; // массив для записи вводимой кодовой последовательности

void setup() {
  Serial.begin(9600);
  pinMode(relayPin, OUTPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(anglePin, INPUT);
  pinMode(diodePin, OUTPUT);
  //digitalWrite(relayPin, LOW); // включение реле
}

void loop() {
  int k = 0;
  angleInput = digitalRead(anglePin);
  if (angleInput == LOW) {
    Serial.println("Start code");
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
    }
    if (openDoor) {
      tone(buzzerPin, 1000, 1000);
      //digitalWrite(relayPin, HIGH);
    }
    else tone(buzzerPin, 700, 500);
  }
}

void RecordingCode() {
  int i = 0;
  while (i < 5) {                                 // sizeof(rightCode) / sizeof(int)
    while (angleInput == LOW) {
      angleInput = digitalRead(anglePin);
    }
    startChange = millis();
    Serial.println(startChange);
    while (angleInput != LOW) {
      angleInput = digitalRead(anglePin);
    }
    finishChange = millis();
    code[i] = finishChange - startChange;
    i++;
  }
}
