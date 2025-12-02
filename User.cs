namespace TDD_Bank
{
    internal class User
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int Tries { get; set; }

        public User(string username, string password, bool isAdmin, int tries)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            Tries = tries;
        }
        //changed to return to User insted of Bool we need to know what user is logged in
       
    }
}
