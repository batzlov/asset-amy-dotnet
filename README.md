# Asset Amy

Asset Amy, ein Projekt von Robert Ackermann. Made with ♥ and ☕ in Erfurt.

## Allgemein

Hier soll eine Web-Applikation namens "Asset Amy" entstehen. Anders als die ürsprüngliche ["Asset Amy"-Software](https://github.com/batzlov/asset-amy), wird diese aber nicht mit NestJS und React als Technologie-Stack sondern ausschließlich mit .NET 7.0 implementiert.

Die Anwendung soll verschiedene Funktionen zur Verfügung stellen, darunter das Tracken von regelmäßigen Einnahmen.
Nutzerinnen und Nutzer können beispielsweise ihr monatliches Gehalt oder regelmäßige Zahlungen von Investitionen eintragen und verwalten.

Des Weiteren soll es möglich sein, regelmäßige Ausgaben zu tracken, indem Nutzerinnen und Nutzer beispielsweise Mietzahlungen, Versicherungsprämien oder monatliche Abonnements eintragen können.

Ein weiteres Feature der Anwendung soll das Tracken des persönlichen Netto-Vermögens sein. Dabei können Nutzerinnen und Nutzer ihr Vermögen in verschiedene Kategorien wie Aktien,
Immobilien oder Anleihen unterteilen und so ihre Asset Allocation darstellen.

Insgesamt soll "Asset Amy" verschiedene Funktionen zur Verwaltung von Einnahmen, Ausgaben und Vermögen bieten.

## Installation und Ausführung

Folgenden Befehl ausführen um die Konfigurationsdatei zu erstellen:

```bash
    cp appsettings.example.json appsettings.json
```

Um das Projekt erfolgreich zu starten muss zuerst eine mysql-Datenbank angelegt werden.
Die Daten für den Zugang zur Datenbank müssen entsprechend in der `appsettings.json` eingetragen werden.

Um die Datenbank zu initialisieren und Testdaten zu importieren muss folgender Befehl ausgeführt werden auf Windows:

```bash
    dotnet-ef database update
```

bzw. auf Mac/Linux:

```bash
    dotnet ef database update
```

Anschließend kann das Projekt mit folgendem Befehl gestartet werden:

```bash
    dotnet watch
```

## Test-Account

Zum Testen der Anwendung kann folgender Account verwendet werden:

```
    E-Mail: robert.ackermann@fh-erfurt.de
    Passwort: password
```

### Anmerkungen

Nach der erfolgreichen Registrierung müssen Nutzer:innen zuerst ihre E-Mail Adresse verifizieren, bevor sie sich anmelden können.

### Anmerkungen E-Mail Versand

Soll der E-Mail Versand aktiviert werden, muss in der `appsettings.json` ein passender Sendgrid-Api-Key eingetragen werden.

## Screenshots der Anwendung

![Landing Page](./docs/screenshots/landing-page.png)
![Screenshot 1](./docs/screenshots/aa-screenshot-1.png)
![Screenshot 2](./docs/screenshots/aa-screenshot-2.png)
![Screenshot 3](./docs/screenshots/aa-screenshot-3.png)
![Screenshot 4](./docs/screenshots/aa-screenshot-4.png)
![Screenshot 5](./docs/screenshots/aa-screenshot-5.png)

### Funktionallitäten (ursprünglich)

1. Hinzufügen, bearbeiten und löschen von Einnahmen
2. Übersicht über Einnahmen mit Hilfe verschiedener Statistiken
3. Hinzufügen, bearbeiten und löschen von Ausgaben
4. Übersicht über Ausgaben mit Hilfe verschiedener Statistiken
5. Prognose für Vermögenswachstum über verschiedene Zeiträume
6. Hinzufügen, bearbeiten und löschen von Assets
7. Übersicht über Assets mit Hilfe verschiedener Statistiken
8. (eventuell) Import der Daten aus anderen Systemen ermöglichen, interessant wäre hier bspw. der Import von Wertpapierkäufen aus einem Depot
9. Export der Daten für verschiedene Systeme ermöglichen, so z.B. Excel

### Funktionallitäten (tatsächlich)

1. Hinzufügen, bearbeiten und löschen von Einnahmen
2. Übersicht über Einnahmen mit Hilfe verschiedener Statistiken
3. Hinzufügen, bearbeiten und löschen von Ausgaben
4. Übersicht über Ausgaben mit Hilfe verschiedener Statistiken
5. Hinzufügen, bearbeiten und löschen von Assets
6. Übersicht über Assets mit Hilfe verschiedener Statistiken
7. Export der Daten für verschiedene Systeme ermöglichen, mittels z.B. Excel

### Projektstruktur

```
├── bin
├── Controllers
│   ├── ApiAssetController.cs
│   ├── ApiAuthController.cs
│   ├── ApiExpenseController.cs
│   ├── ApiRevenueController.cs
│   ├── AuthController.cs
│   ├── DashboardController.cs
│   └── HomeController.cs
├── DbContext
│   └── AssetAmyContext.cs
├── Managers
│   ├── AssetManager.cs
│   ├── ExpenseManager.cs
│   ├── RevenueManager.cs
│   └── UserManager.cs
├── Models
│   ├── Asset.cs
│   ├── ErrorViewModel.cs
│   ├── Expense.cs
│   ├── Revenue.cs
│   └── User.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── README.md
├── Views
│   ├── Auth
│   │   ├── PasswordForgotten.cshtml
│   │   ├── PasswordReset.cshtml
│   │   ├── SignIn.cshtml
│   │   ├── SignUp.cshtml
│   │   └── VerifyEmail.cshtml
│   ├── Dashboard
│   │   ├── AssetAllocation.cshtml
│   │   ├── Expenses.cshtml
│   │   ├── Index.cshtml
│   │   └── Revenues.cshtml
│   ├── Home
│   │   ├── Error404.cshtml
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Shared
│   │   ├── Error.cshtml
│   │   ├── Icons
│   │   │   ├── BankNotes.cshtml
│   │   │   ├── ChartPie.cshtml
│   │   │   ├── CreditCard.cshtml
│   │   │   └── Home.cshtml
│   │   └── Layouts
│   │       ├── _AuthLayout.cshtml
│   │       ├── _DashboardLayout.cshtml
│   │       ├── _ErrorLayout.cshtml
│   │       ├── _LandingLayout.cshtml
│   │       └── _Layout.cshtml
│   └── _ViewImports.cshtml
├── appsettings.Development.json
├── appsettings.json
├── asset-amy.csproj
├── asset-amy.sln
├── bin
├── docs
│   └── database
│       └── db-erd.png
├── global.json
├── obj
├── prisma
│   ├── node_modules
│   ├── package-lock.json
│   ├── package.json
│   └── prisma
│       ├── migrations
│       ├── schema.prisma
│       └── seed.ts
└── wwwroot
    ├── assets
    │   └── ...
    ├── css
    │   └── app.css
    ├── js
    │   ├── app.js
    │   ├── auth
    │   │   ├── password-forgotten.js
    │   │   ├── password-reset.js
    │   │   ├── sign-in.js
    │   │   └── sign-up.js
    │   ├── dashboard
    │   │   ├── Index.js
    │   │   ├── assets.js
    │   │   ├── expenses.js
    │   │   └── revenues.js
    │   └── shared
    │       ├── constants.js
    │       ├── form.js
    │       ├── modal.js
    │       ├── request.js
    │       ├── schema.js
    │       ├── toast.js
    │       └── utils.js
    ├── lib
    │   ├── chartjs
    │   ├── daisyui
    │   └── tailwind
    └── mails
        ├── reset-password.html
        └── verify-email.html
```

### ERD-Modell

![ERD-Modell](./docs/database/db-erd.png)

### Integration von Prisma in das Projekt

Anlegen des Projekts und installieren von Paketabhängigkeiten. Das bearbeiten der "prisma.schema"-Datei wird hier nicht genauer beschrieben.

```
    mkdir prisma
    cd prisma
    npm init -y
    npm i --save-dev prisma
    npx prisma init
    npx prisma migrate dev --name init
```

Installieren der Paketabhängigkeiten für .net. Als Connector wird MySql genutzt.

```
    cd ..
    dotnet add package Pomelo.EntityFrameworkCore.MySql
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.EntityFrameworkCore.Design
```

Erstellen des DbContext und der Model-Dateien. Parameter müss hier je nach Datenbank-Konfiguration abgeändert werden.

```
    dotnet ef dbcontext scaffold "Server=<host>;Port=<port>;Database=<db-name>;User Id=<db-user>;Password=<db-user-password>"
    Pomelo.EntityFrameworkCore.MySql --context-dir DbContext
    --context AssetAmyContext --output-dir Models
```

```
    dotnet ef dbcontext scaffold "Server=localhost;Port=8889;Database=asset-amy-dotnet;User Id=root;Password=root"
    Pomelo.EntityFrameworkCore.MySql --context-dir DbContext
    --context AssetAmyContext --output-dir Models
```

Erstellen der Models unter Ausschluss der Migration-Tabelle

```
    dotnet ef dbcontext scaffold "Server=localhost;Port=8889;Database=asset-amy-dotnet;User Id=root;Password=root"
    Pomelo.EntityFrameworkCore.MySql --context-dir DbContext --context AssetAmyContext
    --output-dir Models --force --table User --table Asset --table Expense --table Revenue
```

### Validierung von Formularen (Frontend)

Für Validierung von Formularen im Frontend wurde eine einfache, gut nutzbare Bibliothek selbst implementiert. Wie diese Bibliothek funktioniert und genutzt werden kann wird im folgenden grob beschrieben.

```js
class Form {
    init() {
        // initialize all needed functionality
    }

    validate() {
        // validate input and check all rules
    }

    validateAll() {
        // call validate for every single input
    }

    isValid() {
        // returns true when form is valid and false when not
    }

    markAsInvalid(inputNames = []) {
        // mark inputs as invalid
    }

    patchValues(obj) {
        // patch values to form
    }

    reset() {
        // call form reset method
    }

    toObj() {
        // return form values as object
    }
}
```

Nachfolgend wird kurz beschrieben wie die Bibliothek genutzt werden kann. Die Validierungs-Regeln werden über ein Schema definiert. Hier ein Beispiel:

```js
    const schema = {
        inputName: {
            type: "number" | "string" | "boolean",
            rules: [
                "required", "requiredCheckboxTrue", "email",
                "number", "min:2", "max:10", "match:inputName",
                "pattern:regex"
            ],
        },
        ...
    };

    const userSchema = {
        firstName: {
            type: "string",
            rules: ["required", "min:2", "max:10"],
        },
        lastName: {
            type: "string",
            rules: ["required"],
        },
        email: {
            type: "string",
            rules: ["required", "email"],
        },
        password: {
            type: "string",
            rules: ["required"],
        },
        confirmPassword: {
            type: "string",
            rules: ["required", "match:password"],
        },
        privacyPolicyAccepted: {
            type: "boolean",
            rules: ["requiredCheckboxTrue"],
        },
    };
```

Wichtig für die Erstellung des html ist es, das die Input-Elemente einen Namen haben, der dem Namen im Schema entspricht.
So würde das passende html-Gerüst aussehen:

```html
<form>
    <div>
        <label>
            <span> Was ist dein Vorname? </span>
        </label>
        <input type="text" name="firstName" placeholder="Vorname" />
        <label>
            <span> Bitte überprüfe deine Eingabe für dieses Feld </span>
        </label>
    </div>
</form>
```

Und so würde die Nutzung in der Programmierung aussehen:

```js
import { Form } from "./form.js";

const formElm = document.querySelector("form");
const form = new Form(formElm, userSchema);

const submit = document.querySelector("button");
submit.addEventListener("click", (e) => {
    e.preventDefault();
    e.stopPropagation();

    if (form.isValid()) {
        // send ajax request to the backend
    } else {
        // display errors
    }
});
```
