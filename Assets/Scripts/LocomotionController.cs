using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class LocomotionController : MonoBehaviour
{
    [SerializeField] ActionBasedController leftTeleportRay;
    [SerializeField] ActionBasedController rightTeleportRay;
    [SerializeField] InputActionReference rightRayActivate;
    [SerializeField] [Range(0f, 1f)] float activationThreshold = 0.8f;
    [SerializeField] bool teleportEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!teleportEnabled) { return; }

        rightRayActivate.action.performed += CheckIfActivated;
        rightRayActivate.action.canceled += CheckIfActivated;
        rightRayActivate.action.Enable();
        rightTeleportRay.gameObject.SetActive(false);
    }

    public void EnableTeleport() {
        teleportEnabled = true;

    }

    public void DisableTeleport() {
        teleportEnabled = false;
        rightTeleportRay.gameObject.SetActive(false);
        
    }


    private void CheckIfActivated(InputAction.CallbackContext obj) {
        if (teleportEnabled && obj.ReadValue<Vector2>().y >= activationThreshold) {
            rightTeleportRay.gameObject.SetActive(true);
            return;
        }
        rightTeleportRay.gameObject.SetActive(false);
    }

}
