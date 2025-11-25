namespace TDD_Bank
{
    internal class User
    {
        
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
       
    }
}
