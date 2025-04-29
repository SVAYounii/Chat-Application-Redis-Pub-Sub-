# Redis Chat Backend API

Een schaalbare en real-time chat backend gebouwd met **ASP.NET Core Web API** en **Redis Streams + Pub/Sub**.  
Deze API beheert chatrooms, berichtenopslag, real-time notificaties en historiek.

---

## 🚀 Technologieën

- **C# / ASP.NET Core Web API**
- **Redis** (Streams & Pub/Sub)
- **StackExchange.Redis**
- **Docker** (voor lokale Redis-omgeving)
- **Clean architecture**: gescheiden Repository / Service / Controller lagen

---

## ✅ Functionaliteit

- Chatroom aanmaken
- Chatroom joinen
- Bericht versturen naar een groep
- Realtime notificaties via Pub/Sub
- Chatgeschiedenis ophalen via Redis Streams

---

## 🧠 Redis Data Structuur

| Key                      | Type     | Omschrijving                               |
|--------------------------|----------|--------------------------------------------|
| `chat:rooms`             | Set      | Lijst van bestaande chatroom-ID's          |
| `chat_stream:<roomId>`   | Stream   | Volledige berichtgeschiedenis van een room |
| `chat:<roomId>`          | Pub/Sub  | Kanaal voor realtime meldingen             |

---

## 📡 API Endpoints

### 🔹 POST `/api/chat/create`

Aanmaken van een nieuwe chatroom.

**Request Body:**
```json
{
  "roomId": "room42"
}
```

**Responses:**
- `200 OK` – Room succesvol aangemaakt.
- `400 Bad Request` – Room bestaat al.

---

### 🔹 POST `/api/chat/join`

Checkt of een chatroom bestaat, zodat de gebruiker kan joinen.

**Request Body:**
```json
{
  "roomId": "room42"
}
```

**Responses:**
- `200 OK` – Room bestaat.
- `404 Not Found` – Room bestaat niet.

---

### 🔹 POST `/api/chat/send`

Stuurt een bericht naar een bestaande chatroom.  
Bericht wordt opgeslagen in de stream en tegelijk gepusht via Pub/Sub.

**Request Body:**
```json
{
  "roomId": "room42",
  "sender": "user123",
  "message": "Hoi iedereen!"
}
```

**Responses:**
- `200 OK` – Bericht succesvol verzonden.
- `404 Not Found` – Room bestaat niet.

---

### 🔹 GET `/api/chat/history/{roomId}`

Haalt de volledige berichtgeschiedenis van een room op (alle berichten vanaf `"0-0"`).

**Response Body:**
```json
[
  {
    "sender": "user123",
    "message": "Hoi iedereen!",
    "timestamp": "2025-04-29T13:47:00Z"
  },
  {
    "sender": "user456",
    "message": "Welkom bij de groep!",
    "timestamp": "2025-04-29T13:48:05Z"
  }
]
```

**Responses:**
- `200 OK` – Berichten gevonden.
- `404 Not Found` – Room bestaat niet.

---

## 🐳 Redis lokaal draaien (Docker)

Je kunt Redis lokaal starten met Docker:

```bash
docker run --name redis -p 6379:6379 -d redis
```

---

## 📁 Projectstructuur (Clean Architecture)

```
Application/
  └── Chat/
      └── ChatService.cs

Domain/
  ├── DTO/
  │   └── ChatMessage.cs
  └── Interface/
      └── Chat/
          ├── IChatRepository.cs
          └── IChatService.cs

Infrastructure/
  └── Chat/
      └── ChatRepository.cs

Controllers/
  └── ChatController.cs
```

---

## ✅ Best Practices toegepast

- **Services** bevatten alle businesslogica (zoals validatie, key-constructie, room-checks).
- **Repositories** doen uitsluitend Redis-CRUD (geen logica, geen hardcoded keys).
- **Stream + Pub/Sub** worden gecombineerd:
  - Pub/Sub → realtime meldingen
  - Stream → permanente opslag
- **Async/await** consistent toegepast
- **Geen hardcoded Redis keys** in repositories

---

## 🧠 Redis Stream ID Uitleg

Elke Redis Stream Entry heeft een ID in dit formaat:

```
<timestamp>-<sequence>
```

Bijvoorbeeld:

- `1714403200000-0`
- `1714403200000-1`

De timestamp is in milliseconden sinds epoch.  
Als meerdere berichten op exact hetzelfde moment worden gepusht, worden de sequence-nummers verhoogd.

### `"0-0"` in XREAD

Wanneer je `XREAD` doet vanaf `"0-0"`, dan lees je:

- **Alle berichten**, ongeacht timestamp of sequence
- Van het allereerste bericht tot het meest recente

---

## 👨‍💻 Auteur

**Younes Aroiych**  
Met passie voor schaalbare real-time systemen, software design en Redis-architecturen.
