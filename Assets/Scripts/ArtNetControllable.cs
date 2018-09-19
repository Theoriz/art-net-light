using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ArtNetControllable : Controllable {

    [OSCProperty]
    public string DeviceIP;

    [OSCProperty]
    public int ListeningPort;
}
