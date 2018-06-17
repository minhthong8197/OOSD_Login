create database OOSD_db
go
USE [OOSD_db]
GO
CREATE TABLE [dbo].[Group_Right](
	[GroupID] [nvarchar](30) NULL,
	[RightID] [nvarchar](30) NULL
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Right_tb](
	[RightID] [nvarchar](30) NOT NULL,
	[RightName] [nvarchar](30) NULL,
	[Description] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[RightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[User_Right](
	[username] [nvarchar](30) NULL,
	[RightID] [nvarchar](30) NULL
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[UserGroup](
	[GroupID] [nvarchar](30) NOT NULL,
	[GroupName] [nvarchar](30) NULL,
	[Description] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Users](
	[username] [nvarchar](30) NOT NULL,
	[password] [nvarchar](30) NULL,
	[fullname] [nvarchar](30) NULL,
	[GroupID] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G1', N'R1')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G1', N'R2')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G1', N'R3')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G1', N'R4')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G2', N'R1')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G2', N'R2')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G2', N'R4')
GO
INSERT [dbo].[Group_Right] ([GroupID], [RightID]) VALUES (N'G3', N'R1')
GO
INSERT [dbo].[Right_tb] ([RightID], [RightName], [Description]) VALUES (N'R1', N'Read', N'')
GO
INSERT [dbo].[Right_tb] ([RightID], [RightName], [Description]) VALUES (N'R2', N'Add', N'')
GO
INSERT [dbo].[Right_tb] ([RightID], [RightName], [Description]) VALUES (N'R3', N'Delete', N'')
GO
INSERT [dbo].[Right_tb] ([RightID], [RightName], [Description]) VALUES (N'R4', N'Update', N'')
GO
INSERT [dbo].[Right_tb] ([RightID], [RightName], [Description]) VALUES (N'R5', N'Login', N'')
GO
INSERT [dbo].[User_Right] ([username], [RightID]) VALUES (N'nguyenminhthong', N'R5')
GO
INSERT [dbo].[User_Right] ([username], [RightID]) VALUES (N'doquocviet', N'R5')
GO
INSERT [dbo].[User_Right] ([username], [RightID]) VALUES (N'tranhuynhphieu', N'R5')
GO
INSERT [dbo].[UserGroup] ([GroupID], [GroupName], [Description]) VALUES (N'G1', N'AdminGroup', N'')
GO
INSERT [dbo].[UserGroup] ([GroupID], [GroupName], [Description]) VALUES (N'G2', N'MemberGroup', N'')
GO
INSERT [dbo].[UserGroup] ([GroupID], [GroupName], [Description]) VALUES (N'G3', N'TrialGroup', N'')
GO
INSERT [dbo].[Users] ([username], [password], [fullname], [GroupID]) VALUES (N'doquocviet', N'123', N'dqv', N'G2')
GO
INSERT [dbo].[Users] ([username], [password], [fullname], [GroupID]) VALUES (N'nguyenminhthong', N'123', N'nmt', N'G1')
GO
INSERT [dbo].[Users] ([username], [password], [fullname], [GroupID]) VALUES (N'tranhuynhphieu', N'123', N'thp', N'G3')
GO
INSERT [dbo].[Users] ([username], [password], [fullname], [GroupID]) VALUES (N'nguyenthevinh', N'123', N'ntv', N'G3')
GO
ALTER TABLE [dbo].[Group_Right]  WITH CHECK ADD FOREIGN KEY([GroupID])
REFERENCES [dbo].[UserGroup] ([GroupID])
GO
ALTER TABLE [dbo].[Group_Right]  WITH CHECK ADD FOREIGN KEY([RightID])
REFERENCES [dbo].[Right_tb] ([RightID])
GO
ALTER TABLE [dbo].[User_Right]  WITH CHECK ADD FOREIGN KEY([RightID])
REFERENCES [dbo].[Right_tb] ([RightID])
GO
ALTER TABLE [dbo].[User_Right]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[Users] ([username])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([GroupID])
REFERENCES [dbo].[UserGroup] ([GroupID])
GO
