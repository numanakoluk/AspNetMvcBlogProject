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
        UsernameAlreadyExists =101,
        EmailAlredyExists=102,
        UserIsNotActive =151,
        UsernamaOrPassWrong=152,
        CheckYourEmail = 153
    }
}
