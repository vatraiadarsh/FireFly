using System.Text.Json;

namespace Utils
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}