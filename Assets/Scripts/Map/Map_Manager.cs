using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public List<MapConfig> listMap;
    
    public void LoadMap()
    {
   
        if(listMap.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, listMap.Count);

        MapConfig selectMap = listMap[randomIndex];
    }
}
