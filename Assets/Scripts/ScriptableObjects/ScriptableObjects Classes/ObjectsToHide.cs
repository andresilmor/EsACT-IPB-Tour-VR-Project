using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectsToHide", order = 1)]

public class ObjectsToHide : ScriptableObject
{
    public List<GameObject> objects;

}
