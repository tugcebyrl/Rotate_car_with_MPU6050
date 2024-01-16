using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class CarController : MonoBehaviour
{
    // HIZ DEGISKENLERI
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 2.0f;

    // ARABA KOMPONENTININ OLUSTURULMASI
    private Rigidbody carRigidbody;

    //SERI PORT OLUÞTURMA
    SerialPort stream = new SerialPort("COM5", 115200); 

    //VERI DEGISKENLERI
    public string strReceived;
    public string[] strData = new string[3];
    public string[] strData_received = new string[3];

    //KONUM DEGISKENLERI
    public float qx, qy, qz;

    void Start()
    {
        //SERI HABERLESMENIN BASLATILMASI KISMI
        stream.Open();

        //RIGIDBODY BILESENININ OZELLIKLERININ ALINMASI
        carRigidbody = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        //VERI OKUMA KISMI
        strReceived = stream.ReadLine();  

        //GELEN VERININ AYRIÞTIRILMASI VE YENI DEGISKENLERE ATILMASI KISMI
        strData = strReceived.Split(',');

        //GELEN VERININ KONTROL EDILMESI
        if (strData[0] != "" && strData[1] != "" && strData[2] != "")
        {
            strData_received[0] = strData[0];
            strData_received[1] = strData[1];
            strData_received[2] = strData[2];
           
            //DEGISKEN DONUSUMU (STR --> FLOAT)
            qx = float.Parse(strData_received[0]);
            qy = float.Parse(strData_received[1]);
            qz = float.Parse(strData_received[2]);
            
            //KONUM VEKTORUNUN OLUSTURULMASI
            Vector3 direction = new Vector3(0, qy, 0);

            //VEKTORUN BELIRLI BIR YONDE BIRIM UZUNLUKTA HAREKET ETMESININ SAGLANMASI
            direction.Normalize(); 

            //SENSORDEN GELEN VERIYE GORE KOMPONENTIN HAREKETININ SAGLANMASI
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        }

        
        

    }
   

    
}
