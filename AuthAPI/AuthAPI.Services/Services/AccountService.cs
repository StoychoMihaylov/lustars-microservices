﻿namespace AuthAPI.Services.Services
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using Microsoft.EntityFrameworkCore;

    using AuthAPI.Data.Entities;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Utilities;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Services.Interfaces;

    public class AccountService : Service, IAccountService
    {
        public AccountService(IAuthDBContext context)
            : base (context) { }

        public bool CheckIfUserExist(RegisterUserBindingModel bm)
        {
            try
            {
                this.Context
                    .Users
                    .Where(user => user.Email == bm.Email)
                    .First();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public AccountCredentialsViewModel CreateNewUserAccount(RegisterUserBindingModel bm)
        {
            try
            {
                var passwordHashAndSalt = this.GenerateSaltedHash(bm.Password); // Returns byte[][] array of 2 elements(hashed password and salt)

                User newUser = new User();
                newUser.Name = bm.Name;
                newUser.Email = bm.Email;
                newUser.PasswordHash = Convert.ToBase64String(passwordHashAndSalt[0]);
                newUser.Salt = Convert.ToBase64String(passwordHashAndSalt[1]);

                this.Context.Users.Add(newUser);
                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            // After user has been created login the user (return token)
            LoginUserBindingModel loginBm = new LoginUserBindingModel()
            {
                Email = bm.Email,
                Password = bm.Password
            };

            var tokenBearer = LoginUser(loginBm);

            return tokenBearer;
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

        public AccountCredentialsViewModel LoginUser(LoginUserBindingModel bm)
        {
            var user = new User();
            string tokenBearer = string.Empty;
            Guid userId = Guid.Empty;
            string name = string.Empty;
            string email = string.Empty;

            try
            {
                user = this.Context
                    .Users
                    .Where(u => u.Email == bm.Email)
                    .First();
            }
            catch
            {
                throw new Exception("User not found!");
            }
            
            // taking the user data to send it to the client
            userId = user.Id; 
            name = user.Name;
            email = user.Email;

            var passwordHash = GenerateHashOfPassword(bm.Password, user.Salt);

            if (user.PasswordHash == passwordHash)
            {
                tokenBearer = GenerateToken();
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
                return null;
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

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString() + TokenGenerator.Generate(30);
        }

        public UserCredentials CheckIfTokenIsValidAndReturnUserCredentials(Token bm)
        {
            var token = this.Context
                .Tokens
                .AsNoTracking()
                .Where(t => t.Value == bm.Value)
                .Select(t => t.Value)
                .FirstOrDefault();

            var userId = this.Context
                .Users
                .AsNoTracking()
                .Where(u => u.Tokens.All(t => t.Value == token))
                .Select(u => u.Id)
                .FirstOrDefault();

            if (token != null && userId != null)
            {
                return new UserCredentials() { UserId = userId, Token = token };
            }

            return null;
        }
    }
}
