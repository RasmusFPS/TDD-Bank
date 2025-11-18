namespace TDD_Bank
{
    internal class Login
    {
        public int attempts = 0;
        public List<Users> UserCollection = new List<Users>()
        {
            new Users(1, "Admin-Johan", "1234", true),
            new Users(2, "Carl", "Hawaa", false)

        };




        internal void AddUser()
        {
            //-------------------------
            //Blueprint Usercreation
            //-------------------------

            //string name = Console.ReadLine(); 
            //string password = Console.ReadLine();
            //if (int.TryParse(Console.ReadLine(), out int id)) ;
            //new Users(id, name, password, isAdmin)



            foreach (var i in UserCollection)
            {
                Console.WriteLine(i);
            }
            //UserCollection.Add(id, Name, password);
        }

        internal bool SignIn()
        {
            
            while (attempts < 3)
            {
                Console.Write("User-ID:");
                if (int.TryParse(Console.ReadLine(), out int userID)) ;
                Console.Write("Password:");
                string userPassword = Console.ReadLine();
                bool signedIn = false;
                foreach (Users user in UserCollection)
                {
                    if (user.Id == userID && user.Password == userPassword)
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
