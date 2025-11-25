namespace TDD_Bank
{
    internal class TDDBank
    {
        private static int attempts = 0;

        internal static User SignIn()
        {
            bool signedIn = false;
            while (signedIn == false)
            {
                while (attempts < 3)
                {

                    // 1. It calls the worker to get the data.
                    var Credentials = UI.SignInInput();

                    string username = Credentials.Item1.ToLower();
                    string userPassword = Credentials.Item2;

                    // 2. It does the logic.
                    foreach (User user in Data.UserCollection)
                    {
                        if (user.Username.ToLower() == username && user.Password == userPassword)
                        {
                            Console.WriteLine($"Welcome {user.Username}");
                            // Here you would call the correct menu
                            signedIn = true;
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
                    return null;
                }
            }
            return null;
        }
    }
}
