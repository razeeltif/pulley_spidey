using System;
using System.Collections;
using UnityEngine;
using System.IO.Ports;
using Debug = UnityEngine.Debug;

public class ArduinoSerialInfos : MonoBehaviour {
    public static ArduinoSerialInfos instanceArduino;

    /* The serial port where the Arduino is connected. */
    [Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM8";

    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
    public int baudrate = 9600;

    private bool clockwise = true;
    public bool Clockwise => clockwise;
    private float speedRotation = 0f;
    public float SpeedRotation => speedRotation;

    private SerialPort stream;

   
    private void Awake() {
        if (instanceArduino == null) {
            instanceArduino = this;
        }
    }
    
    private void Start() {
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = 1;
        stream.Open();
//        this.stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
    }

    void Update() {
        if (stream.IsOpen) {
//            Debug.Log("stream is open");
            try {
                String serialInfo = ReadFromArduino();
//                Debug.Log(serialInfo);

                int valInfo = 0;
                try {
                    valInfo = int.Parse(serialInfo);
                }
                catch (ArgumentNullException e) {
                }
                catch (FormatException e) {
                }

                ReceivedInfo(valInfo);
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
            Debug.Log("Clockwise = " + clockwise + " // SpeedRotation = " + speedRotation);
        }
        else if (rotation == -1) {
            clockwise = false;
            speedRotation = 1f;
            Debug.Log("Clockwise = " + clockwise + " // SpeedRotation = " + speedRotation);
        }
        else {
            speedRotation = 0f;
        }
    }

    public void WriteToArduino(string message)
    {
        // Send the request
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public string ReadFromArduino(int timeout = 0)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }
    

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            // A single read attempt
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield return null;
            } else
                yield return new WaitForSeconds(0.05f);

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }

    public void Close()
    {
        stream.Close();
    }
}