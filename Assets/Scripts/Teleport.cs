using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Teleport : BaseTeleportationInteractable
{
    [Header("Teleport Config:")]
    [SerializeField] Transform destiny;
    [SerializeField] string objectTagLimitation = "";
    [SerializeField] LayerMask objectLayerLimitation;

    private void OnTriggerEnter(Collider other) {
        TeleportObject(other.gameObject);

    }

    public void TeleportObject(GameObject objectToTeleport) {
        if (ValidateObject(objectToTeleport)) {
            Debug.Log(objectToTeleport.name);
            objectToTeleport.transform.position = new Vector3(0,0,0);
            Debug.Log(objectToTeleport.transform.position);
            return;
        }

        Debug.Log("NOP0");

    }

    private bool ValidateObject(GameObject objectToTeleport) {
        if (objectLayerLimitation == (objectLayerLimitation | (1 << objectToTeleport.layer))) {
            if (objectTagLimitation.Length > 0) {
                if (!objectToTeleport.tag.Equals(objectTagLimitation))
                    return false;

            }

            return true;

        }

        return false;
    }
}
