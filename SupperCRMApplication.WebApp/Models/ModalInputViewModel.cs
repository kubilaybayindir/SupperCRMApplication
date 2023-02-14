namespace SupperCRMApplication.WebApp.Models
{
    public class ModalInputViewModel
    {
        public bool IsReadonly { get; set; } = false;
        public string Description { get; set; } = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
        public bool HasId { get; set; } = false;
    }

    public class ModalInputIssueViewModel:ModalInputViewModel
    {
        public bool HasCompletedField { get; set; } = false;
    }
}
