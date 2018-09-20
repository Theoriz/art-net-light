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
    public int ColorRed;

    [OSCProperty(isInteractible = false)]
    public int ColorGreen;

    [OSCProperty(isInteractible = false)]
    public int ColorBlue;
}
