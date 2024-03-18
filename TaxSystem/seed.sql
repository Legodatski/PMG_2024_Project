USE [TaxSystem]

INSERT
INTO [AspNetUsers]
(Id, UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, FirstName, LastName)
VALUES
('f0c12865-d473-47d7-add0-44267ff5b26c', 'Работник1', 'worker@gmail.com', 0, '+359615486348', 0, 0, 0, 0, 'Петър', 'Маринов'),
('ef01363e-a1cc-4eef-b243-87738e4299fa', 'Потребител1', 'user@gmail.com', 0, '+35978935164', 0, 0, 0, 0, 'Ивайло', 'Иванов');

INSERT
INTO [AspNetUserRoles]
(UserId, RoleId)
VALUES
('f0c12865-d473-47d7-add0-44267ff5b26c', 'c9b40704-8420-49bf-abba-6167085695bf'),
('ef01363e-a1cc-4eef-b243-87738e4299fa', '4b446f17-0a3c-4284-aaed-76a7a684f65e');

INSERT
INTO [Desks]
(Id, WorkerId)
VALUES
(1, 'f0c12865-d473-47d7-add0-44267ff5b26c')

INSERT
INTO [