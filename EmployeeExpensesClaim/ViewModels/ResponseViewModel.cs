namespace EmployeeExpensesClaim.ViewModels
{
    public class ResponseViewModel<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? Error { get; set; }
    }


}