
  IF NOT EXISTS (SELECT 1 FROM [Priority] WHERE Code='Normal')
  BEGIN
	INSERT INTO [Priority] (Name,Code,[Color]) VALUES('Normal','Normal','#FFEE9C')
  END

  
  IF NOT EXISTS (SELECT 1 FROM [Priority] WHERE Code='Low')
  BEGIN
	INSERT INTO [Priority] (Name,Code,[Color]) VALUES('Low','Low','Gray')
  END

    IF NOT EXISTS (SELECT 1 FROM [Priority] WHERE Code='High')
  BEGIN
	INSERT INTO [Priority] (Name,Code,[Color]) VALUES('High','High','Red')
  END