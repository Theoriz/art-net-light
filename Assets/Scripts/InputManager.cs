using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject Canvas;

    public void ToggleUI()
    {
        Canvas.SetActive(!Canvas.activeInHierarchy);
    }
}
