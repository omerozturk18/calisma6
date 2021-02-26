using ETez.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Mvc;

namespace ETez.Controllers {
    public class AdminController : Controller {

        public static string conString = ConfigurationManager.ConnectionStrings["DBAccess"].ConnectionString;
        OleDbConnection baglanti = new OleDbConnection(conString);

        public ActionResult Index() {
            return RedirectToAction("Ogretmenler","Admin");
        }

        //ÖĞRETMEN
        public ActionResult Ogretmenler() {
            List<Ogretmen> ogretmenList = new List<Ogretmen>();

            string sql = "select * from Ogretmen";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    string bolum = "";
                    Ogretmen ogretmen = new Ogretmen();
                    ogretmen.Id = Convert.ToInt32(dataReader["Id"]);
                    ogretmen.Adi = Convert.ToString(dataReader["Adi"]);
                    ogretmen.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogretmen.TelefonNo = Convert.ToString(dataReader["TelefonNo"]);
                    ogretmen.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogretmen.Sifre = Convert.ToString(dataReader["Sifre"]);
                    ogretmenList.Add(ogretmen);
                }
            }
            baglanti.Close();
            return View(ogretmenList);
        }

        public ActionResult OgretmenDetay(int id) {
            Ogretmen ogretmen = new Ogretmen();

            string sql = $"select * from Ogretmen where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    string bolum = " ";
                    ogretmen.Id = Convert.ToInt32(dataReader["Id"]);
                    ogretmen.Adi = Convert.ToString(dataReader["Adi"]);
                    ogretmen.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogretmen.TelefonNo = Convert.ToString(dataReader["TelefonNo"]);
                    ogretmen.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogretmen.Sifre = Convert.ToString(dataReader["Sifre"]);
                }
                baglanti.Close();
            }
            return View(ogretmen);
        }

        public ActionResult OgretmenEkle() {
            return View();
        }

        [HttpPost]
        public ActionResult OgretmenEkle(Ogretmen ogretmen) {
            if (ModelState.IsValid) {
                string sql = $"insert into Ogretmen(Adi,Soyadi,TCKimlikNo,TelefonNo,Sifre,Alani) values('{ogretmen.Adi}'," +
                  $"'{ogretmen.Soyadi}','{ogretmen.TCKimlikNo}','{ogretmen.TelefonNo}','{ogretmen.Sifre}','{ogretmen.Alani}')";
                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Ogretmenler", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult OgretmenSil(int id) {
            string sql = $"delete from Ogretmen where Id={id}";
            using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                baglanti.Open();
                try {
                    command.ExecuteNonQuery();
                } catch (Exception) {
                    throw;
                }
                baglanti.Close();
            }
            return RedirectToAction("Ogretmenler", "Admin");
        }

        public ActionResult OgretmenDuzenle(int id) {
            Ogretmen ogretmen = new Ogretmen();

            string sql = $"select * from Ogretmen where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    ogretmen.Id = Convert.ToInt32(dataReader["Id"]);
                    ogretmen.Adi = Convert.ToString(dataReader["Adi"]);
                    ogretmen.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogretmen.TelefonNo = Convert.ToString(dataReader["TelefonNo"]);
                    ogretmen.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogretmen.Sifre = Convert.ToString(dataReader["Sifre"]);
                }
            }
            baglanti.Close();

            return View(ogretmen);
        }

        [HttpPost]
        public ActionResult OgretmenDuzenle(Ogretmen ogretmen) {
            if (ModelState.IsValid) {
                string sql = $"UPDATE Ogretmen SET Adi='{ogretmen.Adi}', Soyadi='{ogretmen.Soyadi}',TCKimlikNo='{ogretmen.TCKimlikNo}',TelefonNo='{ogretmen.TelefonNo}',Sifre='{ogretmen.Sifre}' WHERE Id={ogretmen.Id}";
                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Ogretmenler", "Admin");
            }
            return View();
        }

        // ÖĞRENCİ
        public ActionResult Ogrenciler() {
            List<Ogrenci> ogrenciList = new List<Ogrenci>();

            string sql = "select * from Ogrenci";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    Ogrenci ogrenci = new Ogrenci();
                    ogrenci.Id = Convert.ToInt32(dataReader["Id"]);
                    ogrenci.Adi = Convert.ToString(dataReader["Adi"]);
                    ogrenci.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogrenci.OkulNo = Convert.ToString(dataReader["OkulNo"]);
                    ogrenci.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogrenci.Sifre = Convert.ToString(dataReader["Sifre"]);
                    ogrenciList.Add(ogrenci);
                }
            }
            baglanti.Close();
            return View(ogrenciList);
        }

        public ActionResult OgrenciDetay(int id) {
            Ogrenci ogrenci = new Ogrenci();

            string sql = $"select * from Ogrenci where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    ogrenci.Id = Convert.ToInt32(dataReader["Id"]);
                    ogrenci.Adi = Convert.ToString(dataReader["Adi"]);
                    ogrenci.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogrenci.OkulNo = Convert.ToString(dataReader["OkulNo"]);
                    ogrenci.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogrenci.Sifre = Convert.ToString(dataReader["Sifre"]);
                }
            }
            baglanti.Close();
            return View(ogrenci);
        }

        public ActionResult OgrenciEkle() {
            return View();
        }
        [HttpPost]
        public ActionResult OgrenciEkle(Ogrenci ogrenci) {
            if (ModelState.IsValid) {
                string sql = $"insert into Ogrenci(Adi,Soyadi,OkulNo,TCKimlikNo,Sifre) values('{ogrenci.Adi}','{ogrenci.Soyadi}','{ogrenci.OkulNo}','{ogrenci.TCKimlikNo}','{ogrenci.Sifre}')";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Ogrenciler", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult OgrenciSil(int id) {
            string sql = $"delete from Ogrenci where Id={id}";
            using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                baglanti.Open();
                try {
                    command.ExecuteNonQuery();
                } catch (Exception) {
                    throw;
                }
                baglanti.Close();
            }
            return RedirectToAction("Ogrenciler", "Admin");
        }

        public ActionResult OgrenciDuzenle(int id) {
            Ogrenci ogrenci = new Ogrenci();

            string sql = $"select * from Ogrenci where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    ogrenci.Id = Convert.ToInt32(dataReader["Id"]);
                    ogrenci.Adi = Convert.ToString(dataReader["Adi"]);
                    ogrenci.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogrenci.OkulNo = Convert.ToString(dataReader["OkulNo"]);
                    ogrenci.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogrenci.Sifre = Convert.ToString(dataReader["Sifre"]);
                }
            }
            baglanti.Close();
            return View(ogrenci);
        }
        [HttpPost]
        public ActionResult OgrenciDuzenle(Ogrenci ogrenci) {
            if (ModelState.IsValid) {
                string sql = $"UPDATE Ogrenci SET Adi='{ogrenci.Adi}',Soyadi='{ogrenci.Soyadi}',OkulNo='{ogrenci.OkulNo}',TCKimlikNo='{ogrenci.TCKimlikNo}',Sifre='{ogrenci.Sifre}' WHERE Id={ogrenci.Id}";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Ogrenciler", "Admin");
            }
            return View();
        }

    }
}