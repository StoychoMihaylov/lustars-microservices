namespace ProfileAPI.App
{
    using AutoMapper;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Models.ViewModels;
    using ProfileAPI.Models.BidingModels;

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserProfile, UserProfileDetailedDataViewModel>();
            CreateMap<EditUserProfileBindingModel, UserProfile>();
        }
    }
}
