# matbestille_prosjekt
gruppe prosjekt om matbestilling

# 📘 Git-regler for gruppeprosjektet

Disse reglene gjelder for alle på gruppen. Følg dem så vi unngår konflikter og holder koden ryddig.

---

## 👥 Gruppemedlemmer og ansvar

- Alle har ansvar for å følge disse reglene
- Spør alltid hvis du er usikker – ikke gjet og push

---

## 🌿 Branch-regler

- `main` er **hovedgrenen** – her ligger alltid fungerende kode
- **Aldri push direkte til `main`**
- Hver person lager sin egen branch:

````bash
git checkout -b feature/ditt-navn
````

- Navngi branches slik:
  - `feature/login`
  - `feature/database`
  - `bugfix/krasjfeil`

- Når du er ferdig → lag en **Pull Request** på GitHub og vent på godkjenning

---

## 📥 Slik jobber du daglig

### 1. Alltid start med å hente siste endringer

````bash
git pull origin main
````

> ⚠️ Gjør dette **hver gang** før du begynner å jobbe – ellers risikerer du konflikter

### 2. Legg til endringene dine

````bash
git add .
````

Eller legg til én spesifikk fil:

````bash
git add filnavn.cs
````

### 3. Commit med en tydelig melding

````bash
git commit -m "kort beskrivelse av hva du gjorde"
````

Gode eksempler:
- `"La til metode for brukerinnlogging"`
- `"Fikset krasj i Main-metoden"`
- `"La til kommentarer i Program.cs"`

Dårlige eksempler:
- `"endringer"`
- `"fix"`
- `"asdfgh"`

### 4. Push til **din egen branch**

````bash
git push origin feature/ditt-navn
````

---

## 🔀 Pull Request (PR) – slik slår vi sammen kode

1. Gå til GitHub → klikk **"Compare & pull request"**
2. Skriv kort hva du har gjort
3. En annen på gruppen **gjennomgår koden** før den godkjennes
4. Etter godkjenning → klikk **"Merge pull request"**
5. Slett branchen etter merge

---

## ⚠️ Viktige regler

| Regel | Hvorfor |
|---|---|
| Aldri push til `main` direkte | Unngår å ødelegge fungerende kode |
| Pull før du begynner | Unngår konflikter |
| Tydelige commit-meldinger | Alle forstår hva som er gjort |
| Én ting per commit | Lettere å spore feil |
| Ikke commit `.exe` eller `bin/`-mappen | Disse genereres automatisk |

---

## 🛑 Hvis du får konflikt (merge conflict)

1. Åpne filen med konflikten – du vil se:
