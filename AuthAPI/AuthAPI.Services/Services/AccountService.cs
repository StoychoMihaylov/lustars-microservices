﻿namespace AuthAPI.Services.Services
{
    using System;
    using System.Linq;
    using AuthAPI.Data.Entities;
    using System.Threading.Tasks;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Utilities;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Services.Interfaces;
    using System.Security.Cryptography;
    using Microsoft.EntityFrameworkCore;

    public class AccountService : Service, IAccountService
    {
        public AccountService(IAuthDBContext context)
            : base (context) { }

        public async Task<bool> CheckIfUserExist(RegisterUserBindingModel bm)
        {   
            var user = await this.Context
                .Users
                .Where(user => user.Email == bm.Email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<AccountCredentialsViewModel> CreateNewUserAccount(RegisterUserBindingModel bm)
        {

            //var passwordHashAndSalt = this.GenerateSaltedHash(bm.Password); // Returns byte[][] array of 2 elements(hashed password and salt)

            //User newUser = new User() 
            //{
            //    Name = bm.Name,
            //    Email = bm.Email,
            //    PasswordHash = Convert.ToBase64String(passwordHashAndSalt[0]),
            //    Salt = Convert.ToBase64String(passwordHashAndSalt[1]),
            //};

            //await this.Context.Users.AddAsync(newUser);
            //await this.Context.SaveChangesAsync();


            //// After user has been created login the user (return token)
            //LoginUserBindingModel loginBm = new LoginUserBindingModel()
            //{
            //    Email = bm.Email,
            //    Password = bm.Password
            //};

            //return await LoginUser(loginBm);

            AccountCredentialsViewModel viewModel = new AccountCredentialsViewModel()
            {
                UserId = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                Name = "TEST",
                Email = "test@abv.bg"
            };

            return viewModel;
        }

        public void DeleteUserToken(LogoutBindingModel bm)
        {
            var token = this.Context
                .Tokens
                .AsNoTracking()
                .Where(t => t.Value == bm.Token)
                .First();

            this.Context.Tokens.Remove(token);
            this.Context.SaveChanges();
        }

        private byte[][] GenerateSaltedHash(string userPassword)
        {
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] password = userPassword.Select(character => (byte)character).ToArray();
                var salt = this.GenerateSalt();
                var saltedPassword = new byte[password.Length + salt.Length];

                password.CopyTo(saltedPassword, 0);
                salt.CopyTo(saltedPassword, password.Length);

                var saltedPasswordHash = sha256.ComputeHash(saltedPassword);

                // return passwordHash and salt in double array to store it in User
                var result = new byte[][] { saltedPasswordHash, salt };

                return result;
            }
        }

        private byte[] GenerateSalt()
        {
            using (RandomNumberGenerator random = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                random.GetNonZeroBytes(salt);
                return salt;
            }
        }

        public async Task<AccountCredentialsViewModel> LoginUser(LoginUserBindingModel bm)
        {
            string tokenBearer = string.Empty;

            var user = await this.Context
                .Users
                .Where(u => u.Email == bm.Email)
                .FirstOrDefaultAsync();

            if (user == null ) return null;
        
            // taking the user data to send it to the client
            var userId = user.Id; 
            var name = user.Name;
            var email = user.Email;

            var passwordHash = GenerateHashOfPassword(bm.Password, user.Salt);

            if (user.PasswordHash == passwordHash)
            {
                tokenBearer = TokenGenerator.Generate(30);
                TokenManager newToken = new TokenManager()
                {
                    Value = tokenBearer,
                    CreatedOn = DateTime.Now,
                };
                newToken.User = user;

                this.Context.Tokens.Add(newToken);
                this.Context.SaveChanges();
            }
            else
            {
                throw new Exception("Account Login fail!");
            }
      
            AccountCredentialsViewModel viewModel = new AccountCredentialsViewModel()
            {
                UserId = userId,
                Token = tokenBearer,
                Name = name,
                Email = email
            };

            return viewModel;
        }

        private string GenerateHashOfPassword(string password, string salt)
        {
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] passwordInBytes = password.Select(character => (byte)character).ToArray();
                byte[] saltInBytes = Convert.FromBase64String(salt);
                var saltedPassword = new byte[passwordInBytes.Length + saltInBytes.Length];

                passwordInBytes.CopyTo(saltedPassword, 0);
                saltInBytes.CopyTo(saltedPassword, passwordInBytes.Length);
                var saltedPasswordHash = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(saltedPasswordHash);
            }
        }

        public UserCredentials CheckIfTokenIsValidAndReturnUserCredentials(Token bm)
        {
            var userId = Guid.Empty;

            try
            {
                userId = this.Context
                   .Tokens
                   .AsNoTracking()
                   .Where(t => t.Value == bm.Value)
                   .Select(t => t.User.Id)
                   .First();
            }
            catch
            {
                return null;
            }
     
            return new UserCredentials() { UserId = userId, Token = bm.Value };
        }

        public bool DeleteUser(AccountCredentialsViewModel accountCredentialsVm)
        {
            try
            {
                var user = this.Context
                .Users
                .Find(accountCredentialsVm.UserId);

                this.Context.Users.Remove(user);
                this.Context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
