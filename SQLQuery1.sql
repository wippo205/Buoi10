CREATE DATABASE QuizDatabase;
USE QuizDatabase;

CREATE TABLE Questions
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    QuestionText NVARCHAR(MAX),
    AnswerA NVARCHAR(MAX),
    AnswerB NVARCHAR(MAX),
    AnswerC NVARCHAR(MAX),
    AnswerD NVARCHAR(MAX),
    CorrectAnswer NVARCHAR(1)
);

CREATE TABLE QuizResults
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    PlayerName NVARCHAR(50),
    QuestionIndex INT,
    SelectedAnswer NVARCHAR(1),
    CONSTRAINT FK_QuizResults_QuestionIndex FOREIGN KEY (QuestionIndex) REFERENCES Questions(ID)
);