using UnityEngine;

public abstract class MapConfig : ScriptableObject
{
    public string mapName;
    public GameObject[] sceneries;
    public abstract void PlayMapLogic();
}