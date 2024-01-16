// MPU6050 SENSÖR VERİ OKUMA

//KÜTÜPHANELER
#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>

Adafruit_MPU6050 mpu;

void setup(void) {
  /* SERİAL HABERLEŞME İÇİN BAUD RATE HIZINI AYARLAMA */
  Serial.begin(115200);

  /* MPU SENSÖRÜNÜ İ2C ÜZERİNDEN HABERLEŞMESİNİ BAŞLATMA */
  if (!mpu.begin()) {
    while (1) {
      delay(10);
    }
  }

 /* SENSÖR AYARLARI*/
  mpu.setAccelerometerRange(MPU6050_RANGE_8_G);
  mpu.setGyroRange(MPU6050_RANGE_500_DEG);
  mpu.setFilterBandwidth(MPU6050_BAND_21_HZ);


}

void loop() {

  /* KÜTÜPHANEDEKİ KOMUTLARI KULLANILARAK SENSÖRDE OKUNAN DEĞERLERİ ELDE ETME */
  sensors_event_t a, g, temp;
  mpu.getEvent(&a, &g, &temp);

  /* VERİLERİ SERİAL ÜZERİNDEN YAZDIRMA */

    Serial.print(a.acceleration.x);
    Serial.print(",");
    Serial.print(a.acceleration.y);
    Serial.print(",");
    Serial.println(a.acceleration.z);  

  delay(50);
}
