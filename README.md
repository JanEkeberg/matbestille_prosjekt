# 🍽️ MATBESTILLE_PROSJEKT – Prosjekt Struktur

## 📁 Mappe-struktur


MATBESTILLE_PROSJEKT/
│
├── Program.cs
├── Models/
├── Enums/
├── Interfaces/
├── Repositories/
├── Services/
├── Data/
└── Tests/


---

## 📂 Hva går i hver mappe

### 📦 Models
Inneholder alle kjerneklasser (data + regler):

- User (abstrakt/baseklasse)
- Customer
- Employee
- Admin
- MenuItem
- OrderItem
- Order
- Invoice

Representerer systemets objekter og struktur

---

### 🔢 Enums
- OrderStatus

Mulige statuser:
- Placed
- Delivered
- ConfirmedReceived
- Invoiced

---

### 📑 Interfaces
- IOrderRepository
- ICustomerRepository
- IInvoiceService

Definerer kontrakter (hva systemet skal gjøre, ikke hvordan)

---

### 💾 Repositories
- JsonOrderRepository
- JsonCustomerRepository

Ansvar:
- Lese fra JSON
- Skrive til JSON

---

### ⚙️ Services
- OrderService
- ProductService
- AdminService
- InvoiceService

Inneholder forretningslogikk:
- Opprette ordre
- Oppdatere status
- Håndtere produkter
- Generere faktura

---

### 🗂️ Data (for at vi trenger persistent storage siden vi lager Console App)
- orders.json
- customers.json (Vi kan tenke at admin og canteen medarbeider er fixed! Kunder kan registere.)

Lagrer bestillinger permanent

---

### 🧪 Tests
- Enhetstester

Sikrer at:
- Logikk fungerer riktig
- Endringer ikke ødelegger systemet

---

## 🚀 Kort flyt (hvordan systemet fungerer)

1. Kunde lager en ordre
2. Ordre lagres i JSON via Repository
3. Ansatt markerer som levert
4. Kunde bekrefter mottatt
5. Admin lager faktura

---

## 🎯 Mål

- Enkel konsollapplikasjon
- Klar struktur
- Separasjon av ansvar (Models / Services / Data)