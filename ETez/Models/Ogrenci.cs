using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETez.Models {
    public class Ogrenci {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string OkulNo { get; set; }

        public string TezAdi { get; set; }

        public string TezKonusu { get; set; }

        public DateTime BitisTarihi { get; set; }

        public string Sifre { get; set; }

        public string TCKimlikNo { get; set; }

        public bool Onay { get; set; }

        public int OgretmenId { get; set; }

        public string DosyaYolu { get; set; }
        public alani Alani { get; set; }

    }

    public enum alani
    {
        BilgisayarAğları,
        SistemGüvenliği,
        ERP,
        MobilUygulama,
        Girişimcilik

    }
}