using ERAMS.API.Enum;

namespace ERAMS.API.Helper
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> CreateResponse<T>( ApiResponseStatus status, string message,  T? data = default)
        {
            return status switch
            {
                ApiResponseStatus.Success =>
                    new ApiResponse<T>(true, message, data, 200),

                ApiResponseStatus.BadRequest =>
                    new ApiResponse<T>(false, message, data, 400),

                ApiResponseStatus.NotFound =>
                    new ApiResponse<T>(false, message, data, 404),

                ApiResponseStatus.Error =>
                    new ApiResponse<T>(false, message, data, 500),

                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
