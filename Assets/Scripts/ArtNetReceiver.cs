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

    private int _listeningPort = 5999;
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

    public int ColorRed;
    public int ColorGreen;
    public int ColorBlue;

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
        udp.Client.ReceiveTimeout = 10;
        BreakThread = true;
    }

    private void ThreadMethod()
    {
        Running = true;
        udp = new UdpClient(5999);
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

                ColorRed = receiveBytes[17 + StartChannel];
                ColorGreen = receiveBytes[18 + StartChannel];
                ColorBlue = receiveBytes[19 + StartChannel];
            }
        }
        Running = false;
    }
}
