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

    public override void Awake()
    {
        usePresets = false;
        base.Awake();
    }
}
