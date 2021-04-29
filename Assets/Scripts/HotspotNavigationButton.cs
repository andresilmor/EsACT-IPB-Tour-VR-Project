using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class HotspotNavigationButton : MonoBehaviour
{
    [Header("Hotspot Navigation Button Config:")]
    [Tooltip("Input Action returning float.")]
    [SerializeField] InputActionReference leftInputAction;
    [Tooltip("Input Action returning float.")]
    [SerializeField] InputActionReference rightInputAction;
    [SerializeField] [Range(0f, 1f)] float inputThreshold = 0.9f;
    [SerializeField] Hotspot hotspot;
    [SerializeField] Material nextLocation;
    [SerializeField] GameObject nextLocationNavigationButtonsParent;

    private bool isNavigating = false;
    private bool isExiting = false;


    public void SetInputActionNavigate() {
        RemoveAllInputAction();
        Debug.Log("SetInputAction");
        if (leftInputAction) {
            leftInputAction.action.performed += Navigate;
            isNavigating = true;

        }

        if (rightInputAction) {
            rightInputAction.action.performed += Navigate;
            isNavigating = true;

        }


    }

    public void SetInputActionExit() {
        RemoveAllInputAction();

        if (leftInputAction) {
            leftInputAction.action.performed += Exit;
            isExiting = true;

        }

        if (rightInputAction) {
            rightInputAction.action.performed += Exit;
            isExiting = true;

        }

    }

    public void RemoveAllInputAction() {
        if (leftInputAction) {
            if (isNavigating)
                leftInputAction.action.performed -= Navigate;

            if (isExiting)
                leftInputAction.action.performed -= Exit;

        }

        if (rightInputAction) {
            if (isNavigating)
                rightInputAction.action.performed -= Navigate;

            if (isExiting)
                rightInputAction.action.performed -= Exit;

        }
        Debug.Log("RemoveAllInputAction");

        isNavigating = false;
        isExiting = false;
    }

    private void Navigate(InputAction.CallbackContext obj) {
        if (!nextLocation || obj.ReadValue<float>() < inputThreshold) { return; }

        RemoveAllInputAction();

        hotspot.LoadNextPlace(nextLocation, nextLocationNavigationButtonsParent);

    }

    private void Exit(InputAction.CallbackContext obj) {
        Debug.Log("Exit <-");
        if (obj.ReadValue<float>() < inputThreshold) { return; }
        Debug.Log("Exit ->");

        RemoveAllInputAction();

        hotspot.StopHotspot();
    }

}
