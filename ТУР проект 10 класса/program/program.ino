byte relayPin = 2; // пин реле
byte buzzerPin = 3; // пин зуммера
byte piezoPin = A0; // пин датчика наклона
int piezoInput; //сигнал с датчика
int start;
int finish;
int i = 1;

void setup() {
  Serial.begin(9600);
  pinMode(piezoPin, INPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(relayPin, OUTPUT);
}

void loop() {
  piezoInput = analogRead(piezoPin); // считываю сигнал с пьезодатчика
  Serial.println("Значение пьезодатчика");
  Serial.println(piezoInput);
  while (piezoInput == 0) { // пока сигнал с пьезодатчика равен 0 (нет удара), программа ждет 
    piezoInput = analogRead(piezoPin);
  } // как только ударали сигнал с пьезодатчика стал больше нуля
  while (piezoInput != 0) { // программа ждет, когда удар закончится
    piezoInput = analogRead(piezoPin);
  }
  start = millis(); // записываем начало паузы
  while (piezoInput == 0) { // ждем следующего удара
    piezoInput = analogRead(piezoPin);
  }
  finish = millis(); // записываем время окончания паузы
  Serial.println("Пауза");
  Serial.println(finish - start); // выводим длительность паузы
  Serial.println("Какой по счету удар");
  Serial.println(i);
  i++;
}
