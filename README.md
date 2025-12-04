<h1 align="center"><u>TDD Bank</u></h1>

1. **Start**: När programmet startas laddas testdatan (kunder/konton)
2. **Välkomstmeddelande**: Användaren får en ASCII art Logo och sedan en meny för att logga in eller avsluta programmet
3. **Inloggning**
   - Användaren anger Användarnamn och lösenord
   - Systemet kollar om användaren finns och om lösenordet passar
   - Om användaren skriver fel lösenord 3 gånger så blir dom utlåsta och måste fråga en Admin om hjälp
4. **Inloggad**
   - När Kunden loggat in, hamnar de i en loop där de kan se sina konton, sätta in/ta ut pengar och göra överföringar
   - Om en admin loggar in hamnar de i en alternativ meny för enbart Admins, där kan kunder bli skapade och valutas värde bli uppdaterat
5. **Utloggning**: När Användaren väljer loggout så kommer deras user bli satt till null och dem kommer tas till Inloggning

<h2 align="center"><u>Objekt och Klasser</u></h2>

1. **Program.cs**: Skapar Bank-objektet för att köra programmet
2. **Bank.cs**: Här körs själva programmet och är hjärnan bakom allt
3. **UI.cs**: Denna klassen är bara för menyer/det som syns på skärmen

<h3 align="center"><u>Data</u></h3>

- **Data.cs**: Detta är en statisk klass som innehåller Listor på användare, växelkurs och transaktionshistorik

<h4 align="center"><u>Arv</u></h4>

- **User**: Grundmallen för att skapa en användare
- **Client**: En vanlig bankkund. Ärver från user så att användare får ett bankkonto
- **Admin**: Ärver från user men har tillgång till administrativa verktyg istället för bankkonton.
