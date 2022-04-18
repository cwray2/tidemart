using api.Interfaces;
using api.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using api.Database;

namespace api.Database
{
    public class AuthDatahandler : IAuthDataHandler
    {
        const double TOKEN_EXPIRATION_TIME = 1;
        IUserDataHandler dataHandler = new UserDataHandler();
        static List<AuthToken> allSessionTokens = new List<AuthToken>();
        public bool IsTokenValid(AuthToken authToken)
        {
            foreach (AuthToken storedToken in allSessionTokens)
            {
                if(authToken.Token == storedToken.Token) return true;
            }
            return false;
        }

        public Login LoginUser(User thisUser)
        {
            if (thisUser.Username == null || thisUser.Password == null)
            {
                return new Login()
                {
                    Response = 400,
                    Message = "This is a mesage"
                };
            }

            User foundUser;
            try
            {
                foundUser = dataHandler.SelectByUsername(thisUser.Username)[0];
            }
            catch (Exception e)
            {
                return new Login()
                {
                    Response = 400,
                    Message = "There exists no user with username of " + thisUser.Username + "\nSystem Message: " + e.Message
                };
            }

            //does password match?
            string hashedPasswordFromUserInput = HashPassword(thisUser.Password);
            if (hashedPasswordFromUserInput == foundUser.Password)
            {
                //Create session token and add to list
                Guid thisGuid = Guid.NewGuid();
                allSessionTokens.Add(new AuthToken(thisGuid));

                //send a Login Model back to signify a valid login
                return new Login()
                {
                    Response = 200,
                    Message = "OK",
                    Username = thisUser.Username,
                    AuthToken = new AuthToken(thisGuid)
                };
            }
            else
            {
                return new Login()
                {
                    Response = 400,
                    Message = "not a correct password"
                };
            }
        }

        public Login LogoutUser(AuthToken authToken)
        {
            int index = -1;
            for (int i = 0; i < allSessionTokens.Count; i++)
            {
                if (authToken.Token == allSessionTokens[i].Token)
                {
                    index = i;
                }
            }

            try
            {
                allSessionTokens.RemoveAt(index);
                return new Login()
                {
                    Response = 200,
                    Message = "Logout Successful"
                };
            }
            catch (System.Exception)
            {
                return new Login()
                {
                    Response = 400,
                    Message = "Logout Unsuccessful"
                };
            }
        }

        public Register RegisterUser(User newUser)
        {
            try
            {
                List<User> userList = dataHandler.SelectByUsername(newUser.Username);

                if (userList.Count > 0)
                {
                    return new Register()
                    {
                        Response = 400,
                        Message = "A User with the username, email, or SSN already exists"
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("User does not exist");
                 // TODO
            }
            //second: Hash password and replace with unhashed password
            string hashedPassword = HashPassword(newUser.Password);
            newUser.Password = hashedPassword;

            try
            {
                dataHandler.Insert(newUser);
            }
            catch (Exception e)
            {
                return new Register()
                    {
                        Response = 400,
                        Message = e.Message
                    };
            }
            //fifth
            return new Register()
            {
                Response = 200,
                Message = $"User {newUser.Username} created!",
                Username = newUser.Username
            };
        }

        public void RemoveExpiredTokens()
        {
            DateTime currentDate = DateTime.Now;

            foreach (AuthToken authToken in allSessionTokens.ToList())
            {
                TimeSpan ts = DateTime.Now - authToken.TimeStamp;
                if (ts.TotalHours > TOKEN_EXPIRATION_TIME)
                {
                    allSessionTokens.Remove(authToken);
                }
            }
        }

        private string HashPassword(string notHashedPassword)
        {
            //unique salt number
            byte[] salt = new byte[4499];
            //derive a 256-bit subkey (use H)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: notHashedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
            return hashed;
        }
    }
}