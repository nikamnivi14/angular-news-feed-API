namespace NewsFeedAPI.Model
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Result { get; set; } = false;

        public string Message { get; set; } = null;
    }
}
