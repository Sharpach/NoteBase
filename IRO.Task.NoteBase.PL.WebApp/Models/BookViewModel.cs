using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IRO.Task.NoteBase.PL.WebApp.Models
{
    public class BookViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        [DisplayName("Название книги")]
        [Required(ErrorMessage = "Введите название книги")]
        public string Name { get; set; }

        [HiddenInput]
        public virtual long OwnerId { get; set; }

        [DisplayName("Владелец")]
        public string OwnerName { get; set; }

        public List<NoteViewModel> Notes { get; set; }
    }
}
