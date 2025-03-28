IF DB_ID('tf_message_write') IS NULL
BEGIN
    CREATE DATABASE tf_message_write;
END

GO

USE [tf_message_write];
/*
IF OBJECT_ID('Users', 'U') IS NULL
BEGIN
    CREATE TABLE [Users](
        [id]          [nvarchar](128) NOT NULL,
        [name]        [nvarchar](100) NOT NULL

        CONSTRAINT PK_Messages_id PRIMARY KEY CLUSTERED([id])
    );
END;
*/

IF OBJECT_ID('Messages', 'U') IS NULL
BEGIN
    CREATE TABLE [Messages](
        [id]            [nvarchar](128) NOT NULL,
        [content]       [text] NOT NULL,
        [author]        [nvarchar](100) NOT NULL

        CONSTRAINT PK_Messages_id PRIMARY KEY CLUSTERED([id])
    );
END;

