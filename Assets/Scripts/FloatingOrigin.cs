using Cinemachine; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour
{
    [Header("Cài đặt Reset Tọa Độ")]
    [SerializeField] private float threshold = 3000f;

    void FixedUpdate()
    {
        if (transform.position.z > threshold)
        {
            ShiftWorld();
        }
    }

    private void ShiftWorld()
    {
        Vector3 offset = new Vector3(0f, 0f, transform.position.z);
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        // 1. Dịch chuyển thế giới (đã cách ly Camera rất chuẩn)
        foreach (GameObject go in rootObjects)
        {
            if (go.GetComponent<Camera>() != null || go.GetComponent<CinemachineVirtualCamera>() != null)
            {
                continue;
            }

            go.transform.position -= offset;

            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.position -= offset;
            }
        }
        TrailRenderer[] allTrails = FindObjectsByType<TrailRenderer>(FindObjectsInactive.Exclude);
        foreach (TrailRenderer trail in allTrails)
        {
            trail.Clear();
        }

        // Đồng bộ hệ thống vật lý (để xe không bị khựng)
        Physics.SyncTransforms();


        // 2. DỊCH CHUYỂN BẰNG API CHUẨN CỦA CINEMACHINE
        CinemachineVirtualCamera vCam = FindAnyObjectByType<CinemachineVirtualCamera>();
        if (vCam != null && vCam.Follow != null)
        {
            // Điểm mấu chốt: Truyền đúng vCam.Follow thay vì transform của xe
            // Lệnh này tự động dời cả Virtual Camera và Main Camera đi -offset
            // Đồng thời dời luôn bộ đệm nội bộ của Cinemachine, giữ nguyên khoảng cách tuyệt đối!
            vCam.OnTargetObjectWarped(vCam.Follow, -offset);
        }
        // Đồng bộ lại mốc gọi Map mới của RoadManager cho khớp với tọa độ vừa lùi
        if (RoadManager.instance != null)
        {
            RoadManager.instance.AdjustResetPosition(offset.z);
        }
    }
}