namespace ProfileAPI.Services
{
    using ProfileAPI.Data.Interfaces;

    public class Service
    {
        public Service(IProfileDBContext context)
        {
            this.Context = context;
        }

        protected IProfileDBContext Context { get; set; }
    }
}
