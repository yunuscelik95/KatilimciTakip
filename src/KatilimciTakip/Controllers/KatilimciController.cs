using System.Web.Mvc;
using KatilimciTakip.Models;

namespace KatilimciTakip.Controllers
{
    public class KatilimciController : Controller
    {
        private KatilimciRepository _repo = new KatilimciRepository();

        public ActionResult Index(string arama)
        {
            ViewBag.Arama = arama;
            var katilimcilar = _repo.TumunuGetir(arama);
            return View(katilimcilar);
        }

        public ActionResult Ekle()
        {
            return View(new Katilimci());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Katilimci katilimci)
        {
            if (string.IsNullOrEmpty(katilimci.Telefon) && string.IsNullOrEmpty(katilimci.Eposta))
                ModelState.AddModelError("", "Telefon veya e-posta girin.");

            if (!ModelState.IsValid)
                return View(katilimci);

            _repo.Ekle(katilimci);
            TempData["Mesaj"] = "Katılımcı eklendi.";
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var katilimci = _repo.Getir(id);
            return View(katilimci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Katilimci katilimci)
        {
            if (string.IsNullOrEmpty(katilimci.Telefon) && string.IsNullOrEmpty(katilimci.Eposta))
                ModelState.AddModelError("", "Telefon veya e-posta girin.");

            if (!ModelState.IsValid)
                return View(katilimci);

            _repo.Guncelle(katilimci);
            TempData["Mesaj"] = "Katılımcı güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            _repo.Sil(id);
            TempData["Mesaj"] = "Katılımcı silindi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DavetGonder(int id, string kanal)
        {
            var katilimci = _repo.Getir(id);

            if (kanal == "SMS" && string.IsNullOrEmpty(katilimci.Telefon))
            {
                TempData["Hata"] = "Telefon numarası yok.";
                return RedirectToAction("Index");
            }

            if (kanal == "Mail" && string.IsNullOrEmpty(katilimci.Eposta))
            {
                TempData["Hata"] = "E-posta adresi yok.";
                return RedirectToAction("Index");
            }

            _repo.DavetGonderildi(id, kanal);
            TempData["Mesaj"] = katilimci.AdSoyad + " kişisine " + kanal + " gönderildi.";
            return RedirectToAction("Index");
        }

        public ActionResult Gecmis(int id)
        {
            var model = new DavetGecmisiModel();
            model.Katilimci = _repo.Getir(id);
            model.Kayitlar = _repo.DavetGecmisi(id);
            return View(model);
        }
    }
}
