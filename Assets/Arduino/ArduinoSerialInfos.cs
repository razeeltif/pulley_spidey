using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Networking;

public class ArduinoSerialInfos : MonoBehaviour {

    /* The serial port where the Arduino is connected. */
    [Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM6";

    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
    public int baudrate = 9600;

    private bool clockwise = true;
    public bool Clockwise => clockwise;
    private float speedRotation = 0f;
    public float SpeedRotation => speedRotation;

    private SerialPort stream;

    public void Open() {
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = 1;
        stream.Open();
//        SerialDataReceivedEventHandler(DataReceivedHandler);
    }

    void Update() {
        if (stream.IsOpen) {
            try {
                ReceivedInfo(stream.ReadByte());
                print(stream.ReadByte());
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    void ReceivedInfo(int rotation) {
        if (rotation == 1) {
            clockwise = true;
            speedRotation = 1f;
        }
        else if (rotation == -1) {
            clockwise = false;
            speedRotation = 1f;
        }
        else {
            speedRotation = 0f;
        }
    }
}