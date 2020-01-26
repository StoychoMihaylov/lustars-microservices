namespace AuthAPI.Services
{
    using AuthAPI.Data.Interfaces;

    public class Service
    {
        public Service(IAuthDBContext context)
        {
            this.Context = context;
        }

        protected IAuthDBContext Context { get; set; }
    }
}
