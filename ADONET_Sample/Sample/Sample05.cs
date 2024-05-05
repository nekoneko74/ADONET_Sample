using System;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample05：データベーステーブルの参照処理（接続型）（as演算子）
    ///  ⇒ Sample03におけるデータ型変換の別バージョンです。
    ///  ⇒ データベーステーブルから取得した各フィールドのデータをas演算子を使用して変換・取り込みます。
    /// </summary>
    internal class Sample05
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample05 - データベースの参照処理（接続型）（as演算子）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "himitu";
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
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (true == reader.Read())
            {
                // 取得できたレコード数をカウントする
                count++;

                // 整数型データを取得する
                int intData = (int)reader["IntData"];
                Console.Write("IntData=" + intData);

                // 浮動小数点型データを取得する
                //  ⇒ 受け取り側（C#側）でnullを許可している場合にはas演算子での変換も可能である
                double? doubleData = reader["DoubleData"] as double?;
                if (doubleData is null)
                {
                    Console.Write(",DoubleData=null");
                }
                else
                {
                    Console.Write(",DoubleData=" + doubleData);
                }

                // 十進浮動小数点型データを取得する
                decimal? decimalData = reader["DecimalData"] as decimal?;
                if (decimalData is null)
                {
                    Console.Write(",DecimalData=null");
                }
                else
                {
                    Console.Write(",DecimalData=" + decimalData);
                }

                // 文字列型データを取得する
                string stringData = reader["StringData"] as string;
                if (stringData is null)
                {
                    Console.Write(",StringData=null");
                }
                else
                {
                    Console.Write(",StringData=" + stringData);
                }

                // 日付時刻オブジェクトデータを取得する
                DateTime? dateTimeData = reader["DatetimeData"] as DateTime?;
                if (dateTimeData is null)
                {
                    Console.Write(",DatetimeData=null");
                }
                else
                {
                    Console.Write(",DatetimeData=" + dateTimeData);
                }

                // 論理型データを取得する
                bool? boolData = reader["BoolData"] as bool?;
                if (boolData is null)
                {
                    Console.WriteLine(",BoolData=null");
                }
                else
                {
                    Console.WriteLine(",BoolData=" + boolData);
                }
            }

            // 取得できたレコード数を表示する
            Console.WriteLine("取得できたレコード数：" + count);

            // 結果セット読み取りオブジェクトおよびSQL Serverへの接続を閉じる
            reader.Close();
            sqlConnection.Close();

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}