using UnityEngine;

public class AutoStreetLight : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        // Tự động lấy cái bóng đèn gắn trên cùng Object này
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        // Nếu hệ thống thời gian chưa sẵn sàng thì bỏ qua
        if (DayTime.instance == null || myLight == null) return;

        // Chỉ ra lệnh bật/tắt khi trạng thái của đèn đang bị NGƯỢC với bầu trời
        // (Viết thế này giúp game siêu mượt, không bị lag dù có 1000 cái đèn)
        if (myLight.enabled != DayTime.instance.isNight)
        {
            myLight.enabled = DayTime.instance.isNight;
        }
    }
}