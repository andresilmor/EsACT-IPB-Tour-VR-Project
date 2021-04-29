using UnityEngine;

[System.Serializable]
public class Level {
    public GameObject levelGroup;
    public bool isLevelFinished = false;
    public int totalHotspots;
    public int hotspotsVisited;

}