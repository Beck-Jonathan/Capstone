USE Night_Rider;
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