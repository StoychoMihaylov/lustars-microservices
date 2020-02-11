namespace WebGateway.Models.DTOs
{
    public class Token
    {
        private string value { get; set; }

        public Token(string token)
        {
            this.value = token;
        }

        public string Value { get { return this.value; } set { this.value = value; } }
    }
}
