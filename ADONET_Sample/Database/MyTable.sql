/** 使用するデータベース「ADONET」を先に作成しておいてください */

/** 使用するデータベースを選択する */
USE ADONET;

/** MyTableテーブルが既に存在する場合には削除する */
DROP TABLE IF EXISTS MyTable;

/** MyTableテーブルを作成する */
CREATE TABLE MyTable
(
	IntData int,
	DoubleData float,
	DecimalData decimal(10, 4),
	StringData nvarchar(50),
	DatetimeData date,
	BoolData bit,
	PRIMARY KEY ( IntData )
);

/** MyTableテーブルにサンプルデータを挿入する */
INSERT INTO MyTable ( IntData, DoubleData, DecimalData, StringData, DatetimeData, BoolData )
VALUES ( 1, 11.1, 11.1111, 'aaa', '2001-01-01', 'true' );
INSERT INTO MyTable ( IntData, DoubleData, DecimalData, StringData, DatetimeData, BoolData )
VALUES ( 2, 22.2, 22.2222, 'bbb', '2002-02-02', 'false' );
INSERT INTO MyTable ( IntData, DoubleData, DecimalData, StringData, DatetimeData, BoolData )
VALUES ( 3, 33.3, 33.3333, 'ccc', '2003-03-03', 'true' );
INSERT INTO MyTable ( IntData, DoubleData, DecimalData, StringData, DatetimeData, BoolData )
VALUES ( 4, null, null, null, null, null );