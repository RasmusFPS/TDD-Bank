<h1 align="center"><u>TDD Bank</u></h1>

1. **Start**: När programmet startas laddas testdatan (kunder/konton)
2. **Välkomstmeddelande**: Användaren får en ASCII art Logo och sedan en meny för att logga in eller avsluta programmet
3. **Inloggning**
   - Användaren anger Användarnamn och lösenord
   - Systemet kollar om användaren finns och om lösenordet passar
   - Om användaren skriver fel lösenord 3 gånger så blir dom utlåsta och måste fråga en Admin om hjälp
4. **Inloggad**
   - När Kunden loggat in, hamnar de i en loop där de kan se sina konton, sätta in/ta ut pengar, göra överföringar, skapa nytt Bankkonto/Sparkonto och ta lån
   - Om en admin loggar in hamnar de i en alternativ meny för enbart Admins, där man kan låsa upp kunder, skapad kunder till banken och uppdaterat värdet av bankens valutor
5. **Utloggning**: När Användaren väljer loggout så kommer deras user bli satt till null och dem kommer tas till Inloggning

<h2 align="center"><u>Objekt och Klasser</u></h2>

1. **Program.cs**: Skapar Bank-objektet för att köra programmet
2. **Bank.cs**: Här körs själva programmet och är hjärnan bakom allt
3. **UI.cs**: Denna klassen är bara för menyer/det som syns på skärmen
4. **BankTransfer.cs**: En hjälpklass som sköter logiken för att flytta pengar. Den hanterar valutaväxling, input-validering och lägger överföringar i kö.
5. **TransferLog.cs**: Ett objekt som fungerar som kvitto. Sparar information om genomförda transaktioner (avsändare, mottagare, belopp, tid).
6. **PendingTransfer.cs**: Ett objekt som håller information om en överföring som ligger i kö (väntar på 15-minuters fördröjningen). Innehåller mottagarkonto och belopp.

<h2 align="center"><u>Data</u></h2>

- **Data.cs**: Detta är en statisk klass som innehåller Listor på användare, växelkurs och transaktionshistorik

<h2 align="center"><u>Arv</u></h2>

- **User**: Grundmallen för att skapa en användare
- **Client**: En vanlig bankkund. Ärver från user så att användare får ett bankkonto
- **Admin**: Ärver från user men har tillgång till administrativa verktyg istället för bankkonton.

<h2 align="center"><u>BankKonto</u></h2>

- **Account**: Ett vanligt bankkonto. Håller koll på saldo, valuta kontonummer
- **SavingAccount**: Sparkonto med en 2% ränta
