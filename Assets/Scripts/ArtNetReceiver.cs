using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class ArtNetReceiver : MonoBehaviour {

    int colorR;
    int colorG;
    int colorB;

    public bool BreakThread, Running;

    public string DeviceIP;

    static readonly object lockObject = new object();

    private int _listeningPort;
    public int ListeningPort
    {
        get
        {
            return _listeningPort;
        }
        set
        {
            _listeningPort = value;
            Init();
        }
    }

    public Material Mat;

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
        Mat.color = new Color((colorR /255.0f), (colorG /255.0f), (colorB /255.0f));

        if(!Running)
        {
            BreakThread = false;
            receiverThread = new Thread(new ThreadStart(ThreadMethod));
            receiverThread.Start();
        }
    }


    private void OnApplicationQuit()
    {
        receiverThread.Abort();
    }

    private void Init() {
        BreakThread = true;
    }

    private void ThreadMethod()
    {
        Running = true;
        udp = new UdpClient(6002);
        Debug.Log("Port : " + ListeningPort);
        //Init();
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);

            /*lock object to make sure there data is 
            *not being accessed from multiple threads at thesame time*/
            lock (lockObject)
            {
                //if (BreakThread)
                //    break;

                colorR = receiveBytes[18];
                colorG = receiveBytes[19];
                colorB = receiveBytes[20];
            }
        }
        BreakThread = false;
        Running = false;
    }
}
