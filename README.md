

## KORISNICKI RACUNI IMAJU ISTI PASSWORD KAO I IME 

- korisnik@gmail.com		korisnik	#normalni user
- matan1@gmail.com		matan1		#profesor		
- matan2@gmail.com		matan2		#profesor
- tinf@gmail.com		tinf		#profesor
- legendarni@gmail.com		legendarni	#profesor
- brucos@gmail.com		brucos		#normalni user
- imposter@gmail.com		imposter	#profesor

- Predlazem koristenje korisnika i matan1 jer se sve funkcionalnosti mogu vidjeti vec na njima

# Što sam dodao 
- Ispunio sam sve zahtjeve navedene na kraju readme, i dodao mnogo novih funkcionalnosti

## PROFILE PAGE
- Ovisno tko je ulogiran (student/profesor) vide svoje instrukcije, student nema "nadolazece instrukcije". 
- Samo korisnik može promijeniti / ponovno poslati instrukcije.
- Prikaz svih podataka o instrukcijama u karticama, ovisno o statusu instrukcija može se promijeniti datum ili ponovno dogoviriti
- Limit broja aktivnih instrukcija od 3

## POSTAVKE
- Dodao settings page na kojem je također moguće vidjeti promjenu podataka prije klika na gumb "Save".
- Nakon klika na gumb "Save", korisnik dobije informaciju je li se uspješno sve spremilo.
- Dodan limit da može biti maksimalno 3 aktivna polja za instrukcije, s provjerom duplikata na frontu i na backendu.

## REGISTER
- Dodane provjere postoji li već korisnik s istim emailom.
- Dodan limit da može biti maksimalno 3 aktivna polja za predmete instrukcija, s provjerom duplikata na frontu i na backendu.

## PROFESSOR COMPONENT I DATUM COMPONENT
- Maknuo user-icon ispred datuma jer je redundantan.
- Maknuo gumb za dogovor termina ako je trenutni korisnik profesor.
- Na promjenu datuma instrukcija osvježe se informacije s najnovijima.
- Napravio da gumb "Odustani" ne šalje podatke za promjenu datuma (bug originalno?).
- Ako je ulogirani korisnik student, onda vidi gumb za rezervaciju, ako je profesor onda ne.

## HOME PAGE
- Dodao da se na home page vidi jesu li najpopularniji profesori već rezervirani za korisnika.
- Svi sada vide broj instrukcija za profesore na glavnoj stranici.
- Samo admini mogu vidjeti "novi predmet" gumb

## PREDMET
- Na svakom predmetu sada se može vidjeti je li korisnik imao instrukcije ili ne za profesora
  (npr korisnik@gmail.com pass: korisnik se može vidjeti na /subject/matan2), ako nije, može se prijaviti.

## DODAVANJE PREDMETA
- Sada se dolje vidi status update-a kao i kod promjene postavki korisnika.

## OSTALO
- Broj instrukcija se računa brojanjem dosad odradjenih instrukcija za svakog profesora.
- Ažurira se čim se doda nova instrukcija globalno.




# dotInstrukcije

figma: https://www.figma.com/file/H7MiCGddxAWLAusmMXpeDq/dotInstrukcije?type=design&mode=design&t=yN0IyoeuWHCsdUJk-0
kontakt: dotget@eestec.hr ili pavleergovic@gmail.com

Dobrodošli na programski zadatak za radionicu .GET

Prije svega, forkajte ovaj repo te nadodajte svoje ime i prezime u nazivu projekta.
Vrlo je moguće da ćemo još raditi na ovome, tako da molim da pratite WhatsUp grupu gdje ćemo Vas obavijestiti da syncate repo.

# Instalacija

za ovaj projekt potreban vam je VSC i nmp.
Napravite 
```
git clone
```
svog forkanog repoa te napravite 
```
cd ime-projekta
npm install
npm run dev
```
Preimenujte .env.example u .env te dodajte svoj link na Backend.

Vaš kod je spreman!

# Upute

Motivacija iza projekta je da naučite backend u .NET-u i spajanje frontenda na backend aplikacije.
Frontend je spreman te on poziva rute, Vaš posao je da dovršite te rute kako bi front proradio.
U prvoj fazi projekta, ne bi trebali ništa pisati na frontu jer je sve spremno.

Aplikacija je apk za instrukcije koja spaja studente i profesore te im nudi opciju dogovaranja termina instrukcija.

# Entiteti

Lista entiteta je naš dobronamjerni savjet kako oblikovati bazu. Slobodno sami možete mijenjati stvari. Molimo da pritom pripazite na imenovanje varijabla jer se neke varijable koriste i na frontendu.

## Student
  - id: id
  - email: String
  - name: String
  - surname: String
  - password: String 
  - profilePictureUrl: String

## Professor
  - email: String
  - name: String
  - surname: String
  - password: String 
  - profilePictureUrl: String
  - instructionsCount: Number // broj održanih instrukcija
  - subjects: String[] // lista urlova/id-eva predmeta na kojima profesor predaje

## Subject
  - id: id
  - title: String
  - url: String
  - description: String

