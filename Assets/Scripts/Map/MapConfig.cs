using UnityEngine;

public abstract class MapConfig : ScriptableObject
{
    public string mapName;
    public GameObject[] sceneries;
    public GameObject[] left_view;
    public GameObject[] right_view;
    public GameObject[] obs;
    public float PosLv;
    public float PosRv;
    public float roadpos;

    public abstract void PlayMapLogic();
    

}