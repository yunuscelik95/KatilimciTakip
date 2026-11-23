using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace KatilimciTakip.Models
{
    public class KatilimciRepository
    {
        private string _baglantiCumlesi = ConfigurationManager.ConnectionStrings["KatilimciDb"].ConnectionString;

        public List<Katilimci> TumunuGetir(string arama)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                string sql = @"SELECT * FROM Katilimcilar
                               WHERE AdSoyad LIKE @Arama OR Telefon LIKE @Arama OR Eposta LIKE @Arama
                               ORDER BY Id DESC";

                return db.Query<Katilimci>(sql, new { Arama = "%" + arama + "%" }).ToList();
            }
        }

        public Katilimci Getir(int id)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                return db.QueryFirstOrDefault<Katilimci>("SELECT * FROM Katilimcilar WHERE Id = @Id", new { Id = id });
            }
        }

        public void Ekle(Katilimci katilimci)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                db.Execute("INSERT INTO Katilimcilar (AdSoyad, Telefon, Eposta) VALUES (@AdSoyad, @Telefon, @Eposta)", katilimci);
            }
        }

        public void Guncelle(Katilimci katilimci)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                db.Execute("UPDATE Katilimcilar SET AdSoyad = @AdSoyad, Telefon = @Telefon, Eposta = @Eposta WHERE Id = @Id", katilimci);
            }
        }

        public void Sil(int id)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                db.Execute("DELETE FROM DavetLog WHERE KatilimciId = @Id", new { Id = id });
                db.Execute("DELETE FROM Katilimcilar WHERE Id = @Id", new { Id = id });
            }
        }

        public void DavetGonderildi(int id, string kanal)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                db.Execute("UPDATE Katilimcilar SET DavetKanali = @Kanal, DavetTarihi = GETDATE() WHERE Id = @Id", new { Id = id, Kanal = kanal });
                db.Execute("INSERT INTO DavetLog (KatilimciId, Kanal) VALUES (@Id, @Kanal)", new { Id = id, Kanal = kanal });
            }
        }

        public List<DavetKaydi> DavetGecmisi(int katilimciId)
        {
            using (var db = new SqlConnection(_baglantiCumlesi))
            {
                return db.Query<DavetKaydi>("SELECT * FROM DavetLog WHERE KatilimciId = @Id ORDER BY Id DESC", new { Id = katilimciId }).ToList();
            }
        }
    }
}
