namespace GymManagement.Domain
{
    public class GeneralResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static GeneralResponse<T> Succsess(T data, string message = null)
        {
            return new GeneralResponse<T> { Data = data, Message = message, Success = true };

        }
        public static GeneralResponse<T> ErrorResponse(string message, List<string>? errors = null)
        {
            return new GeneralResponse<T> { Message = message, Success = false };

        }
    }
}
