using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hotspot : MonoBehaviour {
    [Header("360 Image Hotspot Config:")]
    [SerializeField] Material skyboxMaterial;
    [SerializeField] GameObject skyboxPlaceholder;
    [SerializeField] HotspotSkyboxController skyboxController;
    [SerializeField] GameObject hotspotGround;
    [SerializeField] GameObject navigationButtonsParent;
    [SerializeField] GameObject world;
    [SerializeField] List<XRRayInteractor> rayInteractorsToHide;
    [SerializeField] List<XRRayInteractor> UIRayInteractors;

    private PresenceDetector presenceDetector;

    private bool hotspotVisited = false;

    private Material startSkybox;
    private GameObject startNavigationButtonsParent;

    private LevelManager levelManager;

    private void Start() {

        if (skyboxMaterial && skyboxMaterial)
            SetSkyboxMaterial(skyboxMaterial, skyboxPlaceholder);

        levelManager = FindObjectOfType<LevelManager>();

        startSkybox = skyboxMaterial;
        startNavigationButtonsParent = navigationButtonsParent;

        presenceDetector = gameObject.GetComponent<PresenceDetector>();

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

    public bool WasVisited() {
        return hotspotVisited;

    }

    public void RestartHotspot() {
        skyboxMaterial = startSkybox;
        navigationButtonsParent = startNavigationButtonsParent;

    }

    public GameObject GetSkybox() {
        return skyboxPlaceholder;

    }

    public void SetSkyboxMaterial(Material skyboxMaterial, GameObject skyboxPlaceholder) {
        skyboxPlaceholder.GetComponent<MeshRenderer>().material = skyboxMaterial;

    }

    public void SetSkyboxMaterial(Material skyboxMaterial) {
        this.skyboxMaterial = skyboxMaterial;

    }

    public void SetNavigationButtons(GameObject navigationButtonsParent) {
        this.navigationButtonsParent = navigationButtonsParent;

    }

    public void StartHotspot() {
        HideExternRayInteractor();
        ShowUIRayInteractor();

        levelManager.HideLevel(this.gameObject);
        skyboxPlaceholder.SetActive(true);
        hotspotGround.SetActive(true);
        if (navigationButtonsParent)
            navigationButtonsParent.SetActive(true);

        if (presenceDetector)
            presenceDetector.enabled = false;

    }

    public void StopHotspot() {
        ShowExternRayInteractor();
        HideUIRayInteractor();

        levelManager.ShowLevel();
        skyboxPlaceholder.SetActive(false);
        skyboxController.DisableSkybox();

        if (navigationButtonsParent)
            navigationButtonsParent.SetActive(false);

        if (!hotspotVisited) {
            FindObjectOfType<LevelManager>().HotspotVisited();

            hotspotVisited = true;

        }

        if (presenceDetector)
            presenceDetector.enabled = true;

    }

    public void LoadNextPlace(Material nextLocation, GameObject nextLocationButtons) {
        skyboxMaterial = nextLocation;
        SetSkyboxMaterial(skyboxMaterial, skyboxPlaceholder);
        navigationButtonsParent.SetActive(false);
        navigationButtonsParent = nextLocationButtons;
        navigationButtonsParent.SetActive(true);

    }

    public void SetWorldActive(bool status) {
        if (world == null) { return; }

        world.SetActive(status);

    }
}
