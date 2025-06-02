using System;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample03：データベーステーブルの参照処理（接続型）（1）
    ///  ⇒ データベーステーブルから複数のレコードを参照します（接続型）。
    ///  ⇒ SqlCommand.ExecuteReader() メソッドを使用してデータベーステーブルからレコードを参照します。
    ///  ⇒ SqlDataReaderオブジェクトを使用して選択されたレコードからフィールドのデータを取得します。
    ///  ⇒ データベーステーブルから取得した各フィールドのデータを.NET Frameworkプログラムの変数に変換して取り込みます。
    /// </summary>
    internal class Sample03
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample03 - データベースの参照処理（接続型）（1）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "P@ssword";
            string connectionString = connectionStringBuilder.ConnectionString;

            // SQL Serverに接続する
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            // MyTableテーブルから全レコードを取得するSQL文を作成する
            string selectQuery = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] ORDER BY [IntData] ASC";

            // SQL文の実行準備を行う
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = selectQuery;

            // 取得できたレコード数をカウントする
            int count = 0;

            // SQL文を実行して「結果セット読み取りオブジェクト」を取得する
            //  ⇒ SqlDataReader.read()メソッドを使用して1レコードずつ処理を行う
            //  ⇒ while文を使用して全レコードの処理が完了するまで繰り返す
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (true == reader.Read())
            {
                // 取得できたレコード数をカウントする
                count++;

                // 整数型データを取得する
                int intData = (int)reader["IntData"];
                Console.Write("IntData=" + intData);

                // 浮動小数点型データを取得する
                //  ⇒ データベース上のnullはC#のnullとは別に認識することに注意する
                if (DBNull.Value != reader["DoubleData"])
                {
                    double doubleData = (double)reader["DoubleData"];
                    Console.Write(",DoubleData=" + doubleData);
                }
                else
                {
                    Console.Write(",DoubleData=null");
                }

                // 十進浮動小数点型データを取得する
                if (DBNull.Value != reader["DecimalData"])
                {
                    decimal decimalData = (decimal)reader["DecimalData"];
                    Console.Write(",DecimalData=" + decimalData);
                }
                else
                {
                    Console.Write(",DecimalData=null");
                }

                // 文字列型データを取得する
                if (DBNull.Value != reader["StringData"])
                {
                    string stringData = (string)reader["StringData"];
                    Console.Write(",StringData=" + stringData);
                }
                else
                {
                    Console.Write(",StringData=null");
                }

                // 日付時刻オブジェクトデータを取得する
                if (DBNull.Value != reader["DatetimeData"])
                {
                    DateTime dateTimeData = (DateTime)reader["DatetimeData"];
                    Console.Write(",DatetimeData=" + dateTimeData);
                }
                else
                {
                    Console.Write(",DatetimeData=null");
                }

                // 論理型データを取得する
                if (DBNull.Value != reader["BoolData"])
                {
                    bool boolData = (bool)reader["BoolData"];
                    Console.WriteLine(",BoolData=" + boolData);
                }
                else
                {
                    Console.WriteLine(",BoolData=null");
                }
            }

            // 取得できたレコード数を表示する
            Console.WriteLine("取得できたレコード数：" + count);

            // 結果セット読み取りオブジェクトおよびSQL Serverへの接続を閉じる
            reader.Close();
            sqlConnection.Close();
        }
    }
}