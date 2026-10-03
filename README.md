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
<img width="1668" height="931" alt="Screenshot 2026-10-04 000051" src="https://github.com/user-attachments/assets/051d82da-566a-4448-94b9-71109cc1b1b2" />
<img width="1674" height="936" alt="Screenshot 2026-10-04 000019" src="https://github.com/user-attachments/assets/d25a7d68-d28a-48c1-b5d4-4f9a92ca4939" />
<img width="1668" height="934" alt="Screenshot 2026-10-04 000003" src="https://github.com/user-attachments/assets/98bb8f56-03b0-4d45-b509-a54f38c805b8" />
<img width="1682" height="934" alt="Screenshot 2026-10-03 235956" src="https://github.com/user-attachments/assets/d8651c8b-fc0d-4d8a-a464-84e8079bfa7a" />
<img width="1675" height="919" alt="Screenshot 2026-10-04 000149" src="https://github.com/user-attachments/assets/b21b05b5-3ce3-4bd9-a23c-00f248988cca" />
<img width="1671" height="935" alt="Screenshot 2026-10-04 000142" src="https://github.com/user-attachments/assets/f65502ff-6d0a-4da1-9f07-f9af5ae17df7" />
<img width="1668" height="928" alt="Screenshot 2026-10-04 000137" src="https://github.com/user-attachments/assets/c3701aca-ba78-4e0a-8dac-8da2c16f4075" />
<img width="1670" height="929" alt="Screenshot 2026-10-04 000100" src="https://github.com/user-attachments/assets/69f0568f-29f2-4a1e-be14-b9fc40855f23" />

