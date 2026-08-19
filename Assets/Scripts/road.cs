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
    private int RoadCount = 20;

    [Header("Khoảng cách mỗi lần đổi Map")]
    public float distancePerMap = 1000f;

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
        roads = new GameObject[RoadCount];
        LeftRoad = new GameObject[RoadCount * 2];
        RightRoad = new GameObject[RoadCount * 2];

        // 1. TẠO ĐƯỜNG CHÍNH (Đã sửa để lấy đúng tọa độ roadpos từ MapConfig)
        for (int i = 0; i < roads.Length; i++)
        {
            GameObject roadPrefab = Map_Manager.instance.returnRoad();
            float roadX = Map_Manager.instance.returnPRoad(); // LẤY CHUẨN TỪ DATA

            roads[i] = Instantiate(roadPrefab, new Vector3(roadX, 0, i * roadLength - 10), Quaternion.identity, transform);
        }

        // 2. TẠO CẢNH TRÁI
        for (int i = 0; i < LeftRoad.Length; i++)
        {
            GameObject leftPrefab = Map_Manager.instance.returnLeftView();
            float leftPosX = Map_Manager.instance.returnPL();

            float roX = leftPrefab.transform.eulerAngles.x;
            float roZ = leftPrefab.transform.eulerAngles.z;

            LeftRoad[i] = Instantiate(leftPrefab, new Vector3(leftPosX, 0, i * build), Quaternion.Euler(roX, 0, roZ), transform);
        }

        // 3. TẠO CẢNH PHẢI
        for (int i = 0; i < RightRoad.Length; i++)
        {
            GameObject rightPrefab = Map_Manager.instance.returnRightView();
            float rightPosX = Map_Manager.instance.returnPR();

            float roX = rightPrefab.transform.eulerAngles.x;
            float roZ = rightPrefab.transform.eulerAngles.z;

            RightRoad[i] = Instantiate(rightPrefab, new Vector3(rightPosX, 0, i * build), Quaternion.Euler(roX, 180, roZ), transform);
        }
    }

    void Update()
    {
        if (!isStage2 && player.transform.position.z > resetPos - 100)
        {
            isStage2 = true;
            Map_Manager.instance.LoadMap();
        }

        if (isStage2 && replacedRoadCount >= roads.Length && replacedLeftCount >= LeftRoad.Length && replacedRightCount >= RightRoad.Length)
        {
            isStage2 = false;
            replacedRoadCount = 0;
            replacedLeftCount = 0;
            replacedRightCount = 0;
            resetPos += distancePerMap;
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

            // SỬA: Luôn bám theo tọa độ đường của Map hiện tại
            newPosition.x = Map_Manager.instance.returnPRoad();

            for (int i = 0; i < roads.Length - 1; i++)
            {
                roads[i] = roads[i + 1];
            }

            if (isStage2 && replacedRoadCount < roads.Length)
            {
                Destroy(oldestRoad);
                roads[roads.Length - 1] = Instantiate(Map_Manager.instance.returnRoad(), newPosition, Quaternion.identity, transform);
                replacedRoadCount++;
            }
            else
            {
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

            // LUÔN LẤY TỌA ĐỘ CHUẨN TỪ MAPCONFIG
            newPosition.x = Map_Manager.instance.returnPL();

            for (int i = 0; i < LeftRoad.Length - 1; i++)
            {
                LeftRoad[i] = LeftRoad[i + 1];
            }

            if (isStage2 && replacedLeftCount < LeftRoad.Length)
            {
                Destroy(oldestLeftRoad);

                // MÌNH ĐÃ XÓA LOGIC RANDOM IF (rand == 0) Ở ĐÂY
                // Vì nó làm lệch tọa độ chuẩn của MapConfig mà bạn thiết lập

                GameObject leftPrefab = Map_Manager.instance.returnLeftView();
                float roX = leftPrefab.transform.eulerAngles.x;
                float roZ = leftPrefab.transform.eulerAngles.z;

                LeftRoad[LeftRoad.Length - 1] = Instantiate(leftPrefab, newPosition, Quaternion.Euler(roX, 0, roZ), transform);
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

            // LUÔN LẤY TỌA ĐỘ CHUẨN TỪ MAPCONFIG
            newPosition.x = Map_Manager.instance.returnPR();

            for (int i = 0; i < RightRoad.Length - 1; i++)
            {
                RightRoad[i] = RightRoad[i + 1];
            }

            if (isStage2 && replacedRightCount < RightRoad.Length)
            {
                Destroy(oldestRightRoad);

                // MÌNH ĐÃ XÓA LOGIC RANDOM IF (rand == 0) Ở ĐÂY
                // Tránh tình trạng cảnh vật bị giật vào lòng đường trái ý bạn

                GameObject rightPrefab = Map_Manager.instance.returnRightView();
                float roX = rightPrefab.transform.eulerAngles.x;
                float roZ = rightPrefab.transform.eulerAngles.z;

                RightRoad[RightRoad.Length - 1] = Instantiate(rightPrefab, newPosition, Quaternion.Euler(roX, 180, roZ), transform);
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
        if (roads == null || roads.Length <= 5 || roads[5] == null)
        {
            return 5 * roadLength - 10f;
        }
        return roads[5].transform.position.z;
    }

    public void AdjustResetPosition(float offset)
    {
        resetPos -= offset;
    }
}