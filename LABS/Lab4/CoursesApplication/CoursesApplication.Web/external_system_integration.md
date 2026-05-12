The link contains the starter code with the solution from the second laboratory exercise.

## PART 1: Integration with an external system

The task is to integrate with an external system for reviewing questions for a specific exam attempt. The external system exposes the following endpoints:

- `GET /api/attemptquestions` — returns all attempt questions
- `GET /api/attemptquestions/paged?page=1&pageSize=5` — returns paginated attempt questions
- `GET /api/attemptquestions/{id}` — returns an attempt question by ID
- `GET /api/attemptquestions/byattempt/{attemptId}` — returns all questions for a specific attempt
- `GET /api/attemptquestions/byattempt/{attemptId}/paged` — returns paginated questions for a specific attempt

The full documentation of the question system is available at the link

Question system URL: https://integriranisistemi.finki.ukim.mk

When calling `GET /api/examattempt/{id}` in our application, the response should be enriched with the first 5 questions that were given for that attempt, retrieved from the external system.

Access to the external system requires an API key: gSAOEjaqdZW3Rh1JL4miLerb1Ywlpq9W

The key is sent in the `X-Api-Key` header on every request

**Note:** Points for this part will only be awarded if the API key is properly stored (via secrets or environment variables).

Questions are fetched on-demand from the external system.

**Optional (20 points):** To reduce the number of requests, a cache that refreshes every hour should be implemented.


1. Add QuestionSystem section in appsettings.json
2. Save API key using dotnet user-secrets
3. Create QuestionSystemSettings class
4. Register settings in Program.cs
5. Create external DTOs matching the external API JSON
6. Create IAttemptQuestionApiClient
7. Create AttemptQuestionApiClient typed client
8. Register AddHttpClient with BaseUrl, Timeout, and X-Api-Key header
9. Modify ExamAttemptResponse to include Questions
10. In GET /api/examattempt/{id}, fetch local attempt first
11. Call external API endpoint:/api/attemptquestions/byattempt/{attemptId}/paged?page=1&pageSize=5
12. Add returned questions into the response
13. Optional: add IMemoryCache for 1 hour
14. Test with Postman/Swagger




## PART 2: Application Security

We want to expose our system to external systems, but for security reasons API keys must be used.

- An API Key Middleware should be created that authenticates only users with a key issued by us.

To prevent excessive load on the application, a rate limit must be applied to at least one endpoint.


