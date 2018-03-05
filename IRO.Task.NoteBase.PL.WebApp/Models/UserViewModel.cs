using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IRO.Task.NoteBase.PL.WebApp.Models
{
    public class UserViewModel
    {
        [HiddenInput]
        public long Id { get; set; }
        
        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Remote(action: "CheckUserName", controller: "Users", ErrorMessage = "Это имя пользователя уже занято!")]
        public string Name { get; set; }
    }
}
