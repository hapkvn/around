using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Game Data/Map Config")]
public class MapConfig : ScriptableObject
{
    public string mapName;
    public GameObject[] sceneries;
}