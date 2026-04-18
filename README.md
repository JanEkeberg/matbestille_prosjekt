# Matbestille_prosjekt
Gruppe prosjekt om matbestilling
---
Prosjekt beskrivelse skrives av gruppleder
---


--- 
⚠️  Viktige: Git regler vil slettes etter alle i gruppe forstått og enig
---



# Git-regler for gruppeprosjektet

Disse reglene gjelder for alle på gruppen. Følg dem så vi unngår konflikter og holder koden ryddig.

---

##  Gruppemedlemmer og ansvar

- Alle har ansvar for å følge disse reglene
- Spør alltid hvis du er usikker – ikke gjet og push

---

## Branch-regler

- `main` er **hovedgrenen** – her ligger alltid fungerende kode
- **Aldri push direkte til `main`**
- Hver person lager sin egen branch:

```bash
git checkout -b feature/ditt-navn_oppgaven eller _hva_du _jobber_med
```

- Når du er ferdig → lag en **Pull Request** på GitHub og vent på godkjenning

---

## Slik jobber du daglig !

### 1. Alltid start med å hente siste endringer

```bash
git pull origin main
```
> ⚠️ Gjør dette **hver gang** før du begynner å jobbe – ellers risikerer du konflikter

### 2. Legg til endringene dine

```bash
git add .
```

Eller legg til én spesifikk fil:

```bash
git add filnavn.cs
```

### 3. Commit med en tydelig melding

```bash
git commit -m "kort beskrivelse av hva du gjorde"
```

Gode eksempler:
- `"La til metode for brukerinnlogging"`
- `"Fikset feilen"`
- `"Endret fronten til side 2"`

Dårlige eksempler:
- `"endringer"`
- `"fix"`
- `"asdfgh"`

### 4. Push til **din egen branch**

```bash
git push origin feature/branch_navn
```

---

## Pull Request (PR) – slik slår vi sammen kode

1. Gå til GitHub → klikk **"Compare & pull request"**
2. Skriv kort hva du har gjort
3. En annen på gruppen **gjennomgår koden** før den godkjennes
4. Etter godkjenning → klikk **"Merge pull request"**
5. Slett branchen etter merge

---

## Viktige regler

| Regel | Hvorfor |
|---|---|
| Tydelige commit-meldinger | Alle forstår hva som er gjort |
| Aldri push eller skrive kode til `main` direkte | Unngår å ødelegge fungerende kode |
| Pull før du begynner | Unngår konflikter |
| Én ting per commit | Lettere å spore feil |
| Ikke commit `.exe` eller `bin/`-mappen | Disse genereres automatisk |

---

## 🛑 Hvis du får konflikt (merge conflict)

1. Prate med minst en av gruppe medlem
2. Bestem hvilken kode som skal beholdes (eller kombiner begge)
3. Slett konfliktmarkørene (`<<<<`, `====`, `>>>>`)
4. Lagre, og kjør:

```bash
git add .
git commit -m "løste merge conflict i filnavn.cs"
```

---

##  Filer som ikke skal committes (.gitignore)
Disse håndteres automatisk av `.gitignore` (Visual Studio-malen):
- `bin/`
- `obj/`
- `.vs/`
- `*.user`

> Sjekk at `.gitignore` ligger i rooten av prosjektet!

---

## Git Kommandor som man bruke oftest:

```bash
git pull origin main          # Hent siste endringer
git checkout -b feature/navn  # Lag ny branch
git add .                     # Legg til alle endringer
git commit -m "melding"       # Lagre med beskrivelse
git push origin feature/navn  # Push til din branch
```