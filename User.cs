namespace TDD_Bank
{
    internal class User
    {
        public static int attempts = 0;
        public static List<User> UserCollection = new List<User>()
        {
            new User( "Admin-Johan", "1234", true),
            new User( "Carl", "Hawaa", false)

        };
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
        }



        internal static bool SignIn()
        {
            while (attempts < 3)
            {
                // 1. It calls the worker to get the data.
                var Credentials = UI.SignInInput();

                string username = Credentials.Item1;
                string userPassword = Credentials.Item2;

                // 2. It does the logic.
                foreach (User user in UserCollection)
                {
                    if (user.Username == username && user.Password == userPassword)
                    {
                        Console.WriteLine($"Welcome {user.Username}");
                        // Here you would call the correct menu
                        return true;
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
            return false;
        }
    }
}
