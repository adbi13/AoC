DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_input
GO

CREATE TABLE AoC2025_Day01.dbo.day01_input (
	  rowId INT IDENTITY(1, 1) PRIMARY KEY
	, combination VARCHAR(10)
)
GO

CREATE OR ALTER VIEW dbo.vw_day01_inputLoadView AS
SELECT combination
FROM AoC2025_Day01.dbo.day01_input
GO

BULK INSERT dbo.vw_day01_inputLoadView
FROM '/input01.txt'  WITH (TABLOCK)
GO


DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_parsedInput
GO

SELECT
	  rowId														AS rowId
	, IIF(SUBSTRING(combination, 1, 1) = 'L', 'left', 'right')	AS direction
	, CAST(LTRIM(combination, 'LR') AS INT)						AS distance
INTO AoC2025_Day01.dbo.day01_parsedInput
FROM AoC2025_Day01.dbo.day01_input
GO


DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_directedDistances
GO

SELECT
	  rowId														AS rowId
	, direction													AS direction
	, IIF(direction = 'left', -distance, distance)				AS distance
INTO AoC2025_Day01.dbo.day01_directedDistances
FROM AoC2025_Day01.dbo.day01_parsedInput
GO


DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_rawPositions
GO

SELECT
	  rowId							AS rowId
	, direction						AS previousDirection
	, 50 + SUM(distance) OVER (
		ORDER BY rowId
		ROWS UNBOUNDED PRECEDING
	)								AS rawPosition
INTO AoC2025_Day01.dbo.day01_rawPositions
FROM AoC2025_Day01.dbo.day01_directedDistances
GO


DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_positions
GO

SELECT
	  rowId								AS rowId
	, ((rawPosition % 100) + 100) % 100	AS position
INTO AoC2025_Day01.dbo.day01_positions
FROM AoC2025_Day01.dbo.day01_rawPositions
GO


DROP TABLE IF EXISTS AoC2025_Day01.dbo.day01_resultPart1
GO

SELECT
	COUNT(*)	AS result
INTO AoC2025_Day01.dbo.day01_resultPart1
FROM AoC2025_Day01.dbo.day01_positions
WHERE position = 0
GO


DECLARE positionsCursor CURSOR FOR
SELECT
	  LAG (rawPosition)	OVER (ORDER BY rowId)	AS actualPosition
	, rawPosition								AS nextPosition
FROM AoC2025_Day01.dbo.day01_rawPositions
ORDER BY rowId

DECLARE
	  @actualPosition INT
	, @nextPosition INT
	, @zeroCount INT = 0

OPEN positionsCursor
FETCH NEXT FROM positionsCursor INTO @actualPosition, @nextPosition


WHILE @@FETCH_STATUS = 0
BEGIN
	IF @actualPosition < @nextPosition
	BEGIN
		WITH position_range AS (
			SELECT ISNULL(@actualPosition, 50) AS position
			UNION ALL
			SELECT position + 1
			FROM position_range
			WHERE position < @nextPosition - 1
		)
		SELECT @zeroCount = @zeroCount + COUNT(*)
		FROM position_range
		WHERE ((position % 100) + 100) % 100 = 0
		OPTION (MAXRECURSION 0)
	END
	ELSE
	BEGIN
		WITH position_range AS (
			SELECT ISNULL(@actualPosition, 50) AS position
			UNION ALL
			SELECT position - 1
			FROM position_range
			WHERE position > @nextPosition + 1
		)
		SELECT @zeroCount = @zeroCount + COUNT(*)
		FROM position_range
		WHERE ((position % 100) + 100) % 100 = 0
		OPTION (MAXRECURSION 0)
	END

	FETCH NEXT FROM positionsCursor INTO @actualPosition, @nextPosition
END

CLOSE positionsCursor
DEALLOCATE positionsCursor

SELECT @zeroCount AS result
