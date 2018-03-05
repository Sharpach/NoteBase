namespace IRO.Task.NoteBase.PL.WebApp.Models
{
    public class ModalFooter
    {
        public string SubmitButtonText { get; set; } = "Ок";
        public string CancelButtonText { get; set; } = "Отмена";
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
        public bool OnlyCancelButton { get; set; }
    }
}