using ETez.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETez.Controllers
{
    public class OgretmenController : Controller
    {
        public static string conString = ConfigurationManager.ConnectionStrings["DBAccess"].ConnectionString;
        OleDbConnection baglanti = new OleDbConnection(conString);

        public ActionResult Index(int id)
        {
            List<Ogrenci> ogrenciList = new List<Ogrenci>();
            bool onay = false;
            string sql = $"select * from Ogrenci where Onay={onay} and OgretmenId={id} ";
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
                    ogrenci.Onay = Convert.ToBoolean(dataReader["Onay"]);

                    ogrenciList.Add(ogrenci);
                }
                baglanti.Close();
            }
            return View(ogrenciList);
        }

        public ActionResult StajOnayla(int id) {
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
                    ogrenci.Onay = Convert.ToBoolean(dataReader["Onay"]);
                }
                baglanti.Close();
            }
            return View(ogrenci);
        }

        [HttpPost]
        public ActionResult TezOnayla(int? id) {
            if (ModelState.IsValid) {
                bool onay = true;
                Ogretmen ogretmen = (Ogretmen)Session["Ogretmen"];
                string sql = $"UPDATE Ogrenci SET Onay={onay} WHERE Id={id}";

                using (OleDbCommand command = new OleDbCommand(sql, baglanti)) {
                    command.CommandType = System.Data.CommandType.Text;
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    baglanti.Close();
                }
                return RedirectToAction("OnaylananOgrenciler", "Ogretmen");
            }
            return View();
        }

        public ActionResult OnaylananOgrenciler() {
            List<Ogrenci> ogrenciList = new List<Ogrenci>();
            bool onay = true;
            string sql = $"select * from Ogrenci where Onay={onay}";
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
                    if (dataReader["TezAdi"] != DBNull.Value)
                    {
                        ogrenci.TezAdi = Convert.ToString(dataReader["TezAdi"]);
                        ogrenci.TezKonusu = Convert.ToString(dataReader["BaslangicTarihi"]);
                        ogrenci.BitisTarihi = Convert.ToDateTime(dataReader["BitisTarihi"]);
                    }
                    ogrenci.Sifre = Convert.ToString(dataReader["Sifre"]);
                    ogrenci.Onay = Convert.ToBoolean(dataReader["Onay"]);
                    ogrenciList.Add(ogrenci);
                }
                baglanti.Close();
            }
            return View(ogrenciList);
        }
    }
}