namespace TDD_Bank
{
    internal class User
    {
        public static int attempts = 0;
        
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
        }
        //changed to return to User insted of Bool we need to know what user is logged in
        internal static User SignIn()
        {
            while (attempts < 3)
            {
                // 1. It calls the worker to get the data.
                var Credentials = UI.SignInInput();

                string username = Credentials.Item1;
                string userPassword = Credentials.Item2;

                // 2. It does the logic.
                foreach (User user in Data.UserCollection)
                {
                    if (user.Username == username && user.Password == userPassword)
                    {
                        Console.WriteLine($"Welcome {user.Username}");
                        // Here you would call the correct menu
                        return user;
                    }
                }

                // 3. It handles failure.
                attempts++;
                Console.WriteLine("Wrong user-ID or password.");
            }

            if (attempts == 3)
            {
                Console.WriteLine("Too many attempts.");
            }
            return null;
        }
    }
}
