CREATE DATABASE KatilimciDb;
GO
USE KatilimciDb;
GO

CREATE TABLE Katilimcilar (
    Id          INT IDENTITY PRIMARY KEY,
    AdSoyad     NVARCHAR(100) NOT NULL,
    Telefon     NVARCHAR(20) NULL,
    Eposta      NVARCHAR(100) NULL,
    DavetKanali NVARCHAR(10) NULL,
    DavetTarihi DATETIME NULL
);

CREATE TABLE DavetLog (
    Id          INT IDENTITY PRIMARY KEY,
    KatilimciId INT NOT NULL,
    Kanal       NVARCHAR(10) NOT NULL,
    DavetTarihi DATETIME NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO Katilimcilar (AdSoyad, Telefon, Eposta) VALUES (N'Ayşe Yılmaz', '05321112233', 'ayse@ornek.com');
INSERT INTO Katilimcilar (AdSoyad, Telefon, Eposta) VALUES (N'Mehmet Demir', '05334445566', NULL);
INSERT INTO Katilimcilar (AdSoyad, Telefon, Eposta) VALUES (N'Zeynep Kaya', NULL, 'zeynep@ornek.com');
GO
