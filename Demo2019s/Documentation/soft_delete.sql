IF db_id('tr_softdelete_update') IS NOT NULL BEGIN
    USE master
    DROP DATABASE tr_softdelete_update
END
GO

CREATE DATABASE tr_softdelete_update
GO

USE tr_softdelete_update
GO

CREATE TABLE Person (
    Id      INT            NOT NULL,

    Person_name    VARCHAR(128)    NOT NULL,
	Person_Email    VARCHAR(128)           NOT NULL,
	Person_PhoneNr    INT           NOT NULL,

	Order_Id      INT            NULL,
	Order_Deleted_at DATETIME2 NOT NULL,


	Deleted_at DATETIME2 NOT NULL,
	Created_at DATETIME2 NOT NULL DEFAULT Current_timestamp,

	CONSTRAINT PK_Person PRIMARY KEY (Id, Deleted_at),
	CONSTRAINT PK_Person_PersonOrder FOREIGN KEY (Order_Id, Order_Deleted_at) REFERENCES Person_Order(Person_Order_Id, Deleted_at) ON UPDATE CASCADE,

)

CREATE TABLE Person_Order (
    Person_Order_Id      INT NOT NULL,

	Created_at DATETIME2(7) NOT NULL DEFAULT Current_timestamp,
	Deleted_at DATETIME2(7) NOT NULL,

	Person_Id       INT NOT NULL,
	Wash_Id       INT NOT NULL,
	Comment       VARCHAR(128) NOT NULL,

	CONSTRAINT PK_Order PRIMARY KEY (Person_Order_Id, Deleted_at),
)
------------------------------------

DROP TABLE Person
DROP TABLE Person_Order

DECLARE @Time1 DATETIME2
SELECT @Time1 = '2020-03-03'

DECLARE @Time2 DATETIME2
SELECT @Time2 = '9999-12-03'

INSERT INTO Person(Id, Person_name, Person_Email, Person_PhoneNr, Order_Deleted_at, Deleted_at, Created_at) 
VALUES (1, 'Bar', 'Bar@gmail.com', 123123124, @Time2, @Time2, Current_timestamp)

INSERT INTO Person(Id, Person_name, Person_Email, Person_PhoneNr, Order_Deleted_at, Deleted_at, Created_at) 
VALUES (2, 'Foo', 'Foo@gmail.com', 123123123, @Time2, @Time2, Current_timestamp)


DECLARE @PersonId INT
SELECT @PersonId = Id FROM Person Where Person_name like 'Bar'


INSERT INTO Person_Order (Person_Order_Id, Created_at, Deleted_at, Person_Id, Wash_Id, Comment) VALUES (1, Current_timestamp, @Time2, @PersonId, 1, 'Damaged Paint')
SELECT @PersonId = Id FROM Person Where Person_name like 'Foo'
INSERT INTO Person_Order (Person_Order_Id, Created_at, Deleted_at, Person_Id, Wash_Id, Comment) VALUES (2, Current_timestamp, @Time2, @PersonId, 2, 'Comment one')


SELECT 'Initial data'
SELECT * FROM Person

SELECT * FROM Person_Order

DELETE FROM Person
DELETE FROM Person_Order

-------------------------------------------------------------------------------------------
-- Update Person
DECLARE @Time2 DATETIME2
SELECT @Time2 = '2020-03-05'
DECLARE @TimeNow DATETIME2
SELECT @TimeNow = '2020-03-08'

DECLARE @NewOrderId INT
select @NewOrderId = 2

DECLARE @OriginalId INT
SELECT @OriginalId = Id FROM Person Where Id like '2'

DECLARE @Original_Order_Id INT
SELECT @Original_Order_Id = Person_Order_Id FROM Person_Order Where Person_Id like @OriginalId

DECLARE @personId INT
UPDATE Person SET Deleted_at = @TimeNow WHERE Id like @OriginalId
INSERT INTO Person (Id, Person_name, Person_Email, Person_PhoneNr, Order_Id, Order_Deleted_at, Deleted_at, Created_at) 
SELECT Id, Person_name, Person_Email, Person_PhoneNr, @NewOrderId, Order_Deleted_at, '9999-12-03', Created_at FROM Person where Person.Id = '2'

SELECT Person.Id, Person.Person_name, Person.Person_PhoneNr, Person.Order_Id, Person.Order_Deleted_at, Person.Deleted_at, Person.Created_at FROM Person
INNER JOIN Person_Order ON Person_Order.Person_Id = Person.Id WHERE Person_Order.Person_Id = '2'

SELECT 'Full Data after soft update'
SELECT * FROM Person
SELECT 'Correct Data after soft update'
SELECT * FROM Person WHERE  Created_at <= @TimeNow AND (Deleted_at IS NULL OR Deleted_at > @TimeNow)
-------------------------------------------------------------------------------------------
-- Delete Foo from Persons
DECLARE @Time3 DATETIME2
SELECT @Time3 = '2020-03-04' 
DECLARE @TimeNow DATETIME2
SELECT @TimeNow = '2020-03-09' 

UPDATE Person SET Deleted_at=@Time3 WHERE Id like '1'

SELECT 'Full Data after soft delete'
SELECT * FROM Person
SELECT * FROM Person_Order

SELECT 'Correct Data after soft delete'
SELECT * FROM Person WHERE  Created_at <= @TimeNow AND (Deleted_at > @TimeNow)

-- Select deleted Person
SELECT * FROM Person WHERE  Created_at <= @TimeNow AND (Deleted_at < @TimeNow)