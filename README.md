# Redis Chat Backend API

Een schaalbare en real-time chatbackend gebouwd met **ASP.NET Core Web API** en **Redis Streams + Pub/Sub**.  
Deze API beheert chatrooms, berichtenopslag, real-time notificaties en historiek.

---

## ⚙️ Technologieën

- 🧠 **C# / ASP.NET Core Web API**
- 🔥 **Redis** (Streams & Pub/Sub)
- 🧱 **StackExchange.Redis**
- 🐳 **Docker** (voor Redis lokaal)
- 📁 Clean architecture: Repository / Service / Controller lagen

---

## 🚀 Functionaliteit

- ✅ Chatroom aanmaken
- ✅ Chatroom joinen
- ✅ Bericht verzenden naar een groep
- ✅ Berichten realtime ontvangen (Pub/Sub)
- ✅ Berichten historisch ophalen (Streams)
- ✅ Redis als centrale message engine

---

## 🧪 Redis Structuur

| Key                      | Type     | Functie                          |
|--------------------------|----------|----------------------------------|
| `chat:rooms`             | Set      | Bevat alle geldige roomIds       |
| `chat_stream:<roomId>`   | Stream   | Volledige opslag van berichten   |
| `chat:<roomId>`          | Pub/Sub  | Realtime notificatiekanaal       |

---

## 📦 API Endpoints

### 🔹 `POST /api/chat/create`
Maak een nieuwe chatroom aan.

```json
{
  "roomId": "room42"
}
