using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance { get; private set; }

    [SerializeField] private GameObject[] roads;
    [SerializeField] private GameObject[] roads_2;
    [SerializeField] private GameObject[] buildRoadPrefab;
    [SerializeField] private GameObject[] buildRoadPrefab_v2;
    private GameObject[] LeftRoad;
    private GameObject[] RightRoad;
    [SerializeField] private GameObject player;

    [SerializeField] private float roadLength = 40f;
    [SerializeField] private float build = 10f;
    [SerializeField] private float triggerOffset = 15f;
    private float resetPos = 500f;

    private bool isStage2 = false;
    private int replacedRoadCount = 0;
    private int replacedLeftCount = 0;
    private int replacedRightCount = 0;



    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        LeftRoad = new GameObject[roads.Length * 2];
        RightRoad = new GameObject[roads.Length * 2];

        for (int i = 0; i < roads.Length; i++)
        {
            roads[i] = Instantiate(roads[i], new Vector3(-5, 0, i * roadLength - 10), Quaternion.identity, transform);
        }

        for (int i = 0; i < LeftRoad.Length; i++)
        {
            int randomIndex = Random.Range(0, buildRoadPrefab.Length);
            LeftRoad[i] = Instantiate(buildRoadPrefab[randomIndex], new Vector3(-15, 0, i * build), Quaternion.identity, transform);
        }

        for (int i = 0; i < RightRoad.Length; i++)
        {
            int randomIndex = Random.Range(0, buildRoadPrefab.Length);
            RightRoad[i] = Instantiate(buildRoadPrefab[randomIndex], new Vector3(15, 0, i * build), Quaternion.Euler(0, -180, 0), transform);
        }
    }

    void Update()
    {
        // Chốt trạng thái chuyển sang Map mới. 
        // Khi đã bật lên True, dù Floating Origin có kéo Z về 0 thì trạng thái này vẫn được giữ nguyên.
        if (!isStage2 && player.transform.position.z > resetPos - 100)
        {
            isStage2 = true;
        }

        UpdateRoad();
        UpdateBuildRoadRight();
        UpdateBuildRoadLeft();
    }

    void UpdateRoad()
    {
        if (roads[1] != null && player.transform.position.z > roads[1].transform.position.z + triggerOffset)
        {
            GameObject oldestRoad = roads[0];
            Vector3 newPosition = roads[roads.Length - 1].transform.position;
            newPosition.z += roadLength;

            for (int i = 0; i < roads.Length - 1; i++)
            {
                roads[i] = roads[i + 1];
            }

            // Nếu đang ở map mới VÀ chưa thay thế xong 100% số lượng đường
            if (isStage2 && replacedRoadCount < roads.Length)
            {
                Destroy(oldestRoad);

                // Lấy an toàn index, tránh bị văng game nếu mảng roads_2 ít hơn roads
                int safeIndex = Mathf.Min(replacedRoadCount, roads_2.Length - 1);
                roads[roads.Length - 1] = Instantiate(Map_Manager.instance.returnRoad(), newPosition, Quaternion.identity, transform);

                replacedRoadCount++;
            }
            else
            {
                // Khi chưa đến Map mới, HOẶC khi đã thay thế XONG 100% Map mới -> Quay lại tái sử dụng
                oldestRoad.transform.position = newPosition;
                roads[roads.Length - 1] = oldestRoad;
                Physics.SyncTransforms();
            }
        }
    }

    void UpdateBuildRoadLeft()
    {
        if (LeftRoad[1] != null && player.transform.position.z > LeftRoad[1].transform.position.z + triggerOffset)
        {
            GameObject oldestLeftRoad = LeftRoad[0];
            Vector3 newPosition = LeftRoad[LeftRoad.Length - 1].transform.position;
            newPosition.z += build;


            for (int i = 0; i < LeftRoad.Length - 1; i++)
            {
                LeftRoad[i] = LeftRoad[i + 1];
            }

            if (isStage2 && replacedLeftCount < LeftRoad.Length)
            {
                Destroy(oldestLeftRoad);
                int rand = Random.Range(0, buildRoadPrefab_v2.Length);

                if (rand == 0)
                {
                    newPosition.x = -5f;
                }

                LeftRoad[LeftRoad.Length - 1] = Instantiate(Map_Manager.instance.returnLeftView(), newPosition, Quaternion.identity, transform);
                replacedLeftCount++;
            }
            else
            {
                oldestLeftRoad.transform.position = newPosition;
                LeftRoad[LeftRoad.Length - 1] = oldestLeftRoad;

            }
        }
    }

    void UpdateBuildRoadRight()
    {
        if (RightRoad[1] != null && player.transform.position.z > RightRoad[1].transform.position.z + triggerOffset)
        {
            GameObject oldestRightRoad = RightRoad[0];
            Vector3 newPosition = RightRoad[RightRoad.Length - 1].transform.position;
            newPosition.z += build;
            if (LeftRoad[LeftRoad.Length - 1] == roads_2[0])
            {
                newPosition.x = 5f;
            }

            for (int i = 0; i < RightRoad.Length - 1; i++)
            {
                RightRoad[i] = RightRoad[i + 1];
            }

            if (isStage2 && replacedRightCount < RightRoad.Length)
            {
                Destroy(oldestRightRoad);
                int rand = Random.Range(0, buildRoadPrefab_v2.Length);
                if (rand == 0)
                {
                    newPosition.x = 5f;
                }
                RightRoad[RightRoad.Length - 1] = Instantiate(Map_Manager.instance.returnRightView(), newPosition, Quaternion.Euler(0, 180, 0), transform);
                replacedRightCount++;
            }
            else
            {
                oldestRightRoad.transform.position = newPosition;
                RightRoad[RightRoad.Length - 1] = oldestRightRoad;
            }
        }
    }

    public float spawn()
    {
        return roads[10].transform.position.z;
    }
}