using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialReader : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM5";
    public int baudRate = 9600;
    Thread readThread;
    ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    bool keepReading = true;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 1000;
            serialPort.Open();

            readThread = new Thread(ReadSerialData);
            readThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void ReadSerialData()
    {
        while (keepReading)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    messageQueue.Enqueue(message);
                }
                catch (TimeoutException) {}
                catch (Exception e)
                {
                    Debug.LogError("Error reading serial port: " + e.Message);
                }
            }
        }
    }

    void Update()
    {
        while (messageQueue.TryDequeue(out string message))
        {
            ArduinoDataManager.Instance.SetButtonState(message);
        }
    }

    public void SendCommand(string command)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.WriteLine(command);
            }
            catch (Exception e)
            {
                Debug.LogError("Error writing to serial port: " + e.Message);
            }
        }
    }

    public void Dobbel()
    {
        Debug.Log("Send DOBBEL command");
        SendCommand("DOBBEL");
    }

    void OnApplicationQuit()
    {
        keepReading = false;

        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
