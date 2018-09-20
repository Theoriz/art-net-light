using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInterface : MonoBehaviour {

    public Material QuadMaterial;
    public ArtNetReceiver Receiver;

	// Update is called once per frame
	void Update () {
        QuadMaterial.color = new Color((Receiver.ColorRed / 255.0f), (Receiver.ColorGreen / 255.0f), (Receiver.ColorBlue / 255.0f));
    }
}
