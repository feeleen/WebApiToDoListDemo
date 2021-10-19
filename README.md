[![Build status](https://ci.appveyor.com/api/projects/status/qsx5k8qr65aeectm?svg=true)](https://ci.appveyor.com/project/feeleen/webapitodolistdemo)

# WebApiToDoListDemo
Demo .NET Core Web Api project with logging and database operations using linq2db


SqlServer datatabase script:

```sql
CREATE DATABASE [ToDoList]
GO
USE [ToDoList]
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDoList](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsComplete] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateCompleted] [datetime] NULL,
 CONSTRAINT [PK_ToDoList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ToDoList] ADD  CONSTRAINT [DF_ToDoList_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO

ALTER TABLE [dbo].[ToDoList] ADD  CONSTRAINT [DF_ToDoList_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO


```
