namespace UniversityManagement.Core.ViewModels
{
    public class ResponseEntity<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
