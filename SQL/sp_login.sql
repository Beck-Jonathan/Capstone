USE Night_Rider;

GO
print '' print '*** creating sp_authenticate_employee ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_authenticate_employee] (
  @Username [nvarchar](50),
  @Password_Hash [nvarchar](100)
)
AS
BEGIN
  SELECT
    er.[Role_ID],
    e.[Employee_ID],
    e.[Given_Name],
    e.[Family_Name],
    e.[Address],
    e.[Address2],
    e.[City],
    e.[State],
    e.[Country],
    e.[Zip],
    e.[Phone_Number],
    e.[Email],
    e.[Position]
  FROM [dbo].[Employee] e
  LEFT JOIN [dbo].[Employee_Role] er ON e.[Employee_ID] = er.[Employee_ID]
  JOIN [dbo].[Login] l ON e.[Employee_ID] = l.[Employee_ID]
  WHERE
    l.[Active] = 1
    AND e.[Is_Active] = 1
    AND l.[Username] = @Username
    AND l.[Password_Hash] = @Password_Hash
END;
GO

GO
print '' print '*** creating sp_authenticate_client_for_security_questions ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_authenticate_client_for_security_questions] (
  @Username [nvarchar](50),
  @Password_Hash [nvarchar](100)
)
AS
BEGIN
  SELECT [Security_Question_1], [Security_Question_2], [Security_Question_3]
  FROM [dbo].[Login]
  WHERE
    [Active] = 1
    AND [Client_ID] IS NOT NULL
    AND [Username] = @Username
    AND [Password_Hash] = @Password_Hash;
END;
GO

print '' print '*** creating sp_authenticate_employee_for_security_questions ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_authenticate_employee_for_security_questions] (
  @Username [nvarchar](50),
  @Password_Hash [nvarchar](100)
)
AS
BEGIN
  SELECT [Security_Question_1], [Security_Question_2], [Security_Question_3]
  FROM [dbo].[Login]
  WHERE
    [Active] = 1
    AND [Employee_ID] IS NOT NULL
    AND [Username] = @Username
    AND [Password_Hash] = @Password_Hash;
END;
GO

-- AUTHOR: Parker Svoboda
Print '***Creating [dbo].[sp_get_security_questions_for_username_retrieval]***' 
 go
CREATE PROCEDURE [dbo].[sp_get_security_questions_for_username_retrieval] (
  @Email [nvarchar](50)
)
AS
BEGIN
  SELECT
	l.[Security_Question_1],
	l.[Security_Question_2],
	l.[Security_Question_3]
  FROM [dbo].[Login] l
  JOIN [dbo].[Employee] e ON e.[Employee_ID] = l.[Employee_ID]
  WHERE
    l.[Active] = 1
    AND e.[Is_Active] = 1
	AND e.[Email] = @Email;
END;
GO

-- AUTHOR: Parker Svoboda
Print '***Creating [dbo].[sp_get_username]***' 
 go
CREATE PROCEDURE [dbo].[sp_get_username] (
  @Email [nvarchar](50),
  @Security_Response_1 [nvarchar](100) = NULL,
  @Security_Response_2 [nvarchar](100) = NULL,
  @Security_Response_3 [nvarchar](100) = NULL
)
AS
BEGIN
  SELECT
	l.[Username]
  FROM [dbo].[Login] l
  JOIN [dbo].[Employee] e ON e.[Employee_ID] = l.[Employee_ID]
  WHERE
    l.[Active] = 1
    AND e.[Is_Active] = 1
    AND e.[Email] = @Email
    AND (l.[Security_Response_1] IS NULL OR l.[Security_Response_1] = @Security_Response_1)
    AND (l.[Security_Response_2] IS NULL OR l.[Security_Response_2] = @Security_Response_2)
    AND (l.[Security_Response_3] IS NULL OR l.[Security_Response_3] = @Security_Response_3);
END;
GO