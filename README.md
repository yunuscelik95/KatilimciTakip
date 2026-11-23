# KatilimciTakip

ASP.NET MVC 5 (.NET Framework 4.8), Dapper ve MSSQL ile yazılmış basit bir katılımcı takip uygulaması.

Katılımcılara SMS ya da mail daveti gönderilmiş gibi kayıt tutulur (gerçek SMS / mail gönderilmez).

## Özellikler

- Katılımcı ekleme, düzenleme, silme ve arama
- SMS / Mail daveti kaydetme
- Katılımcının davet geçmişi

## Kurulum

1. `Database/KatilimciDb.sql` dosyasını SSMS'te çalıştırın.
2. Bağlantı cümlesi `src/KatilimciTakip/Web.config` içinde. Farklıysa değiştirin.
3. `KatilimciTakip.sln` dosyasını Visual Studio ile açıp F5'e basın.
