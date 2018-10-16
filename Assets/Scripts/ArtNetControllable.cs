using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ArtNetControllable : Controllable {

    [Header("Settings")]

    [OSCProperty(isInteractible = false)]
    public string DeviceIP;

    [OSCProperty(isInteractible = false)]
    public int ListeningPort;

    [OSCProperty]
    public int StartChannel;

    [Header("Values")]

    [OSCProperty(isInteractible = false)]
    public int Red;

    [OSCProperty(isInteractible = false)]
    public int Green;

    [OSCProperty(isInteractible = false)]
    public int Blue;

    [OSCProperty]
    public bool ReceiveHVS;

    [OSCProperty(isInteractible = false)]
    public int Hue;

    [OSCProperty(isInteractible = false)]
    public int Saturation;

    [OSCProperty(isInteractible = false)]
    public int Variance;

    public override void Awake()
    {
        usePresets = false;
        base.Awake();
    }
}
