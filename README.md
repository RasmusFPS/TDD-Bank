<h1 align="center"><u>TDD Bank</u></h1>

1. **Start**: When the program starts, test data (clients/accounts/time) is loaded.
2. **Welcome Message**: The user receives an ASCII art Logo and then a menu to log in or exit the program.
3. **Login**
   - The user enters Username and password.
   - The system checks if the user exists and if the password matches.
   - If the user enters the wrong password 3 times, they become locked out and must ask an Admin for help.
4. **Logged In**
   - When the Client logs in, they enter a loop where they can view their accounts, deposit/withdraw money, make transfers, create new Bank Accounts/Savings Accounts, and take loans.
   - If an admin logs in, they enter an alternative menu strictly for Admins, where one can unlock clients, create clients for the bank, and update the value of the bank's currencies.
5. **Logout**: When the User selects logout, their user object is set to null and they are returned to Login.

<h2 align="center"><u>Objects and Classes</u></h2>

1. **Program.cs**: Creates the Bank object to run the program.
2. **Bank.cs**: This is where the program itself runs and is the brain behind everything.
3. **UI.cs**: This class is only for menus/what is visible on the screen.
4. **BankTransfer.cs**: A helper class that handles the logic for moving money. It handles currency exchange, input validation, and queues transfers.
5. **TransferLog.cs**: An object that functions as a receipt. Saves information about completed transactions (sender, receiver, amount, time).
6. **PendingTransfer.cs**: An object that holds information about a transfer currently in the queue (waiting for the 15-minute delay). Contains recipient account and amount.

<h2 align="center"><u>Data</u></h2>

- **Data.cs**: This is a static class containing Lists of users, exchange rates, and transaction history.

<h2 align="center"><u>Inheritance</u></h2>

- **User**: The basic template for creating a user.
- **Client**: A standard bank customer. Inherits from user so that the user gets a bank account.
- **Admin**: Inherits from user but has access to administrative tools instead of bank accounts.

<h2 align="center"><u>BankAccount</u></h2>

- **Account**: A standard bank account. Keeps track of balance, currency, and account number.
- **SavingAccount**: Savings account with 2% interest.
