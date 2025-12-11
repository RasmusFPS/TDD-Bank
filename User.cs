namespace TDD_Bank
{
    internal class User
    {
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal bool IsAdmin { get; set; }
        internal int Tries { get; set; }

        internal User(string username, string password, bool isAdmin, int tries)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            Tries = tries;
        }
        //changed to return to User insted of Bool we need to know what user is logged in
    }
}
