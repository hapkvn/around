# 🏎️ 3D Endless Runner

Một tựa game đua xe Endless Runner 3D tốc độ cao được phát triển bằng Unity. Người chơi sẽ điều khiển một chiếc xe vượt qua các chướng ngại vật trên một con đường được tạo ra vô tận, trải nghiệm sự chuyển đổi thời gian mượt mà từ bình minh rực rỡ đến màn đêm tĩnh lặng.

---

## 🌟 Giới Thiệu & Tính Năng

*   **Vật lý Đệm khí (Hover Suspension):** Xe di chuyển bằng hệ thống phuộc nhún lò xo (Raycast Suspension) mô phỏng tàu đệm khí, triệt tiêu 100% lỗi vấp mép đường khi ghép nối map.
*   **Chu kỳ Ngày Đêm Động:** Môi trường game (ánh sáng mặt trời, sương mù, đèn đường, đèn pha ô tô) tự động nội suy chuyển đổi mượt mà dựa trên góc chiếu của mặt trời.
*   **Camera Điện Ảnh (Cinemachine):** Hệ thống chuyển cảnh tự động từ góc máy "Intro" ấn tượng sang góc nhìn sau xe khi bắt đầu game mà không cần chuyển Scene.
*   **Thế Giới Vô Tận:** Kết hợp thuật toán đẻ đường ngẫu nhiên và hệ thống "Floating Origin" (Giật lùi tọa độ thế giới) giúp xe chạy xa hàng vạn dặm mà không bị rung giật vật lý vật lý.

---

## 🎮 Hướng Dẫn Chơi

*   **Điều khiển:** Sử dụng phím `A` / `D` hoặc `Mũi tên Trái` / `Mũi tên Phải` để bẻ lái.
*   **Luật chơi:** Tránh va chạm với các chướng ngại vật trên đường.
*   **Tính điểm:** Điểm số được cộng dồn tự động dựa trên quãng đường thực tế xe lăn bánh (1 điểm / 50m).

---

## 🛠️ Điểm Nhấn Kỹ Thuật

Dự án sử dụng C# hướng đối tượng với cấu trúc tách biệt rõ ràng, áp dụng mẫu thiết kế Singleton cho các hệ thống quản lý:
*   `Player.cs`: Xử lý vật lý di chuyển, nội suy vận tốc bẻ lái (Lerp), giả lập trọng lực và bắt sự kiện va chạm (Collision).
*   `DayNightCycle.cs`: Quản lý `RenderSettings` (Màu sương mù, độ sáng môi trường) đồng bộ hoàn toàn với trục xoay của Directional Light.
*   `StartGame.cs` & `AudioManager.cs`: Quản lý Game State (Start, Pause, Game Over, Quick Restart) và âm thanh toàn cục thông qua cấu trúc Singleton bất tử (`static instance`).

---

## 🚀 Cài Đặt & Chạy Dự Án

*   **Phiên bản Unity:** (Điền phiên bản Unity của bạn vào đây, ví dụ: 2022.3 LTS)
*   Yêu cầu Package: Bắt buộc cài đặt **Cinemachine** thông qua Package Manager.
*   **Cách chạy:** Mở project bằng Unity, nạp Scene `Game` (hoặc MainScene), ấn Play và trải nghiệm!
