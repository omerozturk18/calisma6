using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETez.Models {
    public class Ogretmen {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string TCKimlikNo { get; set; }

        public string TelefonNo { get; set; }

        public string Sifre { get; set; }

        public alan Alani { get; set; }
       
    }

    public enum alan
    {
        BilgisayarAğları,
        SistemGüvenliği,
        ERP,
        MobilUygulama,
        Girişimcilik

    }
}
