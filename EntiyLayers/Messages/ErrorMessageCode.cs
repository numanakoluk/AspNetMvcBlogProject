using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers.Messages
{
    //Hata Mesajları Yarın bir gün farklı dillerde hata mesajları için
    public enum  ErrorMessageCode
    {
       
        KullanıcıAdiKayitli =101,
        EpostaAdresiKayitli=102,
        KullaniciAktifDegil =151,
        KullaniciyadaSifreHatali=152,
        MailKontrol = 153,
        KullaniciAktif = 154,
        AktifKullaniciBulunamadi=155,
        KullaniciBulunamadi =156,
        ProfilGuncellenemedi = 157,
        KullaniciSilinemedi = 158,
        KullaniciEklenemedi = 159,
        KullaniciGuncellenemedi = 160
    }
}
