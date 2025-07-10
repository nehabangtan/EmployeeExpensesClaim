using EmployeeExpensesClaim.ViewModels;

namespace EmployeeExpensesClaim.Helpers
{
    public static class ResponseHelper
    {
        public static ResponseViewModel<T> Success<T>(string message, T data)
        {
            return new ResponseViewModel<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseViewModel<T> Failure<T>(string message, string? error = null)
        {
            return new ResponseViewModel<T>
            {
                Status = false,
                Message = message,
                Error = error
            };
        }
    }

}
