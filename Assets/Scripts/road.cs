using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // Kéo thả các Prefab đường vào đây trong Inspector
    [SerializeField] private GameObject[] roads;
    [SerializeField] private GameObject[] buildRoadPrefab;
    private GameObject[] LeftRoad;
    private GameObject[] RightRoad;
    [SerializeField] private GameObject player;
    

    [SerializeField] private float roadLength = 40f;
    [SerializeField] private float build = 10f;
    [SerializeField] private float triggerOffset = 15f;

    void Start()
    {
        LeftRoad = new GameObject[roads.Length * 2];
        RightRoad = new GameObject[roads.Length * 2];

        for (int i = 0; i < roads.Length; i++)
        {
            // 1. Khởi tạo đường và gán ngược lại vào mảng
            // 2. Thêm 'transform' ở cuối để đưa các đường này làm con của RoadManager cho gọn Hierarchy (tuỳ chọn)
            roads[i] = Instantiate(roads[i], new Vector3(-5, 0, i * roadLength - 10), Quaternion.identity, transform);
        }

        for(int i=0; i<roads.Length*2; i++)
        {
            int randomIndex = Random.Range(0, 3);
            LeftRoad[i] =  Instantiate(buildRoadPrefab[randomIndex], new Vector3(-15, 0, i * build), Quaternion.identity, transform);
        }
        for (int i = 0; i < roads.Length * 2; i++)
        {
            int randomIndex = Random.Range(0, 3);
            RightRoad[i] = Instantiate(buildRoadPrefab[randomIndex], new Vector3(15, 0, i * build), Quaternion.Euler(0, -180, 0), transform);
        }

    }

    void Update()
    {
        UpdateRoad();
        UpdateBuildRoadRight();
        UpdateBuildRoadLeft();
    }

    void UpdateRoad()
    {
        // Kiểm tra xem roads[1] đã tồn tại và player có đi qua chưa
        if (roads[1] != null && player.transform.position.z > roads[1].transform.position.z + triggerOffset)
        {
            GameObject oldestRoad = roads[0];
            GameObject newestRoad = roads[roads.Length - 1];

            // Tính toán vị trí mới
            Vector3 newPosition = newestRoad.transform.position;
            newPosition.z += roadLength;
            oldestRoad.transform.position = newPosition;

            // Dịch chuyển các phần tử trong mảng lên 1 bậc
            for (int i = 0; i < roads.Length - 1; i++)
            {
                roads[i] = roads[i + 1];
            }

            // Đưa đoạn đường vừa di chuyển xuống cuối mảng
            roads[roads.Length - 1] = oldestRoad;
        }
    }
    void UpdateBuildRoadLeft()
    {
        // Kiểm tra xem LeftRoad[1] đã tồn tại và player có đi qua chưa
        if (LeftRoad[1] != null && player.transform.position.z > LeftRoad[1].transform.position.z + triggerOffset)
        {
            GameObject oldestLeftRoad = LeftRoad[0];
            GameObject newestLeftRoad = LeftRoad[LeftRoad.Length - 1];
            // Tính toán vị trí mới
            Vector3 newPosition = newestLeftRoad.transform.position;
            newPosition.z += build;
            oldestLeftRoad.transform.position = newPosition;
            // Dịch chuyển các phần tử trong mảng lên 1 bậc
            for (int i = 0; i < LeftRoad.Length - 1; i++)
            {
                LeftRoad[i] = LeftRoad[i + 1];
            }
            // Đưa đoạn đường vừa di chuyển xuống cuối mảng
            LeftRoad[LeftRoad.Length - 1] = oldestLeftRoad;
        }
    }
    void UpdateBuildRoadRight()
    {
        // Kiểm tra xem LeftRoad[1] đã tồn tại và player có đi qua chưa
        if (RightRoad[1] != null && player.transform.position.z > RightRoad[1].transform.position.z + triggerOffset)
        {
            GameObject oldestLeftRoad = RightRoad[0];
            GameObject newestLeftRoad = RightRoad[RightRoad.Length - 1];
            // Tính toán vị trí mới
            Vector3 newPosition = newestLeftRoad.transform.position;
            newPosition.z += build;
            oldestLeftRoad.transform.position = newPosition;
            // Dịch chuyển các phần tử trong mảng lên 1 bậc
            for (int i = 0; i < RightRoad.Length - 1; i++)
            {
                RightRoad[i] = RightRoad[i + 1];
            }
            // Đưa đoạn đường vừa di chuyển xuống cuối mảng
            RightRoad[RightRoad.Length - 1] = oldestLeftRoad;
        }
    }

}