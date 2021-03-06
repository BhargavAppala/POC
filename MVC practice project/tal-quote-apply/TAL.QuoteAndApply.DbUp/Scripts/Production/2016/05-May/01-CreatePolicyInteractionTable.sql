﻿SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyInteraction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PolicyId] [int] NOT NULL FOREIGN KEY REFERENCES [Policy]([Id]),
	[InteractionTypeId] [int] NOT NULL FOREIGN KEY REFERENCES [InteractionType]([Id]),
	[RV] [timestamp] NOT NULL,
	[CreatedBy] NVARCHAR(256) NOT NULL,
	[CreatedTS] DATETIME NOT NULL,
	[ModifiedBy] NVARCHAR(256) NULL,
	[ModifiedTS] DATETIME NULL,

CONSTRAINT [PK_PolicyInteraction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

exec sp_addextendedproperty 'MS_Description'
    ,'Holds information about a PolicyInteraction'
    ,'user', 'dbo', 'table'
    ,'PolicyInteraction'

exec sp_addextendedproperty 'MS_Description'
    ,'Row version time stamp'
    ,'user', 'dbo'
    ,'table', 'PolicyInteraction'
    ,'column', 'RV'  

exec sp_addextendedproperty 'MS_Description'
    ,'The user the record was created by'
    ,'user', 'dbo'
    ,'table', 'PolicyInteraction'
    ,'column', 'CreatedBy'  

exec sp_addextendedproperty 'MS_Description'
    ,'The date time the record was created'
    ,'user', 'dbo'
    ,'table', 'PolicyInteraction'
    ,'column', 'CreatedTS'  

exec sp_addextendedproperty 'MS_Description'
    ,'The user that last modified the record'
    ,'user', 'dbo'
    ,'table', 'PolicyInteraction'
    ,'column', 'ModifiedBy'  

exec sp_addextendedproperty 'MS_Description'
    ,'The date time the record was last modified'
    ,'user', 'dbo'
    ,'table', 'PolicyInteraction'
    ,'column', 'ModifiedTS'
GO

