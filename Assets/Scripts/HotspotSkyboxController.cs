using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotSkyboxController : MonoBehaviour
{
    [Header("Skybox Sphere Controller Config:")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Hotspot hotspot;
    [SerializeField] LocomotionController locomotionController;
    [SerializeField] ContinuousMovement continuousMovement;
    [SerializeField] GameObject particleSystem;
    [SerializeField] GameObject meshGroup;
    [SerializeField] Canvas info;


    private void OnTriggerEnter(Collider other) {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer))) {
            EnableSkybox();

            info.enabled = false;

        }
    }

    private void OnTriggerExit() {
        info.enabled = true;

    }

    private void EnableSkybox() {
        hotspot.StartHotspot();
        locomotionController.DisableTeleport();
        continuousMovement.DisableContinuousWalk();
        particleSystem.SetActive(false);
        meshGroup.SetActive(false);
        hotspot.SetWorldActive(false);

    }

    public void DisableSkybox() {
        hotspot.SetWorldActive(true);
        locomotionController.EnableTeleport();
        continuousMovement.EnableContinuousWalk();
        hotspot.RestartHotspot();
        particleSystem.SetActive(true);
        meshGroup.SetActive(true);

    }
}
