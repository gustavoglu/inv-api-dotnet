namespace Inv.Services.Api.Models
{
    public class ApiResponse
    {
        public ApiResponse(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        public ApiResponse(object data)
        {
            Success = true;
            Data = data;
        }

        public bool Success { get; set; }
        public object Data { get; set; }
    }
}
