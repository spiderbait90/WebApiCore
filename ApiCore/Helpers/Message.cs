using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Helpers
{
    public class Message
    {
        public const string LoggedInUserNotFound = "Cant find current logged in user!";

        public const string UsernamePasswordIncorrect = "Username or password is incorrect";

        public const string OrderUserNotFound = "User to add the order is not found!";

        public const string ProductsNotFound = "All or some products were not found!";

        public const string PasswordRequired = "Password is required!";

        public const string UsernameTaken = "Username {0} is already taken";

        public const string PassHashLength  = "Invalid length of password hash (64 bytes expected).";

        public const string PassSaltLength  = "Invalid length of password salt (128 bytes expected).";

        public const string NoProductsIds = "You must enter products Id !";
    }
}
