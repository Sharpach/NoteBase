using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IRO.Task.NoteBase.PL.WebApp.Models
{
    public class NoteViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        [DisplayName("Текст заметки")]
        [Required(ErrorMessage = "Введите текст заметки")]
        public string Text { get; set; }

        [HiddenInput]
        public virtual long ParentBookId { get; set; }
    }
}
