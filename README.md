# ContactList
Unutar ovog repozitorija razvijena je aplikacija koja omogućuje pregled kontakata korisnika. Razvijena je prema zadatku koji je definiran unutar dokumenta *Zadatak-za-programere.docx*. 

Aplikacija je razvijena unutar okvira ASP.NET Core. Za bazu je korištena lokalna baza MSSQL. Za izradu korisničkog sučelja korišten je Bootstrap, jQuery, JavaScript. Za rad s podacima korištena je Microsoft.EntityFrameworkCore tehnologija.

Osim opisanog u dokumentu *Zadatak-za-programere.docx*, pomoću ASP.NET Core Identity razvijena je funkcionalnost prijave u aplikaciju. Razvijena je i funkcionalnost brisanja te ažuriranja kontakta.

Za pokretanje aplikacije potrebno je kreirati bazu prema kreiranim migracijama (izvođenjem naredbe Update-Database unutar PackageManagerConsole). 

Nekoliko *Usera* kreirano je pomoću *Seed* funkcije. Podaci od *Usera* koji su potrebni za prijavu u aplikaciju jesu:
1.	Email: user1@user.user
  Pass: User123!
2.	Email: user2@user.user
  Pass: User123!
