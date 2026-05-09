using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputSystem : ITickable
{
    public bool IsSelectableButtonClicked;
    public bool IsInteractButtonClicked;

    private void Update()
    {
        IsSelectableButtonClicked = Input.GetKey(KeyCode.E);
        IsInteractButtonClicked = Input.GetKey(KeyCode.R);
    }

    public void Tick()
    {
        Update();
    }
}