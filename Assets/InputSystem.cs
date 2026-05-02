using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool IsSelectableButtonClicked;
    public bool IsInteractButtonClicked;

    private void Update()
    {
        IsSelectableButtonClicked = Input.GetKey(KeyCode.E);
        IsInteractButtonClicked = Input.GetKey(KeyCode.F);
    }
}