# Asset Amy
Asset Amy, ein Projekt von Robert Ackermann. Made with ♥ and ☕ in Erfurt.

## Allgemein
Hier soll eine Web-Applikation namens "Asset Amy" entstehen. Anders als die ürsprüngliche ["Asset Amy"-Software](https://github.com/batzlov/asset-amy), wird diese aber nicht mit NestJS und React als Technologie-Stack sondern ausschließlich mit .NET 6.0 implementiert. 

Die Anwendung soll verschiedene Funktionen zur Verfügung stellen, darunter das Tracken von regelmäßigen Einnahmen. 
Nutzerinnen und Nutzer können beispielsweise ihr monatliches Gehalt oder regelmäßige Zahlungen von Investitionen eintragen und verwalten.

Des Weiteren soll es möglich sein, regelmäßige Ausgaben zu tracken, indem Nutzerinnen und Nutzer beispielsweise Mietzahlungen, Versicherungsprämien oder monatliche Abonnements eintragen können.

Ein weiteres Feature der Anwendung soll das Tracken des persönlichen Netto-Vermögens sein. Dabei können Nutzerinnen und Nutzer ihr Vermögen in verschiedene Kategorien wie Aktien, 
Immobilien oder Anleihen unterteilen und so ihre Asset Allocation darstellen.

Insgesamt soll "Asset Amy" verschiedene Funktionen zur Verwaltung von Einnahmen, Ausgaben und Vermögen bieten. 

### Funktionallitäten

1. Hinzufügen, bearbeiten und löschen von Einnahmen
2. Übersicht über Einnahmen mit Hilfe verschiedener Statistiken
3. Hinzufügen, bearbeiten und löschen von Ausgaben
4. Übersicht über Ausgaben mit Hilfe verschiedener Statistiken
5. Prognose für Vermögenswachstum über verschiedene Zeiträume
6. Hinzufügen, bearbeiten und löschen von Assets
7. Übersicht über Assets mit Hilfe verschiedener Statistiken
8. (eventuell) Import der Daten aus anderen Systemen ermöglichen, interessant wäre hier bspw. der Import von Wertpapierkäufen aus einem Depot
9. Export der Daten für verschiedene Systeme ermöglichen, so z.B. Excel

### Projektstruktur
```
├── ...
├── Models
│── Views
└── Controllers
```

### ERD-Modell
![ERD-Modell](./docs/database/db-erd.png)