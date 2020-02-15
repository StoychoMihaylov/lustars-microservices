namespace WebGateway.App.Infrastructure.Authorization
{
    using System;

    public class User
    {
        private Guid id { get; set; }

        private string token { get; set; }

        public Guid Id
        {
            get { return this.id; }

            set
            {
                ValidateUserUdFormat(value);

                this.id = value; 
            }
        }

        public string Token
        {
            get { return this.token; }
            

            set 
            {
                if (value == string.Empty)
                {
                    throw new Exception("User 'token' can't be empty!");
                }

                this.token = value; 
            }
        }

        private void ValidateUserUdFormat(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new Exception("User 'id' can't be empty!");
            }

            Guid id;
            var isValid = Guid.TryParse(value.ToString(), out id);
            if (!isValid)
            {
                throw new Exception("The provided 'id' is not a valid Guid!");
            }
        }
    }
}
