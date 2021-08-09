namespace Notes.Api.Responses
{
    /// <summary>
    /// return type response T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="data">T</param>
        public ApiResponse(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Attibute  T data
        /// </summary>
        public T Data { get; set; }

    }
}
