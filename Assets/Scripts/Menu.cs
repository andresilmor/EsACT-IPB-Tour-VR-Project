using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using System;

public class Menu : MonoBehaviour
{
    [Header("Menu Config:")]
    [SerializeField] XRRig playerRig;
    [SerializeField] GameObject VRCamera;
    [SerializeField] InputActionReference leftHandMenuAction;
    [SerializeField] InputActionReference rightHandMenuAction;
    [SerializeField] LocomotionController locomotionController;
    [SerializeField] ContinuousMovement continuousMovement;
    [SerializeField] GameObject environment;
    [SerializeField] GameObject world;
    [SerializeField] List<XRRayInteractor> rayInteractorsToHide;
    [SerializeField] List<XRRayInteractor> UIRayInteractors;


    [SerializeField] VideoPlayer videoPlayer;
    private bool videoPlaying = false;

    private LevelManager levelManager;

    private bool isOpened = false;

    private void Start() {
        leftHandMenuAction.action.performed += ManageMenu;
        rightHandMenuAction.action.performed += ManageMenu;

        levelManager = FindObjectOfType<LevelManager>();

    }

    private void ManageMenu(InputAction.CallbackContext obj) {
        isOpened = !isOpened;

        if (isOpened)
            OpenMenu();
        else
            CloseMenu();

    }

    private void OpenMenu() {
        environment.transform.position = playerRig.transform.position;
        environment.transform.rotation = new Quaternion(0f, playerRig.transform.rotation.y, 0f, playerRig.transform.rotation.w);
        environment.SetActive(true);
        world.SetActive(false);
        levelManager.HideLevel();

        if (videoPlayer && videoPlayer.isPlaying) {
            videoPlayer.Pause();
            videoPlaying = true;

        }

        ShowUIRayInteractor();
        HideExternRayInteractor();

        continuousMovement.ChangeGravityStatus(false);

        locomotionController.DisableTeleport();
        continuousMovement.DisableContinuousWalk();

    }

    private void CloseMenu() {
        environment.SetActive(false);
        world.SetActive(true);
        levelManager.ShowLevel();

        if (videoPlayer && videoPlaying) {
            videoPlayer.Pause();
            videoPlaying = false;

        }

        HideUIRayInteractor();
        ShowExternRayInteractor();
            
        locomotionController.EnableTeleport();
        continuousMovement.ChangeGravityStatus(true);
        continuousMovement.EnableContinuousWalk();

    }

    public void HideExternRayInteractor() {
        foreach (XRRayInteractor interactor in rayInteractorsToHide)
            interactor.gameObject.GetComponent<XRInteractorLineVisual>().enabled = false;

    }

    public void ShowExternRayInteractor() {
        foreach (XRRayInteractor interactor in rayInteractorsToHide)
            interactor.gameObject.GetComponent<XRInteractorLineVisual>().enabled = true;

    }

    public void HideUIRayInteractor() {
        foreach (XRRayInteractor interactor in UIRayInteractors)
            interactor.gameObject.GetComponent<XRInteractorLineVisual>().enabled = false;

    }

    public void ShowUIRayInteractor() {
        foreach (XRRayInteractor interactor in UIRayInteractors)
            interactor.gameObject.GetComponent<XRInteractorLineVisual>().enabled = true;

    }

}
