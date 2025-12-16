# 🚗 CQRS Rent-A-Car Project

Modern CQRS mimarisi ve AI destekli özellikler ile geliştirilmiş araç kiralama yönetim sistemi.

## 📋 Proje Hakkında

Bu proje, **CQRS (Command Query Responsibility Segregation)** pattern kullanarak yazma ve okuma işlemlerini ayıran, Google Gemini AI entegrasyonu ile akıllı özellikler sunan ve RapidAPI servisleri ile zenginleştirilmiş kapsamlı bir araç kiralama sistemidir.

## ✨ Öne Çıkan Özellikler

### Kullanıcı Tarafı
- 📅 **Tarih Bazlı Araç Arama** - Seçilen tarihlerde müsait araçları listeleme
- ✈️ **Havalimanı Entegrasyonu** - RapidAPI ile Türkiye ve dünya havalimanları
- 💰 **Akıllı Maliyet Hesaplama** - Mesafe, yakıt tüketimi ve güncel fiyatlarla otomatik hesaplama
- 🗺️ **Rota Analizi** - İki lokasyon arası km ve tahmini süre
- 📧 **Otomatik Bildirimler** - SMTP ile email gönderimi

### Admin Paneli
- 🤖 **AI Araç Asistanı** - Google Gemini ile akıllı araç önerileri
- ⛽ **Gerçek Zamanlı Yakıt Fiyatları** - Benzin, motorin, LPG fiyat widget'ı
- 💬 **AI Destekli Mesajlaşma** - Otomatik müşteri mesaj cevaplama
- 📊 **CQRS İstatistikler** - Performanslı raporlama ve analiz
- 👥 **Personel Yönetimi** - Departman ve pozisyon bazlı organizasyon

## 🛠️ Teknoloji Stack

**Backend:** ASP.NET Core MVC, CQRS (MediatR), Entity Framework Core, SQL Server

**Frontend:** HTML5, CSS3, SCSS, JavaScript, Bootstrap

**AI & APIs:**
- Google Gemini AI (Araç önerileri, otomatik mesaj cevaplama)
- RapidAPI (Havalimanları, lokasyon, mesafe hesaplama, yakıt fiyatları)
- SMTP Email Service (Otomatik bildirimler)


## 💡 CQRS Mimarisi

### Commands (Yazma)

public class CreateReservationCommand : IRequest<ReservationDto>
{
    public int CarId { get; set; }
    public DateTime PickupDate { get; set; }
    public string PickupLocation { get; set; }
  
}


### Queries (Okuma)

public class GetAvailableCarsQuery : IRequest<List<CarDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
}

## 🎯 Temel İşleyiş

### Araç Kiralama Akışı
1. Kullanıcı tarih ve havalimanı seçer
2. Sistem müsait araçları listeler
3. Kullanıcı araç seçer ve lokasyonları belirler
4. **RapidAPI** ile mesafe ve yakıt maliyeti hesaplanır
5. Rezervasyon oluşturulur
6. **SMTP** ile onay maili gönderilir

### AI Mesaj Sistemi
1. Kullanıcı anasayfadan mesaj gönderir
2. Mesaj admin paneline kaydedilir
3. **Google Gemini AI** otomatik cevap oluşturur
4. Cevap hem panelde gösterilir hem de email olarak gönderilir

### Maliyet Hesaplama

Antalya → Trabzon
📍 1172.4 km
⏱️ 14 saat 39 dakika
⛽ Yakıt: ₺4,513.92
➕ Ekstra: ₺586.22
💵 Toplam: ₺5,100.14


## 👤 Geliştirici

**Nurdan Öz**
- GitHub: [@NurdanOz](https://github.com/NurdanOz)
- LinkedIn: [Nurdan Öz](https://linkedin.com/in/nurdanoz)

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

---

⭐ Eğer projeyi beğendiyseniz yıldız vermeyi unutmayın!


📊 Ekran Görüntüleri
<img width="1920" height="1024" alt="ARAÇÖNERİ1" src="https://github.com/user-attachments/assets/7110c244-ec05-4d38-8b0f-092c948b8715" />
<img width="1920" height="1014" alt="ARAÇÖNERİ2" src="https://github.com/user-attachments/assets/807158f4-d7e3-4550-85db-50024c5c07f9" />
<img width="1920" height="1004" alt="MesajlarAI-1" src="https://github.com/user-attachments/assets/0517f67f-e762-45fa-9b06-aeec13e96a45" />
<img width="1920" height="1019" alt="MesajlarAI-2" src="https://github.com/user-attachments/assets/558a7246-4aca-4a3f-84cf-fb38e7e36e79" />
<img width="1920" height="1017" alt="Ekran görüntüsü 2025-12-16 004629" src="https://github.com/user-attachments/assets/40ce50b9-0412-4b1c-ae88-e44b653fc26b" />
<img width="1920" height="1021" alt="Ekran görüntüsü 2025-12-16 004654" src="https://github.com/user-attachments/assets/c4b88c61-f62c-48c2-be3c-ab4ea74d27a0" />
<img width="1920" height="1007" alt="AirPort" src="https://github.com/user-attachments/assets/a9ae2653-b6e3-4c72-88a1-6900cf42a80c" />
<img width="1920" height="979" alt="DashboardVeri" src="https://github.com/user-attachments/assets/9df1b94f-a198-40da-8d12-e31ede191cde" />
<img width="1920" height="1022" alt="xDashboard1" src="https://github.com/user-attachments/assets/4c1baded-62d6-48ab-963f-6b8a6d607b66" />
<img width="1920" height="1019" alt="xDashboard2" src="https://github.com/user-attachments/assets/eb32b586-c001-45ba-8651-c7927dbf6f01" />
<img width="1920" height="1015" alt="xDashboard3" src="https://github.com/user-attachments/assets/344206b6-7522-4d32-92d0-2b8739375d4d" />
<img width="1920" height="1013" alt="xDashboard4" src="https://github.com/user-attachments/assets/1f065d28-5c2a-4c44-9895-950b767fad68" />
<img width="1920" height="1019" alt="xDefault1" src="https://github.com/user-attachments/assets/bcce012c-a44e-45f6-a5be-1b447ee81037" />
<img width="1920" height="945" alt="xDefault2" src="https://github.com/user-attachments/assets/bb31c388-aa56-491a-ae5d-f899fe4590de" />
<img width="1920" height="1022" alt="xDefault3" src="https://github.com/user-attachments/assets/829f38a4-237a-4ecc-8abf-d19e096905f0" />
<img width="1920" height="1000" alt="xdefault4" src="https://github.com/user-attachments/assets/672aef31-61d3-4d4d-a9a1-c330ecb588e8" />
<img width="1920" height="1013" alt="xDefault5" src="https://github.com/user-attachments/assets/16d9ea9e-cf8c-400e-b66b-488b559a3f0a" />
<img width="1920" height="1010" alt="xDefault6" src="https://github.com/user-attachments/assets/7c779d16-d682-44f0-a116-ab4c51624fe5" />

