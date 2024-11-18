# MarketOrderFlow
### Kullanılan Teknolojiler
>-**.NET Core 8:** Uygulama, modern web uygulamaları için .NET Core 8 kullanılarak geliştirilmiştir.<br>
-**Minimal API:** API geliştirme için Minimal API yapısı tercih edilmiştir.<br>
-**JWT (JSON Web Token):** Kullanıcı kimlik doğrulama için JWT kullanılır.<br>
-**Hangfire:** Arka planda zamanlanmış görevlerin yönetilmesi için Hangfire kullanılmıştır.<br>
-**Serilog:** Loglama işlemleri için Serilog entegrasyonu yapılmıştır.<br>
-**MediatR:** API katmanında MediatR design pattern kullanılmıştır.<br>
-**Domain-Driven Design (DDD):** Domain katmanı DDD prensiplerine göre tasarlanmıştır.

### Mimariler ve Desenler
###### 1. Minimal API
API, basit ve hızlı geliştirme için Minimal API yapısı kullanılarak oluşturulmuştur. Bu, daha az kodla API endpoint'lerini tanımlamaya imkân tanır.

###### 2. Hangfire
Zamanlanmış ve arka planda çalışan görevler Hangfire ile yönetilmektedir. Bu, otomatikleştirilmiş görevlerin düzenli aralıklarla çalıştırılmasına olanak sağlar.

###### 3. MediatR ve CQRS
MediatR deseni, komutlar ve sorgular üzerinden işlem yapılmasını sağlayarak uygulamanın daha sürdürülebilir ve test edilebilir olmasını sağlar.

###### 4. Domain-Driven Design (DDD)
İş mantığı ve domain modelleri, DDD prensiplerine göre tasarlanarak uygulamanın iş kuralları ve veri yapıları güçlü bir şekilde yapılandırılmıştır.