using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [Header("LookAt Config:")]
    [SerializeField] GameObject objectToLook;
    [SerializeField] bool keepLooking;
    [SerializeField]

    // Update is called once per frame
    void FixedUpdate()
    {
        if (keepLooking)
            Look();

    }

    private void Look() {
        if (!objectToLook) { return; }

        gameObject.transform.rotation = new Quaternion(0f, objectToLook.transform.rotation.y, 0f, objectToLook.transform.rotation.w);

    }

}
