using ETez.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETez.Controllers {
    public class GirisController : Controller {
        public static string conString = ConfigurationManager.ConnectionStrings["DBAccess"].ConnectionString;
        OleDbConnection baglanti = new OleDbConnection(conString);

        public ActionResult Index() {
            return View();
        }

        public ActionResult Ogrenci() {
            return View();
        }
        [HttpPost]
        public ActionResult OgrenciGiris(string tckNo, string sifre) {
            Ogrenci ogrenci = new Ogrenci();

            string sql = $"select * from Ogrenci where TCKimlikNo='{tckNo}' and Sifre='{sifre}'";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();

            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                if (!dataReader.Read()) {
                    return RedirectToAction("Ogrenci", "Giris");
                }
                Session["Kullanici"] = "Ogrenci";
                if (dataReader.HasRows) {
                    ogrenci.Id = Convert.ToInt32(dataReader["Id"]);
                    ogrenci.Adi = Convert.ToString(dataReader["Adi"]);
                    ogrenci.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                    ogrenci.OkulNo = Convert.ToString(dataReader["OkulNo"]);
                    ogrenci.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                    ogrenci.Sifre = Convert.ToString(dataReader["Sifre"]);
                }
                Session["Ogrenci"] = ogrenci;
                baglanti.Close();
            }
            return RedirectToAction("Index", "Ogrenci", new { id = ogrenci.Id });
        }

        [HttpPost]
        public ActionResult CikisYap() {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Ogretmen() {
            return View();
        }
        [HttpPost]
        public ActionResult OgretmenGiris(string tckNo, string sifre) {
            Ogretmen ogretmen = new Ogretmen();

            string tckno = tckNo;
            string sifree = sifre;
            string sql = String.Format("Select * From Ogretmen where TCKimlikNo='{0}' and Sifre='{1}'", tckNo, sifre);

            OleDbCommand command = new OleDbCommand(sql, baglanti);
            baglanti.Open();
            using (OleDbDataReader dataReader = command.ExecuteReader()) {
                if (!dataReader.Read()) {
                    return RedirectToAction("Ogretmen", "Giris");
                } else {
                    Session["Kullanici"] = "Ogretmen";
                    if (dataReader.HasRows) {
                        ogretmen.Id = Convert.ToInt32(dataReader["Id"]);
                        ogretmen.Adi = Convert.ToString(dataReader["Adi"]);
                        ogretmen.Soyadi = Convert.ToString(dataReader["Soyadi"]);
                        ogretmen.TelefonNo = Convert.ToString(dataReader["TelefonNo"]);
                        ogretmen.TCKimlikNo = Convert.ToString(dataReader["TCKimlikNo"]);
                        ogretmen.Sifre = Convert.ToString(dataReader["Sifre"]);
                    }
                    Session["Ogretmen"] = ogretmen;
                    baglanti.Close();
                }
            }
            return RedirectToAction("Index", "Ogretmen", new { id = ogretmen.Id });
        }

        public ActionResult Admin() {
            return View();
        }
        [HttpPost]
        public ActionResult AdminGiris(string kullaniciAdi, string sifre) {
            if (kullaniciAdi == "Admin" && sifre == "123") {
                Session["Kullanici"] = "Admin";
                return RedirectToAction("Index", "Admin");
            } else {
                return RedirectToAction("Admin", "Giris");
            }
        }
    }
}