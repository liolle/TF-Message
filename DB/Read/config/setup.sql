
IF DB_ID('tf_message_read') IS NULL
BEGIN
    CREATE DATABASE tf_message_read;
END

GO

USE [tf_message_read];

IF OBJECT_ID('Messages', 'U') IS NULL
BEGIN
    CREATE TABLE [Messages](
        [id]            [nvarchar](128) NOT NULL,
        [content]       [text] NOT NULL,
        [author]        [nvarchar](100) NOT NULL

        CONSTRAINT PK_Messages_id PRIMARY KEY CLUSTERED([id])
    );
END;

