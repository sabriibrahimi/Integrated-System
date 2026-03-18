CREATE TABLE IF NOT EXISTS Semesters
(
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL,
    StartDate TEXT NOT NULL,
    EndDate TEXT NOT NULL
);


CREATE TABLE IF NOT EXISTS Courses
(
    Id TEXT NOT NULL PRIMARY KEY,
    Title TEXT NOT NULL,
    Description TEXT NULL,
    Ects INTEGER NOT NULL,
    Category INTEGER NOT NULL,
    SemesterId TEXT NOT NULL,
    CONSTRAINT FK_Courses_Semesters
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id) ON DELETE CASCADE
    );


CREATE TABLE IF NOT EXISTS AspNetUsers
(
    Id TEXT NOT NULL PRIMARY KEY,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    DateOfBirth TEXT NOT NULL,

    UserName TEXT NULL,
    NormalizedUserName TEXT NULL,
    Email TEXT NULL,
    NormalizedEmail TEXT NULL,
    EmailConfirmed INTEGER NOT NULL,
    PasswordHash TEXT NULL,
    SecurityStamp TEXT NULL,
    ConcurrencyStamp TEXT NULL,
    PhoneNumber TEXT NULL,
    PhoneNumberConfirmed INTEGER NOT NULL,
    TwoFactorEnabled INTEGER NOT NULL,
    LockoutEnd TEXT NULL,
    LockoutEnabled INTEGER NOT NULL,
    AccessFailedCount INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS AspNetRoles
(
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NULL,
    NormalizedName TEXT NULL,
    ConcurrencyStamp TEXT NULL
);

CREATE TABLE IF NOT EXISTS AspNetUserClaims
(
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ClaimType TEXT NULL,
    ClaimValue TEXT NULL,
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
    );

CREATE TABLE IF NOT EXISTS AspNetUserLogins
(
    LoginProvider TEXT NOT NULL,
    ProviderKey TEXT NOT NULL,
    ProviderDisplayName TEXT NULL,
    UserId TEXT NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
    );

CREATE TABLE IF NOT EXISTS AspNetUserRoles
(
    UserId TEXT NOT NULL,
    RoleId TEXT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
    );

CREATE TABLE IF NOT EXISTS AspNetUserTokens
(
    UserId TEXT NOT NULL,
    LoginProvider TEXT NOT NULL,
    Name TEXT NOT NULL,
    Value TEXT NULL,
    PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
    );

CREATE TABLE IF NOT EXISTS AspNetRoleClaims
(
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    RoleId TEXT NOT NULL,
    ClaimType TEXT NULL,
    ClaimValue TEXT NULL,
    CONSTRAINT FK_AspNetRoleClaims_AspNetRoles
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
    );


CREATE TABLE IF NOT EXISTS Teachings
(
    Id TEXT NOT NULL PRIMARY KEY,
    Role INTEGER NOT NULL,
    CourseId TEXT NOT NULL,
    SemesterId TEXT NOT NULL,
    UserId TEXT NOT NULL,

    CONSTRAINT FK_Teachings_Courses
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE,

    CONSTRAINT FK_Teachings_Semesters
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id) ON DELETE CASCADE,

    CONSTRAINT FK_Teachings_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
    );


CREATE TABLE IF NOT EXISTS Enrollments
(
    Id TEXT NOT NULL PRIMARY KEY,
    EnrolledAt TEXT NOT NULL,
    UserId TEXT NOT NULL,
    CourseId TEXT NOT NULL,

    CONSTRAINT FK_Enrollments_AspNetUsers
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,

    CONSTRAINT FK_Enrollments_Courses
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE
    );


CREATE UNIQUE INDEX IF NOT EXISTS IX_AspNetUsers_NormalizedUserName
    ON AspNetUsers (NormalizedUserName);

CREATE INDEX IF NOT EXISTS IX_AspNetUsers_NormalizedEmail
    ON AspNetUsers (NormalizedEmail);

CREATE UNIQUE INDEX IF NOT EXISTS IX_AspNetRoles_NormalizedName
    ON AspNetRoles (NormalizedName);

CREATE INDEX IF NOT EXISTS IX_AspNetUserClaims_UserId
    ON AspNetUserClaims (UserId);

CREATE INDEX IF NOT EXISTS IX_AspNetUserLogins_UserId
    ON AspNetUserLogins (UserId);

CREATE INDEX IF NOT EXISTS IX_AspNetUserRoles_RoleId
    ON AspNetUserRoles (RoleId);

CREATE INDEX IF NOT EXISTS IX_AspNetRoleClaims_RoleId
    ON AspNetRoleClaims (RoleId);

CREATE INDEX IF NOT EXISTS IX_Courses_SemesterId
    ON Courses (SemesterId);

CREATE INDEX IF NOT EXISTS IX_Teachings_CourseId
    ON Teachings (CourseId);

CREATE INDEX IF NOT EXISTS IX_Teachings_SemesterId
    ON Teachings (SemesterId);

CREATE INDEX IF NOT EXISTS IX_Teachings_UserId
    ON Teachings (UserId);

CREATE INDEX IF NOT EXISTS IX_Enrollments_UserId
    ON Enrollments (UserId);

CREATE INDEX IF NOT EXISTS IX_Enrollments_CourseId
    ON Enrollments (CourseId);