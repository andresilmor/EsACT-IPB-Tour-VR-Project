using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager Config: ")]
    [Tooltip("Insert with the pretended order.")]
    [SerializeField] List<Level> levels;
    [SerializeField] bool liberateAllLevels = false;

    private byte actualLevelIndex = 0;
    private Level actualLevel;

    void Start() {
        if (liberateAllLevels) {
            for (int index = 0; index < levels.Count; index += 1) {
                levels[index].levelGroup.SetActive(true);
        
            }
            return;

        }

        for (int index = 0; index < levels.Count; index+=1) {
            levels[index].levelGroup.SetActive(false);
            levels[index].totalHotspots = levels[index].levelGroup.transform.childCount;

        }

        actualLevel = levels[actualLevelIndex];

        actualLevel.levelGroup.SetActive(true);


    }

    public void HotspotVisited() {

        if (actualLevelIndex == levels.Count - 1) { return; }

        actualLevel.hotspotsVisited += 1;

        if (actualLevel.hotspotsVisited == actualLevel.totalHotspots) {
            actualLevel.levelGroup.SetActive(false);
            actualLevel = levels[actualLevelIndex += 1];
            actualLevel.levelGroup.SetActive(true);

        }
            

    }

    public void HideLevel() {
        actualLevel.levelGroup.SetActive(false);

    }

    public void HideLevel(GameObject except) {
        foreach (Transform ht in actualLevel.levelGroup.transform) {
            if (!ht.name.Contains(except.name)) {
                ht.gameObject.SetActive(false);
            } else {
                ht.gameObject.SetActive(true);
            }
        }

    }

    public void ShowLevel() {
        actualLevel.levelGroup.SetActive(true);
        foreach (Transform ht in actualLevel.levelGroup.transform) {
            ht.gameObject.SetActive(true);

        }

    }

    public void ShowLevel(GameObject except) {
        actualLevel.levelGroup.SetActive(true);
        foreach (Transform ht in actualLevel.levelGroup.transform) {
            if (!ht.name.Contains(except.name)) {
                ht.gameObject.SetActive(false);
            } else {
                ht.gameObject.SetActive(true);
            }
        }

    }

}
