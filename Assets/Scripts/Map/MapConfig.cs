using UnityEngine;

public abstract class MapConfig : ScriptableObject
{
    public string mapName;
    public GameObject[] sceneries;
    public GameObject[] left_view;
    public GameObject[] right_view;
    public GameObject[] obs;
    public abstract void PlayMapLogic();
}