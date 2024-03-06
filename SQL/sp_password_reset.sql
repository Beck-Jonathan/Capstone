USE Night_Rider;
GO
print '' print '*** creating sp_insert_password_reset ***'
GO
-- Initial Creator: Jared Hutton
-- Creation Date: 2024-02-24
-- Last Modified: Jared Hutton
-- Modification Description: Initial Creation
-- Stored Procedure Description: Insert new password reset object
CREATE PROCEDURE [dbo].[sp_insert_password_reset] (
  @Username [nvarchar](50),
  @Verification_Code [char](6)
)
AS 
BEGIN
  INSERT INTO [dbo].[Password_Reset] ([Username], [Verification_Code])
  VALUES (@Username, @Verification_Code);
END;
GO

print '' print '*** creating sp_verify_password_reset ***'
GO
-- Initial Creator: Jared Hutton
-- Creation Date: 2024-02-24
-- Last Modified: Jared Hutton
-- Modification Description: Initial Creation
-- Stored Procedure Description: Accepts a username, email, and a password reset code to verify a password reset
CREATE PROCEDURE [dbo].[sp_verify_password_reset] (
  @Username [nvarchar](50),
  @Email [nvarchar](255),
  @Verification_Code [char](6),
  @Seconds_Before_Password_Reset_Expiry [bigint]
)
AS 
BEGIN
  IF EXISTS (
    SELECT pr.[Username]
    FROM [dbo].[Password_Reset] pr
    JOIN [dbo].[Login] l ON pr.[Username] = l.[Username]
    JOIN [dbo].[Employee] e ON l.[Employee_ID] = e.[Employee_ID]
    JOIN [dbo].[Client] c ON c.[Client_ID] = c.[Client_ID]
    WHERE
      l.[Username] = @Username AND
      (e.[Email] = @Email OR c.[Email] = @Email) AND
      pr.[Verification_Code] = @Verification_Code AND
      DATEDIFF(second, CURRENT_TIMESTAMP, pr.[Request_Datetime]) < @Seconds_Before_Password_Reset_Expiry
    )
    SELECT CAST(1 AS BIT);
  ELSE
    SELECT CAST(0 AS BIT);
END;
GO

