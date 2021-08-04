CREATE PROCEDURE Report
	@start_date DATETIME, 
	@end_date DATETIME
AS
BEGIN
	DECLARE @result TABLE (
			[State] [varchar](20) NULL
		  , [Amount] [decimal](8, 2) NULL
		  , [Created] [datetime] NULL
		  , [Month] [varchar](3) NULL
		  , [MonthTotal] [BIT]
		  , [StateTotal] [BIT]
		  , [GrandTotal] [BIT]);

	DECLARE @month_result TABLE (
			[State] [varchar](20) NULL
		  , [Amount] [decimal](8, 2) NULL
		  , [Created] [datetime] NULL
		  , [Month] [varchar](3) NULL
		  , [MonthTotal] [BIT]
		  , [StateTotal] [BIT]
		  , [GrandTotal] [BIT]);

	DECLARE @state_result TABLE (
			[State] [varchar](20) NULL
		  , [Amount] [decimal](8, 2) NULL
		  , [Created] [datetime] NULL
		  , [Month] [varchar](3) NULL
		  , [MonthTotal] [BIT]
		  , [StateTotal] [BIT]
		  , [GrandTotal] [BIT]);

	DECLARE @total_result TABLE (
			[State] [varchar](20) NULL
		  , [Amount] [decimal](8, 2) NULL
		  , [Created] [datetime] NULL
		  , [Month] [varchar](3) NULL
		  , [MonthTotal] [BIT]
		  , [StateTotal] [BIT]
		  , [GrandTotal] [BIT]);

	INSERT INTO @result
	SELECT [Customer].[State]
		 , [Booking].[Amount]
		 , [Booking].[Created]
		 , FORMAT(([Booking].[Created]), 'MMM') AS [Month]
		 , 0 AS [MonthTotal]
		 , 0 AS [StateTotal]
		 , 0 AS [GrandTotal]
	FROM [Carly Challenge].[dbo].[tblBookingTest] [Booking]
	JOIN [Carly Challenge].[dbo].[tblCustomerTest] [Customer] ON [Customer].[CustomerID] = [Booking].[CustomerID]
	WHERE [Booking].[Created] >= @start_date AND [Booking].[Created] <= @end_date

	INSERT INTO @month_result
	SELECT [State]
		 , SUM([Amount]) AS [Amount]
		 , MAX([Created]) AS [Created]
		 , [Month]
		 , 1 AS [MonthTotal]
		 , 0 AS [StateTotal]
		 , 0 AS [GrandTotal]
	FROM @result
	GROUP BY [State], [Month]

	INSERT INTO @state_result
	SELECT [State]
		 , SUM([Amount]) AS [Amount]
		 , MAX([Created]) AS [Created]
		 , '' AS [Month]
		 , 1 AS [MonthTotal]
		 , 1 AS [StateTotal]
		 , 0 AS [GrandTotal]
	FROM @result
	GROUP BY [State]

	INSERT INTO @total_result
	SELECT Max([State])
		 , SUM([Amount]) AS [Amount]
		 , MAX([Created]) AS [Created]
		 , '' AS [Month]
		 , 1 AS [MonthTotal]
		 , 1 AS [StateTotal]
		 , 1 AS [GrandTotal]
	FROM @result

	INSERT INTO @result
	SELECT *
	FROM @month_result

	INSERT INTO @result
	SELECT *
	FROM @state_result

	INSERT INTO @result
	SELECT *
	FROM @total_result

	SELECT CASE
			WHEN [GrandTotal] = 1 THEN 'Total'
			WHEN [StateTotal] = 1 THEN [State] + ' Total'
			WHEN [MonthTotal] = 1 THEN [State] + ' ' + [Month]
			ELSE [State]
			END AS [State]
			, FORMAT([Amount], 'C', 'en-AU') AS [Amount],
			CASE
			WHEN [MonthTotal] = 1 THEN ' '
			ELSE FORMAT([Created], 'dd/MM/yyyy')
			END AS [Date]
			, CASE
			WHEN [MonthTotal] = 1 THEN ' '
			ELSE FORMAT([Created], 'MMM')
			END AS [Month]
	FROM @result
END
GO
