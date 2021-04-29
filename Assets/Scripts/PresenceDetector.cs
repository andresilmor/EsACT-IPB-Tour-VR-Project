using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetector : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject canvasToShow;

    private Hotspot hotspot;

    private void OnTriggerEnter(Collider other) {
       
        if (!canvasToShow) { return; }
        //Debug.Log("Dentro do collider");
        //Debug.Log("Entrnado " + other.gameObject.name);
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer))) {
            canvasToShow.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other) {
       // Debug.Log("Not anymorwe");
        if (canvasToShow)
            canvasToShow.SetActive(false);

    }

    private void OnDisable() {
        if (canvasToShow)
            canvasToShow.SetActive(false);

    }

    private void OnEnable() {
        hotspot = gameObject.GetComponent<Hotspot>();
        if (hotspot) {
            if (hotspot.WasVisited() && canvasToShow) {
                Debug.Log("Y");
                canvasToShow.SetActive(true);
            } else {
                canvasToShow.SetActive(false);
                Debug.Log("F");
            }

            return;
        }

        if (canvasToShow)
            canvasToShow.SetActive(true);

    }

}
