byte relayPin = 2; // пин реле
byte buzzerPin = 3; // пин зуммера
byte piezoPin = A0;//8; // пин датчика наклона
long start;
long finish;
int piezoInput; //сигнал с датчика
int codeLib[5][5] = { {6000, 2000, 6000, 2000, 6000}, {100, 0, 5000, 1000, 1000}, {6000, 1000, 6000, 6000, 6000}, {2000, 6000, 2000, 6000, 2000}, {3000, 1000, 3000, 1000, 3000} }; // база данных
int inaccuracy = 1000; // погрешность пользователя
int code[5]; // массив для записи вводимой кодовой последовательности

void setup() {
  Serial.begin(9600);
  pinMode(piezoPin, INPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(relayPin, OUTPUT);
  digitalWrite(relayPin, HIGH);
}

void loop() {
  int k = 0;
  int n = 0;
  byte openDoor = 0;  // открывать дверь или нет

  while (n < 5) {
    piezoInput = analogRead(piezoPin); // считываю сигнал с пьезодатчика
    while (piezoInput == 0) { // пока сигнал с пьезодатчика равен 0 (нет удара), программа ждет
      piezoInput = analogRead(piezoPin);
    } // как только ударали сигнал с пьезодатчика стал больше нуля
    while (piezoInput != 0) { // программа ждет, когда удар закончится
      piezoInput = analogRead(piezoPin);
      delay(100);
    }
    start = millis(); // записываем начало паузы
    while (piezoInput == 0) { // ждем следующего удара
      piezoInput = analogRead(piezoPin);
    }
    finish = millis(); // записываем время окончания паузы
    if (finish - start > 100) {
      Serial.println("Пауза");
      Serial.println(finish - start);
      code[n] = finish - start;
      n++;
    }
  }

  for (int i = 0; i < 5; i++) {
    for (int j = 0; j < 5; j++) {
      if (codeLib[i][j] - inaccuracy <= code[j] && code[j] <= codeLib[i][j] + inaccuracy) {
        k++;
      }
      else {
        k = 0;
      }
    }
    if (k == 5) {
      openDoor = 1;
    }
  }
  //if (k == 5) openDoor = 1;
  //else openDoor = 0;
  //for (int i = 0; i < 5; i++) {
  //if (code2[i] - inaccuracy <= code[i] && code[i] <= code2[i] + inaccuracy) {
  //k++;
  //}
  //}

  if (openDoor == 1) {
    tone(buzzerPin, 1000, 2000);
    digitalWrite(relayPin, LOW);
    delay(5000);
    digitalWrite(relayPin, HIGH);
    Serial.println("Right");
  }
  if (openDoor == 0) {
    Serial.println("False");
    tone(buzzerPin, 700, 200);
    delay(500);
    tone(buzzerPin, 700, 200);
    delay(500);
    tone(buzzerPin, 700, 200);
    digitalWrite(relayPin, HIGH);
  }
  delay(1000); // пауза для повторного ввода в случае ошибки
}
