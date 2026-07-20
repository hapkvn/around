using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // Mảng chứa các đoạn đường. Bạn có thể set size = 10 trong Unity Inspector
    [SerializeField] private GameObject[] roads;
    [SerializeField] private GameObject player;

    [SerializeField] private float roadLength = 40f;
    [SerializeField] private float triggerOffset = 15f; // Khoảng cách bù trừ để kích hoạt

    void Start()
    {
        for(int i = 0; i < roads.Length; i++)
        {
            Instantiate(roads[i], new Vector3(0, 0, i * roadLength), Quaternion.identity);
        }
       
    }

    void Update()
    {
        UpdateRoad();
    }

    void UpdateRoad()
    {
        // Kiểm tra vị trí player so với đoạn đường thứ 2 (giống logic cũ của bạn với road2)
        // roads[1] tương đương với road2 trong code cũ
        if (player.transform.position.z > roads[1].transform.position.z + triggerOffset)
        {
            // 1. Lấy đường cũ nhất (đang ở tít phía sau)
            GameObject oldestRoad = roads[0];

            // 2. Lấy vị trí của đường mới nhất (nằm ở cuối mảng)
            GameObject newestRoad = roads[roads.Length - 1];

            // 3. Tính toán vị trí mới và di chuyển đường cũ lên phía trước
            Vector3 newPosition = newestRoad.transform.position;
            newPosition.z += roadLength;
            oldestRoad.transform.position = newPosition;

            // 4. Dịch chuyển các phần tử trong mảng lên 1 bậc
            for (int i = 0; i < roads.Length - 1; i++)
            {
                roads[i] = roads[i + 1];
            }

            // 5. Đưa đoạn đường vừa di chuyển xuống cuối mảng
            roads[roads.Length - 1] = oldestRoad;
        }
    }
}