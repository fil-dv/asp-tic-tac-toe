using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Tic_tac_toe.Models.AccountModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(255, ErrorMessage = "Пароль должен содержать не мение 8-ми символов", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        //[StringLength(255, ErrorMessage = "Минимум 7 символов", MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение:")]
        [Compare("Password", ErrorMessage = "Введенные пароли должны совпадать")]
        public string PasswordRepeat { get; set; }
    }
}