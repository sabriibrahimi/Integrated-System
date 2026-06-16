CREATE TABLE IF NOT EXISTS "ConsultationApi"(
    "Id" TEXT NOT NUL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "ApiKey" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "RateLimitMinutes" INTEGER NOT NULL
)
INSERT INTO "ConsultationApi"
(
    "Id",
    "Name",
    "ApiKey",
    "IsActive",
    "RateLimitMinutes"
) VALUES
(
    '22222222-2222-2222-2222-222222222222',
    'External Partner',
    'sabrisabrisabrisabri12',
    1,
    60
)   