## Instructions Date
  -studentId: id
  -professorId: id
  -dateTime: DateTime
  -status: String //status može biti poslan zahtjev, nadolazeća instrukcija ili prošla instukcija

# API Documentation

Lista apija je naš dobronamjerni savjet kako oblikovati APi strukturu. Slobodno sami možete mijenjati stvari. Molimo da pritom pripazite na imenovanje ruta jer se neke varijable koriste i na frontendu.

## Student Routes

### Register a Student

- **URL**: /register/student
- **Method**: POST
- **Description**: Registers a new student by providing their details.
- **Body**:
  - **email**: example@example.com
  - **name**: First
  - **surname**: Last
  - **password**: yourpassword
  - **profilePictureUrl**: https://example.com/profile.jpg
- **Returns**:
  - **success**: true/false
  - **message**: string

### Login a Student

- **URL**: /login/student
- **Method**: POST
- **Description**: Authenticates a student using their email and password.
- **Body**:
  - **email**: example@example.com
  - **password**: yourpassword
- **Returns**:
  - **success**: true/false
  - **student**: student object (if success is true)
  - **token**: JWT token (if success is true)
  - **message**: string

### Get a Specific Student by Email

- **URL**: /student/:email
- **Method**: GET
- **Description**: Retrieves a specific student by their email. Requires JWT authentication.
- **Authentication**: Bearer Token (JWT)
- **Returns**:
  - **success**: true/false
  - **student**: student object (if success is true)
  - **message**: string

### Get All Students

- **URL**: /students
- **Method**: GET
- **Description**: Retrieves a list of all students. Requires JWT authentication.
- **Authentication**: Bearer Token (JWT)
- **Returns**:
  - **success**: true/false
  - **students**: array of student objects (if success is true)
  - **message**: string

## Professor Routes

### Register a Professor

- **URL**: /register/professor
- **Method**: POST
- **Description**: Registers a new professor by providing their details.
- **Body**:
  - **email**: example@example.com
  - **name**: First
  - **surname**: Last
  - **password**: yourpassword
  - **profilePictureUrl**: https://example.com/profile.jpg
  - **subjects**: [Math, Science]
- **Returns**:
  - **success**: true/false
  - **message**: string

### Login a Professor

- **URL**: /login/professor
- **Method**: POST
- **Description**: Authenticates a professor using their email and password.
- **Body**:
  - **email**: example@example.com
  - **password**: yourpassword
- **Returns**:
  - **success**: true/false
  - **professor**: professor object (if success is true)
  - **token**: JWT token (if success is true)
  - **message**: string

### Get a Specific Professor by Email

- **URL**: /professor/:email
- **Method**: GET
- **Description**: Retrieves a specific professor by their email. Requires JWT authentication.
- **Authentication**: Bearer Token (JWT)
- **Returns**:
  - **success**: true/false
  - **professor**: professor object (if success is true)

### Get All Professors

- **URL**: /professors
- **Method**: GET
- **Description**: Retrieves a list of all professors. Requires JWT authentication.
- **Authentication**: Bearer Token (JWT)
- **Returns**:
  - **success**: true/false
  - **students**: array of professor objects (if success is true)

## Subject Routes (Requires JWT authentication)

### Create a Subject

- **URL**: /subject
- **Method**: POST
- **Description**: Creates a new subject with a unique title.
- **Authentication**: Bearer Token (JWT)
- **Body**:
  - **title**: Unique title for the subject
  - **url**: Unique URL
  - **description**: Description of the subject
- **Returns**:
  - **success**: true/false
  - **message**: string

### Get a Subject by URL

- **URL**: /subject/:url
- **Method**: GET
- **Description**: Retrieves a specific subject by its title.
- **Authentication**: Bearer Token (JWT)
- **Returns**:
  - **success**: true/false
  - **subject**: subject object (if success is true)
  - **professors**: list of associated professors (if success is true)
  - **message**: string

### Get All Subjects

- **URL**: /subjects
- **Method**: GET
- **Description**: Retrieves a list of all subjects.
- **Returns**:
  - **success**: true/false
  - **subjects**: array of subject objects (if success is true)

### Schedule Instruction Session

- **URL**: /instructions
- **Method**: POST
- **Authentication**: Bearer Token (JWT)
- **Request Body**:
  - **date** [required]: The date selected for the instruction session.
  - **professorId** [required]: The unique identifier of the professor with whom the session is to be scheduled.
- **Returns**:
  - **success**: true/false
  - **message**: string

# Restrikcije i napomene
  - rute koje vraćaju student objekt također vraćaju objekte sentInstructionRequests, upcomingInstructions, pastInstructions, koji sadrže Jsona profesor objekata koji se uklapaju u svaku varijablu (pogledati ProfilePage.jsx komponentu)
  - pri registraciji morate provjeravati postoje li ti emailovi u bazi
  - pri kreiranju predmeta morate provjeriti postoji li taj url u bazi
  - profesor može biti instruktor na maksimalno 3 predmeta
  - student istovremeno može poslati maksimalno 3 zahtjeva za instrukcije
  - front se brine za to hoće li informacije biti pokazane za studenta ili profesora, jedino profesori mogu kreirati nove predmete
  - morate ostvariti JWT autorizaciju

Sretno! :)


