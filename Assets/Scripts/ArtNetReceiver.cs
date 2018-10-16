using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class ArtNetReceiver : MonoBehaviour {

    public string DeviceIP;

    private int _listeningPort = 6454;
    public int ListeningPort
    {
        get
        {
            return _listeningPort;
        }
        set
        {
            _listeningPort = value;
        }
    }

    public int StartChannel;

    
    public int Red;
    public int Green;
    public int Blue;
    private bool _receivesHSV;
    public bool ReceiveHVS
    {
        get { return _receivesHSV; }
        set
        {
            _receivesHSV = value;
            if(_receivesHSV)
            {
                Hue = Red;
                Saturation = Green;
                Variance = Blue;

                var col = Color.HSVToRGB(Red / 255.0f, Green / 255.0f, Blue / 255.0f);
                //Debug.Log("Col : " + col + " Red / 255.0f" + (Red / 255.0f));
                Red = (int)(col.r * 255.0f);
                Green = (int)(col.g * 255.0f);
                Blue = (int)(col.b * 255.0f);
            }
            else {
                Red = Hue;
                Green = Saturation;
                Blue = Variance;
                float h, s, v;
                Color.RGBToHSV(new Color(Red / 255.0f, Green / 255.0f, Blue / 255.0f), out h, out s, out v);
                Hue = (int)(h * 255.0f);
                Saturation = (int)(s * 255.0f);
                Variance = (int)(v * 255.0f);
            }
        }
    }
    public int Hue;
    public int Saturation;
    public int Variance;


    public bool Running;

    static readonly object lockObject = new object();

    private bool BreakThread;
    private UdpClient udp;
    private Thread receiverThread;

    void Start()
    {
        DeviceIP = IPManager.GetIpv4();

        Running = true;
        receiverThread = new Thread(new ThreadStart(ThreadMethod));
        receiverThread.Start();
    }

    void Update()
    {
       if(!Running)
        {
            receiverThread = new Thread(new ThreadStart(ThreadMethod));
            receiverThread.Start();
        }
    }


    private void OnApplicationQuit()
    {
        receiverThread.Abort();
        if (udp != null)
            udp.Close();

        BreakThread = true;
    }

    private void ThreadMethod()
    {
        Running = true;
        udp = new UdpClient(6454);
        //udp.Client.ReceiveTimeout = 1000; //1sec

        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

            /*lock object to make sure there data is 
            *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                if (BreakThread)
                    break;

                Red = receiveBytes[17 + StartChannel];
                Green = receiveBytes[18 + StartChannel];
                Blue = receiveBytes[19 + StartChannel];
                //Debug.Log("R : " + Red + " | G : " + Green + " | B : " + Blue);
                if (ReceiveHVS)
                {
                    Hue = Red;
                    Saturation = Green;
                    Variance = Blue;

                    var col = Color.HSVToRGB(Red / 255.0f, Green / 255.0f, Blue / 255.0f);
                    //Debug.Log("Col : " + col + " Red / 255.0f" + (Red / 255.0f));
                    Red = (int)(col.r * 255.0f);
                    Green = (int)(col.g * 255.0f);
                    Blue = (int)(col.b * 255.0f);
                }
                else
                {
                    float h, s, v;
                    Color.RGBToHSV(new Color(Red / 255.0f, Green / 255.0f, Blue / 255.0f), out h, out s, out v);
                    Hue = (int)(h * 255.0f);
                    Saturation = (int)(s * 255.0f);
                    Variance = (int)(v * 255.0f);
                }
                    
            }
        }
        Running = false;
    }
}
