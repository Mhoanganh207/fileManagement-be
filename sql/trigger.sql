SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create TRIGGER [dbo].[addChild] 
on [dbo].[Files]
After INSERT
as 
BEGIN

DECLARE @P_Id UNIQUEIDENTIFIER;
SELECT @P_Id = ParentId FROM inserted;

DECLARE @id UNIQUEIDENTIFIER;
SELECT @id = Id FROM inserted;

DECLARE @userId UNIQUEIDENTIFIER;
SELECT @userId = UserId From inserted;



DECLARE @left INT;
SELECT @left = [LEFT] from dbo.Files where Id = @P_Id;

DECLARE @right INT;
SELECT @right = [RIGHT] from dbo.Files where Id = @P_Id;

UPDATE dbo.Files
set [LEFT] = [LEFT] + 2 WHERE UserId = @userId and [Id] != @id and [LEFT] > @left;

UPDATE dbo.Files
set [RIGHT] = [RIGHT] + 2 WHERE UserId = @userId and [Id] != @id and  [RIGHT] > @left;

END
GO
ALTER TABLE [dbo].[Files] ENABLE TRIGGER [addChild]
GO
