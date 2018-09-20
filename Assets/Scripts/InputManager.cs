using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    public GameObject Canvas;

    private bool isPressed;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!isPressed && !EventSystem.current.IsPointerOverGameObject())
            {
                ToggleUI();
            }
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    public void ToggleUI()
    {
        Canvas.SetActive(!Canvas.activeInHierarchy);
    }
}
