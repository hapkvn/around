# 🏎️ 3D Endless Runner

Một tựa game đua xe Endless Runner 3D tốc độ cao được phát triển bằng Unity. Người chơi sẽ điều khiển một chiếc xe vượt qua các chướng ngại vật trên một con đường được tạo ra vô tận, trải nghiệm sự chuyển đổi thời gian mượt mà từ bình minh rực rỡ đến màn đêm tĩnh lặng.---

## 🌟 Giới Thiệu & Tính Năng

*   **Vật lý Đệm khí (Hover Suspension):** Xe di chuyển bằng hệ thống phuộc nhún lò xo (Raycast Suspension) mô phỏng tàu đệm khí, triệt tiêu 100% lỗi vấp mép đường khi ghép nối map.
*   **Chu kỳ Ngày Đêm Động:** Môi trường game (ánh sáng mặt trời, sương mù, đèn đường, đèn pha ô tô) tự động nội suy chuyển đổi mượt mà dựa trên góc chiếu của mặt trời.
*   **Camera Điện Ảnh (Cinemachine):** Hệ thống chuyển cảnh tự động từ góc máy "Intro" ấn tượng sang góc nhìn sau xe khi bắt đầu game mà không cần chuyển Scene.
*   **Thế Giới Vô Tận:** Kết hợp thuật toán đẻ đường ngẫu nhiên và hệ thống "Floating Origin" (Giật lùi tọa độ thế giới) giúp xe chạy xa hàng vạn dặm mà không bị rung giật vật lý.

---

## 🎮 Hướng Dẫn Chơi

*   **Điều khiển:** Nghiêng hoặc xoay điện thoại sang trái/phải để bẻ lái (tích hợp cảm biến gia tốc - Accelerometer cùng bộ lọc nhiễu chống rung tay). Hỗ trợ phím `A` / `D` hoặc `Mũi tên Trái` / `Mũi tên Phải` khi kiểm tra trên Unity Editor.
*   **Luật chơi:** Tránh va chạm với các chướng ngại vật trên đường.
*   **Tính điểm:** Điểm số được cộng dồn tự động dựa trên quãng đường thực tế xe lăn bánh (1 điểm / 50m).

---

## 🛠️ Điểm Nhấn Kỹ Thuật

Dự án sử dụng C# hướng đối tượng với cấu trúc tách biệt rõ ràng, áp dụng mẫu thiết kế Singleton cho các hệ thống quản lý:
*   `Player.cs`: Xử lý vật lý di chuyển, đọc gia tốc phần cứng điện thoại, nội suy vận tốc bẻ lái (Lerp), giả lập trọng lực và bắt sự kiện va chạm (Collision).
*   `DayNightCycle.cs`: Quản lý `RenderSettings` (Màu sương mù, độ sáng môi trường) đồng bộ hoàn toàn với trục xoay của Directional Light.
*   `StartGame.cs` & `AudioManager.cs`: Quản lý Game State (Start, Pause, Game Over, Quick Restart) và âm thanh toàn cục thông qua cấu trúc Singleton bất tử (`static instance`).
  
##   👨‍💻 Nhà phát triển

**[HUY HOANG]** 
*   **Vai trò:** Game Developer (Unity / C#)
*   **GitHub:** [@hapkvn](https://github.com/hapkvn)
*   **Liên hệ:** [huyhoangpkvnn75@gmail.com]
