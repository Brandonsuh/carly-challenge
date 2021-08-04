WITH CTE AS
(
SELECT *, ROW_NUMBER() OVER (PARTITION BY [Email] ORDER BY [Email]) AS RN
FROM [dbo].[tblUserTest]
)
DELETE FROM CTE WHERE RN <> 1
GO

ALTER TABLE [dbo].[tblUserTest] 
ADD [UserID] [int] IDENTITY(1,1) NOT NULL;
GO

ALTER TABLE [dbo].[tblUserTest]
ADD CONSTRAINT PK_tblUserTest PRIMARY KEY CLUSTERED ( [UserID] ASC );
GO

ALTER TABLE [dbo].[tblUserTest]
ADD CONSTRAINT email_unique UNIQUE ([Email]);
GO
