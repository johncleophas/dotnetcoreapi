using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public class UserService : IUserService
    {

        private DataContext context;

        public UserService(AppSettings appSettings)
        {
            this.context = new DataContext(appSettings);
        }

        public ServiceResult<User> Authenticate(string userName, string password)
        {

            var result = new ServiceResult<User>
            {
                Result = null,
                Status = System.Net.HttpStatusCode.InternalServerError
            };

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return result;
            }

            var user = context.Users.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return result;
            }

            if (!VerifyPasswordHash(password, user.Hash, user.Salt))
            {
                result.Message = "Invalid credentials!";

                return result;
            }

            return new ServiceResult<User>
            {
                Result = user,
                Status = System.Net.HttpStatusCode.OK
            };

        }

        public ServiceResult<User> Create(User user, string password)
        {
            var result = new ServiceResult<User>
            {
                Result = null,
                Status = System.Net.HttpStatusCode.InternalServerError
            };

            if (string.IsNullOrWhiteSpace(password))
            {
                result.Message = "Password is required";
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }


            if (this.context.Users.Any(x => x.UserName == user.UserName))
            {
                result.Message = $"Username {user.UserName} is already taken";
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                return result;

            }

            var validationResult = ValidateUser(user);

            if (!string.IsNullOrEmpty(validationResult))
            {
                result.Message = validationResult;
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                return result;
            };

            var hashSalt = GenerateHash(password);

            user.Hash = hashSalt.Hash;
            user.Salt = hashSalt.Salt;

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return new ServiceResult<User>
            {
                Result = user,
                Status = System.Net.HttpStatusCode.OK
            };
        }

        public ServiceResult<User> GetUserByUserName(string userName)
        {
            var user = this.context.Users
                .Where(e => e.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                return new ServiceResult<User>
                {
                    Message = "User not found",
                    Status = System.Net.HttpStatusCode.NotFound
                };
            }

            return new ServiceResult<User>
            {
                Result = user,
                Status = System.Net.HttpStatusCode.OK
            };
        }

        public ServiceResult<bool> Delete(int id)
        {
            var user = this.context.Users.Find(id);
            if (user != null)
            {
                this.context.Users.Remove(user);
                this.context.SaveChanges();

                return new ServiceResult<bool>()
                {
                    Result = true,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
            else
            {
                return new ServiceResult<bool>()
                {
                    Message = "User not found",
                    Result = false,
                    Status = System.Net.HttpStatusCode.NotFound
                };
            }
        }

        private static string ValidateUser(User user)
        {

            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(user.EmailAddress))
            {
                sb.Append($"Email Address {user.EmailAddress} is required");
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                sb.Append($"First Name {user.FirstName} is required");
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                sb.Append($"Last Name {user.LastName} is required");
            }

            if (sb.ToString().Length > 0)
            {
                return sb.ToString();
            }

            return string.Empty;
        }

        private static HashSalt GenerateHash(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return new HashSalt()
                {
                    Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    Salt = hmac.Key
                };
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
