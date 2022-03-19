USE FlashMinder
GO 
BEGIN
IF OBJECT_ID (N'[dbo].[trg_UpdateFlashcard]') IS NOT NULL 
DROP TRIGGER [dbo].[trg_UpdateFlashcard]
IF OBJECT_ID (N'[dbo].[trg_UpdateCategory]') IS NOT NULL 
DROP TRIGGER [dbo].[trg_UpdateCategory]
IF OBJECT_ID (N'[dbo].[trg_UpdateFlashcardCategory]') IS NOT NULL 
DROP TRIGGER [dbo].[trg_UpdateFlashcardCategory]
IF OBJECT_ID (N'[dbo].[trg_UpdateFlashcardAlgorithm]') IS NOT NULL 
DROP TRIGGER [dbo].[trg_UpdateFlashcardAlgorithm]

END

GO
CREATE TRIGGER trg_UpdateFlashcard ON [dbo].[Flashcard] AFTER UPDATE AS
	UPDATE dbo.Flashcard
	SET ModifiedDate = GETDATE()
	WHERE ID IN (SELECT DISTINCT ID FROM Inserted)

GO
CREATE TRIGGER trg_UpdateCategory ON [dbo].[Category] AFTER UPDATE AS
	UPDATE dbo.Category
	SET ModifiedDate = GETDATE()
	WHERE ID IN (SELECT DISTINCT ID FROM Inserted)
GO
CREATE TRIGGER trg_UpdateFlashcardCategory ON [dbo].[Flashcard_Category] AFTER UPDATE AS
	UPDATE dbo.Flashcard_Category
	SET ModifiedDate = GETDATE()
	WHERE CategoryId IN (SELECT DISTINCT CategoryId FROM Inserted)

GO
CREATE TRIGGER trg_UpdateFlashcardAlgorithm ON [dbo].[Flashcard_Algorithm_Data] AFTER UPDATE AS
	UPDATE dbo.Flashcard_Algorithm_Data
	SET ModifiedDate = GETDATE()
	WHERE ID IN (SELECT DISTINCT ID FROM Inserted)
