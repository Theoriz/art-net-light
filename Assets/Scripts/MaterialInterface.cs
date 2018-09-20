using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInterface : MonoBehaviour {

    public Material QuadMaterial;
    public ArtNetReceiver Receiver;

	// Update is called once per frame
	void Update () {
        QuadMaterial.color = new Color((Receiver.Red / 255.0f), (Receiver.Green / 255.0f), (Receiver.Blue / 255.0f));
    }
}
