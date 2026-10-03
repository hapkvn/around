# 🏎️ 3D Endless Runner

Một tựa game đua xe Endless Runner 3D tốc độ cao được phát triển bằng Unity. Người chơi sẽ điều khiển một chiếc xe vượt qua các chướng ngại vật trên một con đường được tạo ra vô tận, trải nghiệm sự chuyển đổi thời gian mượt mà từ bình minh rực rỡ đến màn đêm tĩnh lặng.

<img width="1668" height="934" alt="Screenshot 2026-10-04 000003" src="https://github.com/user-attachments/assets/86d62228-2585-43a3-9f0a-d86b68fdaa08" />
<img width="1682" height="934" alt="Screenshot 2026-10-03 235956" src="https://github.com/user-attachments/assets/e2e3823e-f60e-43e4-a718-359008a48f66" />
<img width="1675" height="919" alt="Screenshot 2026-10-04 000149" src="https://github.com/user-attachments/assets/d9b7496b-5f58-400a-9521-2c1e6f3d37e0" />
<img width="1671" height="935" alt="Screenshot 2026-10-04 000142" src="https://github.com/user-attachments/assets/4d1e92c4-6435-4ad8-bc20-e709915ba50d" />
<img width="1668" height="928" alt="Screenshot 2026-10-04 000137" src="https://github.com/user-attachments/assets/e71f0cdd-0352-4efb-9fb8-06ffad4069c0" />
<img width="1670" height="929" alt="Screenshot 2026-10-04 000100" src="https://github.com/user-attachments/assets/eeac38d7-6267-4fe3-927e-001626767825" />
<img width="1668" height="931" alt="Screenshot 2026-10-04 000051" src="https://github.com/user-attachments/assets/16570aef-8358-4f29-a76b-78dce8700313" />
<img width="1674" height="936" alt="Screenshot 2026-10-04 000019" src="https://github.com/user-attachments/assets/5dc76b59-28ed-4d2a-bde1-d19707bbf3b4" />

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


