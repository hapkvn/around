using UnityEngine;

public class DayTime : MonoBehaviour
{
    public static DayTime instance;

    [Header("Cài đặt Ánh sáng")]
    [Tooltip("Kéo đèn Directional Light (Mặt trời) trên Scene vào đây")]
    public Light sun;

    [Tooltip("Độ sáng tối đa vào giữa trưa")]
    public float maxSunIntensity = 1f;

    [Tooltip("Độ sáng tối thiểu vào ban đêm (thường để 0)")]
    public float minSunIntensity = 0f;

    [Header("Cài đặt Thời gian")]
    [Tooltip("Thời gian để trôi qua 1 ngày/đêm (tính bằng giây)")]
    public float dayDuration = 60f;
    private float rotationSpeed;

    [Header("Cài đặt Đèn xe")]
    public Light carHeadlights;
    public bool isNight { get; private set; }

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        rotationSpeed = 360f / dayDuration;
    }

    void Update()
    {
        if (sun != null)
        {
            // 1. Xoay mặt trời
            sun.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

            // 2. Tự động tăng/giảm độ sáng dựa theo góc cao của mặt trời
            UpdateSunIntensity();

            // 3. Kiểm tra bật/tắt đèn xe
            CheckNightTime();
        }
    }

    private void UpdateSunIntensity()
    {
        // Lấy góc chúc xuống của mặt trời (-sun.transform.forward.y)
        // Khi giữa trưa (chúc thẳng xuống): multiplier = 1
        // Khi hoàng hôn (ngang chân trời): multiplier = 0
        // Khi ban đêm (chỉa lên trời): multiplier bị kẹp về 0 (nhờ hàm Clamp01)
        float sunAngleMultiplier = Mathf.Clamp01(-sun.transform.forward.y);

        // Nội suy độ sáng mượt mà từ Min đến Max
        sun.intensity = Mathf.Lerp(minSunIntensity, maxSunIntensity, sunAngleMultiplier);
    }

    private void CheckNightTime()
    {
        // Bầu trời được coi là "Đêm" khi mặt trời nằm dưới chân trời (y > 0)
        if (sun.transform.forward.y > 0)
        {
            if (!isNight)
            {
                isNight = true;
                TurnOnLights(true);
            }
        }
        else
        {
            if (isNight)
            {
                isNight = false;
                TurnOnLights(false);
            }
        }
    }

    private void TurnOnLights(bool state)
    {
        if (carHeadlights != null)
        {
            carHeadlights.enabled = state;
        }
    }
}