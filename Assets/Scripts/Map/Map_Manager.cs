using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public List<MapConfig> listMap;
    public int randomIndex;
    public static Map_Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }
    public void LoadMap()
    {
   
        if(listMap.Count == 0)
        {
            return;
        }

         randomIndex = Random.Range(0, listMap.Count);

        MapConfig selectMap = listMap[randomIndex];

    }

   
    public GameObject returnRoad()
    {
        MapConfig selectMap = listMap[randomIndex];
        int ranRoad = Random.Range(0, selectMap.sceneries.Length);
        GameObject road = selectMap.sceneries[ranRoad];
        return road;
    }
    public GameObject returnLeftView()
    {
        MapConfig selectMap = listMap[randomIndex];
        int randomVL = Random.Range(0, selectMap.left_view.Length);
        GameObject leftView = selectMap.left_view[randomVL];
        return leftView;
    }

    public GameObject returnRightView()
    {
        MapConfig selectMap = listMap[randomIndex];
        int randomVL = Random.Range(0, selectMap.right_view.Length);
        GameObject rightview = selectMap.right_view[randomVL];
        return rightview;
    }
}
