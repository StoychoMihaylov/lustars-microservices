namespace ProfileAPI.Services.Services
{
    using System;
    using System.Linq;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using ProfileAPI.Services.Interfaces;

    public class ImageService : Service, IImageService
    {
        public ImageService(IProfileDBContext context)
            : base(context)
        { }

        public bool CreateNewUserProfileImage(Guid userId, string imageUrl)
        {
            try
            {
                var imgs = new List<Image>()
                {
                    new Image()
                    {
                        Url = imageUrl,
                        UploadedOn = DateTime.UtcNow
                    }
                };

                var user = this.Context
                    .UserProfiles
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                user.Images = imgs;

                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool SaveUserProfileAvatarImage(Guid userId, string imageUrl)
        {
            try
            {
                var user = this.Context
                .UserProfiles
                .Where(u => u.Id == userId)
                .First();

                user.AvatarImage = imageUrl;

                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool DeleteUserProfileImage(Guid userGuidId, long imageGuidId)
        {
            try
            {
                var user = this.Context
                   .UserProfiles
                   .Include(u => u.Images)
                   .Where(u => u.Id == userGuidId)
                   .First();

                var imageToBeRemoved = user
                    .Images
                    .Where(i => i.Id == imageGuidId)
                    .First();

                user.Images.Remove(imageToBeRemoved);

                this.Context.UserProfiles.Update(user);
                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string GetCurrentUserAvatarImageUrl(Guid guidId)
        {
            return this.Context
                .UserProfiles
                .AsNoTracking()
                .Where(u => u.Id == guidId)
                .Select(u => u.AvatarImage)
                .FirstOrDefault();
        }
    }
}
