using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    public GameObject Canvas;

    public bool IsPressed;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

#if UNITY_STANDALONE
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
        if (Input.GetMouseButton(0))
        {
            if (!IsPressed && !EventSystem.current.IsPointerOverGameObject())
            {
                ToggleUI();
            }
            IsPressed = true;
        }
        else
        {
            IsPressed = false;
        }
#endif

#if UNITY_ANDROID
        // Check if there is a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (!IsPressed && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                ToggleUI();
            }
            IsPressed = true;
        }
        else
        {
            IsPressed = false;
        }
#endif
    }

    public void ToggleUI()
    {
        UIMaster.Instance.ToggleUI();
    }
}
