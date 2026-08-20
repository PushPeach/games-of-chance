# Casino Console App (C#)

Eine Konsolenanwendung, die Blackjack und Roulette mit einem gemeinsamen
Guthaben-System kombiniert.

## Features

- Frei wählbares Startguthaben
- **Blackjack**: Hit/Stand, automatischer Dealer (zieht bis 17), Blackjack zahlt 3:2
- **Roulette**: Wetten auf Zahl (35:1), Farbe oder Gerade/Ungerade (je 1:1)
- Validierte Eingaben — ungültige Werte werden abgefangen, kein Absturz
- Spiel endet automatisch, wenn das Guthaben aufgebraucht ist

## Projektstruktur

```
Program.cs          Einstiegspunkt, Hauptmenü-Loop
Player.cs            Guthaben-Verwaltung
InputHelper.cs        Validierte Konsoleneingaben
Games/
  IGame.cs            Gemeinsames Interface für alle Spiele
  Blackjack.cs
  Roulette.cs
Cards/
  Card.cs
  Deck.cs
```

## Ausführen

```bash
dotnet run
```

Voraussetzung: .NET 8 SDK.

## Hintergrund

Entstanden im Rahmen meiner Ausbildung zum Informatiker EFZ
(Applikationsentwicklung) an der IMS Kantonsschule Baden.
