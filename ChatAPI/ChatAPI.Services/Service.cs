namespace ChatAPI.Services
{
    using ChatAPI.Data.Interfaces;

    public class Service
    {
        public Service(IChatDBContext context)
        {
            this.Context = context;
        }

        protected IChatDBContext Context { get; set; }
    }
}
