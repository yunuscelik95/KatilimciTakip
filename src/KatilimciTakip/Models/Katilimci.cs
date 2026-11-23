using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KatilimciTakip.Models
{
    public class Katilimci
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        public string AdSoyad { get; set; }

        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string DavetKanali { get; set; }
        public DateTime? DavetTarihi { get; set; }
    }

    public class DavetKaydi
    {
        public int Id { get; set; }
        public int KatilimciId { get; set; }
        public string Kanal { get; set; }
        public DateTime DavetTarihi { get; set; }
    }

    public class DavetGecmisiModel
    {
        public Katilimci Katilimci { get; set; }
        public List<DavetKaydi> Kayitlar { get; set; }
    }
}
