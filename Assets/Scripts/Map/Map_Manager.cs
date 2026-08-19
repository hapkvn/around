using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public static Map_Manager instance;

    public List<MapConfig> listMap;
    public int randomIndex = -1; // Để -1 lúc đầu để hàm Random không bị trùng

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // BẮT BUỘC: Phải gọi LoadMap() ngay khi game bật lên để có dữ liệu cho RoadManager lấy ở hàm Start()
        LoadMap();
    }

    public void LoadMap()
    {
        if (listMap.Count == 0) return;

        // Nếu game chỉ có 1 Map thì không cần tính toán chống lặp
        if (listMap.Count == 1)
        {
            randomIndex = 0;
            return;
        }

        int newIndex = randomIndex;

        // ========================================================
        // LOGIC CHỐNG LẶP: Ép vòng quay phải ra một số KHÁC số cũ.
        // Đảm bảo Map 2 chắc chắn phải khác Map 1.
        // ========================================================
        while (newIndex == randomIndex)
        {
            newIndex = Random.Range(0, listMap.Count);
        }

        // Cập nhật Index sang Map mới
        randomIndex = newIndex;

        Debug.Log("<color=green>Đã chuyển sang Map số: </color>" + randomIndex);
    }

    public GameObject returnRoad()
    {
        MapConfig selectMap = listMap[randomIndex];
        int ranRoad = Random.Range(0, selectMap.sceneries.Length);
        return selectMap.sceneries[ranRoad];
    }

    public GameObject returnLeftView()
    {
        MapConfig selectMap = listMap[randomIndex];
        int randomVL = Random.Range(0, selectMap.left_view.Length);
        return selectMap.left_view[randomVL];
    }

    public GameObject returnRightView()
    {
        MapConfig selectMap = listMap[randomIndex];
        int randomVR = Random.Range(0, selectMap.right_view.Length);
        return selectMap.right_view[randomVR];
    }

    public float returnPL()
    {
        return listMap[randomIndex].PosLv;
    }

    public float returnPR()
    {
        return listMap[randomIndex].PosRv;
    }
    public float returnPRoad()
    {
        return listMap[randomIndex].roadpos;
    }
}