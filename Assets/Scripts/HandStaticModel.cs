using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class HandStaticModel : MonoBehaviour
{

    [System.Serializable]
    private enum Hand {
        none,
        left,
        right,
        both
    }

    [Header("Hand Model Config:")]
    [SerializeField] Hand hand;
    [SerializeField] List<GameObject> leftHandModels = null;
    [SerializeField] List<GameObject> rightHandModels = null;
    [SerializeField] GameObject defaultModel;

    [SerializeField] ActionBasedController leftHand = null;
    [SerializeField] ActionBasedController rightHand = null;

    private GameObject leftHandModel = null;
    private GameObject rightHandModel = null;


    void Start() {
        DestroyAll();

        if (!VerifyConditions()) { return; }

        InputSystem.onDeviceChange += onDeviceChange;
        
    }

    private void DestroyAll() {
       
        if (rightHandModel)
            Destroy(rightHandModel);
        if (leftHandModel)
            Destroy(leftHandModel);

        DestroyModels(leftHand.modelTransform.gameObject);
        DestroyModels(rightHand.modelTransform.gameObject);

    }

    private void DestroyModels(GameObject modelParent) {
        foreach (Transform modelChild in modelParent.transform) {
            DestroyImmediate(modelChild.gameObject);
            if (modelChild)
                DestroyImmediate(modelChild.gameObject);

        }
    }

    private bool VerifyConditions() {
        if (hand.Equals(Hand.none) || (!leftHand && !rightHand)) {
            gameObject.GetComponent<HandStaticModel>().enabled = false;
            return false;

        }

        if (hand.Equals(Hand.both) && (leftHandModels.Count == 0 || rightHandModels.Count == 0)) {
            gameObject.GetComponent<HandStaticModel>().enabled = false;
            return false;

        }

        if (hand.Equals(Hand.left) && (leftHandModels.Count == 0 || !leftHand)) {
            gameObject.GetComponent<HandStaticModel>().enabled = false;
            return false;

        }

        if (hand.Equals(Hand.right) && (rightHandModels.Count == 0 || !rightHand)) {
            gameObject.GetComponent<HandStaticModel>().enabled = false;
            return false;

        }

        return true;

    }

    private void onDeviceChange(InputDevice device, InputDeviceChange change) {
            switch (change) {
                case InputDeviceChange.Added:
                    //Debug.Log("Added" + device.displayName);
                    DefineValidHandsModel(device);

                    break;
                case InputDeviceChange.Disconnected:
                    //Debug.Log("Disconnected" + device.displayName);
                    break;
                case InputDeviceChange.Reconnected:
                    DefineValidHandsModel(device);
                    //Debug.Log("Reconnected" + device.displayName);
                    break;
                case InputDeviceChange.Removed:
                    //Debug.Log("Removed " + device.displayName);
                    break;
                case InputDeviceChange.Disabled:
                    //Debug.Log("Disabled " + device.displayName);
                    break;
                case InputDeviceChange.Enabled:
                    DefineValidHandsModel(device);
                    //Debug.Log("Enabled " + device.displayName);
                    break;
                case InputDeviceChange.UsageChanged:
                    DefineValidHandsModel(device);
                    break;
                case InputDeviceChange.ConfigurationChanged:
                    DefineValidHandsModel(device);
                    //Debug.Log("ConfigurationChanged " + device.displayName);
                    break;
                case InputDeviceChange.Destroyed:
                    //Debug.Log("Destroyed " + device.displayName);
                    break;
            default:
                    //Debug.Log("ANOTHER" + device.displayName);
                    break;
            };

    }

    private void DefineValidHandsModel(InputDevice device) {
        if ((hand.Equals(Hand.right) || hand.Equals(Hand.both))) {
            if (rightHandModel) {
                DestroyModels(rightHand.modelTransform.gameObject);
            }

            AttributeModel(device.displayName, rightHandModels, rightHand);
            rightHandModel = rightHand.modelPrefab.gameObject;

        }

        if ((hand.Equals(Hand.left) || hand.Equals(Hand.both))) {
            if (leftHandModel) {
                DestroyModels(leftHand.modelTransform.gameObject);
            }

            AttributeModel(device.displayName, leftHandModels, leftHand);
            leftHandModel = leftHand.modelPrefab.gameObject;

        }
    }

    private void AttributeModel(string controllerName, List<GameObject> handModels,    ActionBasedController actionBasedController) {

        foreach (GameObject model in handModels) {
            if (model.name.Contains(controllerName)) {
                actionBasedController.modelPrefab = Instantiate(model, actionBasedController.modelTransform).transform;
                return;

            }

        }

        actionBasedController.modelPrefab = Instantiate(defaultModel, actionBasedController.modelTransform).transform;

    }


    private void OnEnable() {
        if (!VerifyConditions()) { return; }

        InputSystem.onDeviceChange += onDeviceChange;

    }

    private void OnDisable() {
        InputSystem.onDeviceChange -= onDeviceChange;
        DestroyModels(leftHand.modelTransform.gameObject);
        DestroyModels(rightHand.modelTransform.gameObject);

    }

}
