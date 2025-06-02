using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample_DaoDto：DAO／DTOパターン
    /// </summary>
    internal class Sample_DaoDto
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample_DaoDto - DAO／DTOパターン");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "P@ssword";
            string connectionString = connectionStringBuilder.ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // SQL Serverに接続する
                    sqlConnection.Open();

                    // MyTableテーブルのDAOインスタンスを準備する
                    MyTableDao myTableDao = new MyTableDao();
                    myTableDao.Connection = sqlConnection;

                    //「IntData = 2」を条件としてMyTableテーブルから1レコードを取得する
                    Console.WriteLine("MyTableテーブルから1レコードを取得する");
                    MyTableDto myTableRecord = myTableDao.Select(2);
                    if (myTableRecord is MyTableDto)
                    {
                        DisplayMyTableRecord(myTableRecord);
                    }

                    // MyTableテーブルの全レコードを取得する
                    Console.WriteLine("MyTableテーブルから全レコードを取得する");
                    List<MyTableDto> myTableRecords = myTableDao.Select();
                    foreach (MyTableDto myTableRecord2 in myTableRecords)
                    {
                        DisplayMyTableRecord(myTableRecord2);
                    }

                    // MyTableテーブルに1レコードを挿入する
                    Console.WriteLine("MyTableテーブルに1レコードを挿入する");
                    MyTableDto myTableInsertRecord = new MyTableDto();
                    myTableInsertRecord.IntData = 9;
                    myTableInsertRecord.DoubleData = null;
                    myTableInsertRecord.DecimalData = new decimal(9.99);
                    myTableInsertRecord.StringData = "九つ目";
                    myTableInsertRecord.BoolData = false;
                    if (0 < myTableDao.Insert(myTableInsertRecord))
                    {
                        Console.WriteLine("MyTableテーブルに1レコードを挿入しました");
                    }
                    else
                    {
                        Console.WriteLine("MyTableテーブルにレコードを挿入することができませんでした");
                    }

                    // MyTableテーブルの1レコードを更新する
                    //  ⇒ 先程取得したIntData=2のレコードを書き換えてみる
                    Console.WriteLine("MyTableテーブルの1レコードを更新する");
                    myTableRecord.DoubleData = 999.9999;
                    myTableRecord.DecimalData = new decimal(2000.1234);
                    myTableRecord.StringData = "書き換え後";
                    myTableRecord.BoolData = null;
                    if (0 < myTableDao.Update(myTableRecord, 2))
                    {
                        Console.WriteLine("MyTableテーブルのレコードを更新しました");
                    }
                    else
                    {
                        Console.WriteLine("MyTableテーブルのレコードを更新することができませんでした");
                    }

                    // MyTableテーブルから1レコードを削除する
                    Console.WriteLine("MyTableテーブルの1レコードを削除する");
                    if (0 < myTableDao.Delete(4))
                    {
                        Console.WriteLine("MyTableテーブルのレコードを削除しました");
                    }
                    else
                    {
                        Console.WriteLine("MyTableテーブルのレコードを削除することができませんでした");
                    }

                    // MyTableテーブルの全レコードを取得する
                    Console.WriteLine("MyTableテーブルから全レコードを取得する（2回目）");
                    foreach (MyTableDto myTableRecord2 in myTableDao.Select())
                    {
                        DisplayMyTableRecord(myTableRecord2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("例外が発生しました：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// MyTableの1レコードのデータを保持しているDTOの内容を画面に表示する
        /// </summary>
        /// <param name="myTableRecord">MyTableの1レコードのデータを保持しているDTO</param>
        public static void DisplayMyTableRecord(MyTableDto myTableRecord)
        {
            Console.Write("IntData=" + myTableRecord.IntData);
            Console.Write(myTableRecord.DoubleData is double ? ",DoubleData=" + myTableRecord.DoubleData : ",DoubleData=null");
            Console.Write(myTableRecord.DecimalData is Decimal ? ",DecimalData=" + myTableRecord.DecimalData : ",DecimalData=null");
            Console.Write(myTableRecord.StringData is string ? ",StringData=" + myTableRecord.StringData : ",StringData=null");
            Console.Write(myTableRecord.DateTimeData is DateTime ? ",DatetimeData=" + myTableRecord.DateTimeData : ",DatetimeData=null");
            Console.WriteLine(myTableRecord.BoolData is bool ? ",BoolData=" + myTableRecord.BoolData : ",BoolData=null");
        }
    }
}