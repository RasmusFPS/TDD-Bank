namespace TDD_Bank
{
    internal class User
    {
        public int attempts = 0;
        public List<User> UserCollection = new List<User>()
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

        internal bool SignIn()
        {
            //Få in signininput
            while (attempts < 3)
            {
                UI.SignInInput();
                bool signedIn = false;
                foreach (User user in UserCollection)
                {
                    if (user.Username == username && user.Password == userPassword)
                    {
                        Console.WriteLine($"Welcome {user.Username}");
                        return true;
                    }
                }
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
