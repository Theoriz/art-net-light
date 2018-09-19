using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject Canvas;
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
            Canvas.SetActive(!Canvas.activeInHierarchy);


    }
}
