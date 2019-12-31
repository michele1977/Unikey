
CREATE TABLE [dbo].[ExTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[TestId] [int] NULL,
	[State] [tinyint] NOT NULL,
 CONSTRAINT [PK_ExTest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExTest_Question]    Script Date: 30/12/2019 18:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExTest_Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NULL,
	[ExTestId] [int] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_ExTest_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pa_ExQuestion]    Script Date: 30/12/2019 18:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pa_ExQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PossibleAnswerId] [int] NULL,
	[IsSelected] [bit] NOT NULL,
	[QuestionExId] [int] NOT NULL,
 CONSTRAINT [PK_Pa_ExQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PossibleAnswer]    Script Date: 30/12/2019 18:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PossibleAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](150) NOT NULL,
	[IsCorrect] [tinyint] NOT NULL,
	[QuestionId] [int] NOT NULL,
 CONSTRAINT [PK_PossibleAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 30/12/2019 18:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [varchar](50) NOT NULL,
	[State] [tinyint] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[TestId] [int] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 30/12/2019 18:23:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[State] [tinyint] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pa_ExQuestion] ADD  CONSTRAINT [DF_Pa_ExQuestion_IsSelected]  DEFAULT ((0)) FOR [IsSelected]
GO
ALTER TABLE [dbo].[ExTest]  WITH CHECK ADD  CONSTRAINT [FK_ExTest_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ExTest] CHECK CONSTRAINT [FK_ExTest_Test]
GO
ALTER TABLE [dbo].[ExTest_Question]  WITH CHECK ADD  CONSTRAINT [FK_ExTest_Question_ExTest] FOREIGN KEY([ExTestId])
REFERENCES [dbo].[ExTest] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExTest_Question] CHECK CONSTRAINT [FK_ExTest_Question_ExTest]
GO
ALTER TABLE [dbo].[ExTest_Question]  WITH CHECK ADD  CONSTRAINT [FK_ExTest_Question_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ExTest_Question] CHECK CONSTRAINT [FK_ExTest_Question_Question]
GO
ALTER TABLE [dbo].[Pa_ExQuestion]  WITH CHECK ADD  CONSTRAINT [FK_Pa_ExQuestion_ExTest_Question] FOREIGN KEY([QuestionExId])
REFERENCES [dbo].[ExTest_Question] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pa_ExQuestion] CHECK CONSTRAINT [FK_Pa_ExQuestion_ExTest_Question]
GO
ALTER TABLE [dbo].[Pa_ExQuestion]  WITH CHECK ADD  CONSTRAINT [FK_Pa_ExQuestion_PossibleAnswer] FOREIGN KEY([PossibleAnswerId])
REFERENCES [dbo].[PossibleAnswer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pa_ExQuestion] CHECK CONSTRAINT [FK_Pa_ExQuestion_PossibleAnswer]
GO
ALTER TABLE [dbo].[PossibleAnswer]  WITH CHECK ADD  CONSTRAINT [FK_PossibleAnswer_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PossibleAnswer] CHECK CONSTRAINT [FK_PossibleAnswer_Question]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Test]
GO
