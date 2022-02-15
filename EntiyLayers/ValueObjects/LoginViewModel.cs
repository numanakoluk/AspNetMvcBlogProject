﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntiyLayers.ValueObjects
{
    public class LoginViewModel
    {
        //ViewModeller bu şekilde oluştu.
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Username { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }
    }
}