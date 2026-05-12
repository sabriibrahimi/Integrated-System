CREATE TABLE IF NOT EXISTS "ApiClients" (
                                            "Id" TEXT NOT NULL PRIMARY KEY,
                                            "Name" TEXT NOT NULL,
                                            "ApiKey" TEXT NOT NULL,
                                            "IsActive" INTEGER NOT NULL,
                                            "RateLimitMinutes" INTEGER NOT NULL
);

INSERT INTO "ApiClients" ("Id", "Name", "ApiKey", "IsActive", "RateLimitMinutes")
VALUES (
           '99999999-9999-9999-9999-999999999999',
           'External Partner',
           'sabrisabrisabrisabri12',
           1,
           60
       );