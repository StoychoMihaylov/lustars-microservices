namespace WebGateway.App.Authorization
{
    using System;

    public class User
    {
        private Guid id { get; set; }

        private string token { get; set; }

        public Guid Id
        {
            get { return this.id; }

            set { this.id = value; }
        }

        public string Token
        {
            get { return this.token; }
            

            set { this.token = value; }
        }
    }
}
