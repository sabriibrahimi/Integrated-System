# Hotel External Integration Assignment

At this link, the starter code with the solution from the second laboratory exercise is provided.

---

## PART 1: Integration with an External System — 30 points

The task is to integrate with an external system for reviewing room reviews.

The external system exposes the following endpoint:

```http
GET /api/roomreviews/byroom/{roomId}/paged
```

**Description:** Returns paginated results for a specific hotel room.

The full documentation of the questions system is available at this link.

**URL of the comments system:**

```text
https://integriranisistemi.finki.ukim.mk
```

When calling the following endpoint in our application:

```http
GET /api/room/{id}
```

The response should be enriched with the first **5 reviews** given for the room, fetched from the external system.

### API Key

An API key is required to access the external system:

```text
gSAOEjaqdZW3MhlJL4miLerblYwlpq9W
```

The key is sent in the following header on every request:

```http
X-Api-Key: gSAOEjaqdZW3MhlJL4miLerblYwlpq9W
```

> **Note:** Points for this part will be awarded only if the API key is properly stored, either through **secrets** or *
*environment variables**.

Results are fetched **on-demand** from the external system.

```json
{
  items: [
    {
      id,
      roomId,
      reviewerName,
      comment,
      rating,
      createdAt
    }
  ],
  page,
  pageSize,
  totalCount,
  totalPages
}
```

### Optional — 10 points

To reduce the number of calls, a cache that refreshes every hour must be implemented.

---

## PART 2: Application Security — 20 points

We want to open our system to external systems, but for security reasons API keys must be used.

An **API Key Middleware** must be created that authenticates only users with a key issued by you.

To prevent excessive load on the application, a **rate limit** must be set on at least one endpoint.

---

## PART 3: Accepting External Calls — Inbound REST — 30 points

Since our hotel collaborates with external capacity management platforms, you need to enable their systems to
automatically send data about new rooms.

For that purpose, access must be enabled through the following endpoints:

```http
POST /api/external/room
```

**Description:** Accepts a request, validates the basic structure, and returns `202 Accepted` with an ID.

```http
GET /api/external/room/{id}/status
```

**Description:** Returns the current processing status.

---

## InboundRoomRequest Format

An `InboundRoomRequest` must be created with the following format:

```json
{
  "hotelId": "string",
  "roomNumber": "string",
  "capacity": "integer",
  "pricePerNight": "decimal",
  "status": "string"
}
```

Only requests sent with a valid key issued by your system should be processed.

---

## Inbound Request Storage

Requests received from the external system must be saved in a table named:

```text
InboundAttendanceEntries
```

The table must contain the following information:

| Field           | Type / Description                                                  |
|-----------------|---------------------------------------------------------------------|
| `RawPayload`    | `string`                                                            |
| `Status`        | `ProcessingStatus` — `Pending`, `Processing`, `Completed`, `Failed` |
| `ApiClientId`   | `Guid`                                                              |
| `ReceivedAt`    | `DateTime`                                                          |
| `ProcessedAt`   | `DateTime?`                                                         |
| `ErrorMessage`  | `string?`                                                           |
| `CreatedRoomId` | `Guid?`                                                             |

Requests waiting to be processed have the status:

```text
Pending
```

---

## Background Processing with Quartz

Using a **Quartz job**, every **30 seconds** you must process **5 requests** sent from the external system.

