namespace Notes.Api.Responses
{
    /// <summary>
    /// return response T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponseError<T>
    {
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="error"></param>
        public ApiResponseError(T error)
        {
            this.Errors = error;
        }

        /// <summary>
        /// Attibute  T error
        /// </summary>
        public T Errors { get; set; }
    }
}
