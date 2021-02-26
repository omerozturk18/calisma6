using ETez.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETez.Controllers {
    public class OgrenciController : Controller {
        public static string conString = ConfigurationManager.ConnectionStrings["DBAccess"].ConnectionString;
        OleDbConnection baglanti = new OleDbConnection(conString);

        public ActionResult Index(int id) {
            Ogrenci ogrenci = new Ogrenci();

            string sql = $"select * from Ogrenci where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    ogrenci.Id = Convert.ToInt32(dataReader["Id"]);
                    ogrenci.Adi = Convert.ToString(dataReader["Adi"]);
                    ogrenci.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    if (dataReader["OgretmenId"] != DBNull.Value) {
                        ogrenci.OgretmenId = Convert.ToInt32(dataReader["OgretmenId"]);
                        ogrenci.Onay = Convert.ToBoolean(dataReader["Onay"]);
                    }
                    ogrenci.OkulNo = Convert.ToString(dataReader["OkulNo"]);
                    if (dataReader["TezKonusu"] != DBNull.Value) {
                        ogrenci.TezAdi = Convert.ToString(dataReader["TezAdi"]);
                        ogrenci.TezKonusu = Convert.ToString(dataReader["TezKonusu"]);
                    }
                    ogrenci.DosyaYolu = Convert.ToString(dataReader["DosyaYolu"]);
                    
                }
                baglanti.Close();
            }
            string sqlOgretmen = $"select * from Ogretmen where Id={ogrenci.OgretmenId}";
            OleDbCommand command2 = new OleDbCommand(sqlOgretmen, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command2.ExecuteReader()) {
                while (dataReader.Read()) {
                    ViewBag.Ogretmen = Convert.ToString(dataReader["Adi"]) + " " + Convert.ToString(dataReader["Soyadi"]);
                    
                }
                baglanti.Close();
            }
            return View(ogrenci);
        }

        public ActionResult TezEkle(int? id) {
            return View();
        }

        [HttpPost]
        public ActionResult TezEkle(int id, string TezAdi, string TezKonusu, string DosyaYolu, string Alani) {
     

            if (ModelState.IsValid) {
                string sql = $"UPDATE Ogrenci SET TezAdi='{TezAdi}',TezKonusu='{TezKonusu}',DosyaYolu='{DosyaYolu}',Alani='{Alani}' WHERE Id={id}";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Index", "Ogrenci", new { id });
            }
            return View();
        }

        public ActionResult TezDuzenle(int id) {
            Ogrenci stajOgr = new Ogrenci();

            string sql = $"select * from Ogrenci where Id={id}";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                while (dataReader.Read()) {
                    stajOgr.Id = Convert.ToInt32(dataReader["Id"]);
                    stajOgr.TezAdi = Convert.ToString(dataReader["TezAdi"]);
                    stajOgr.TezKonusu = Convert.ToString(dataReader["TezKonusu"]);
                    stajOgr.DosyaYolu = Convert.ToString(dataReader["DosyaYolu"]);
                }
            }
            baglanti.Close();
            return View(stajOgr);
        }
        [HttpPost]
        public ActionResult TezDuzenle(int id, Ogrenci ogrenci) {
            if (ModelState.IsValid) {
                string sql = $"UPDATE Ogrenci SET TezAdi='{ogrenci.TezAdi}',TezKonusu='{ogrenci.TezKonusu}',DosyaYolu='{ogrenci.DosyaYolu}' WHERE Id={id}";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Index", "Ogrenci", new { id });
            }
            return View();
        }
        [HttpPost]
        public ActionResult TezSil(int id, Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                string sql = $"UPDATE Ogrenci SET TezAdi='',TezKonusu='',DosyaYolu='' WHERE Id={id}";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("Index", "Ogrenci", new { id });
            }
            return View();
        }
    }
}