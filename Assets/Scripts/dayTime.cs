using UnityEngine;

public class DayTime : MonoBehaviour
{
    public static DayTime instance;
    [Header("Cài đặt Ánh sáng")]
    [Tooltip("Kéo đèn Directional Light (Mặt trời) trên Scene vào đây")]
    public Light sun;

    [Header("Cài đặt Thời gian")]
    [Tooltip("Thời gian để trôi qua 1 ngày/đêm (tính bằng giây). Ví dụ: 60 = 1 phút ngoài đời")]
    public float dayDuration = 60f;
    private float rotationSpeed;

    [Header("Cài đặt Đèn xe (Tùy chọn)")]
    public Light carHeadlights;
    public Light[] lightRoad;// Kéo đèn pha của Player vào đây
    public bool isNight { get; private set; }
    // Biến để hệ thống khác biết đang là ngày hay đêm

    void Awake()
    {
        // THÊM 2 DÒNG NÀY ĐỂ KÍCH HOẠT INSTANCE
        if (instance == null) instance = this;
    }
    void Start()
    {
        // Tính toán tốc độ xoay (360 độ chia cho thời gian 1 ngày)
        rotationSpeed = 360f / dayDuration;
    }

    void Update()
    {
        if (sun != null)
        {
            // Liên tục xoay mặt trời quanh trục X theo thời gian
            sun.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

            // Kiểm tra xem trời đang sáng hay tối
            CheckNightTime();
        }
    }

    private void CheckNightTime()
    {
        // Trong Unity, nếu tia sáng (forward) của mặt trời chĩa lên trời (Y > 0)
        // Điều đó có nghĩa là mặt trời đang nằm dưới lòng đất -> Đang là Ban Đêm!
        if (sun.transform.forward.y > 0)
        {
            if (!isNight)
            {
                isNight = true;
                TurnOnLights(true);
            }
        }
        else // Mặt trời chĩa xuống đất -> Ban Ngày
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
            foreach(Light light in lightRoad)
            {
                light.enabled = state;
            }
        }

        // Bạn có thể bật tắt đèn đường hoặc đèn nhà ở đây nếu muốn
    }
}