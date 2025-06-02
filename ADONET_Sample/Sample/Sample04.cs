using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample04：データベーステーブルの参照処理（接続型）（2）
    ///  ⇒ データベーステーブルから指定された条件に合致するレコードのみを参照します（接続型）。
    ///  ⇒ パラメータを使用してプログラムからSqlCommandオブジェクト内のSQL文に値を引き渡します。
    /// </summary>
    internal class Sample04
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample04 - データベースの参照処理（接続型）（2）");

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

            // MyTableテーブルから1レコードを取得するSQL文を作成する
            string selectQuery = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] WHERE [IntData] = @IntData";

            // SQL文の実行準備を行う
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = selectQuery;

            // SQLパラメータオブジェクトを生成してパラメータに値を設定する
            //  ⇒ WHERE [IntData] = @IntData の「@IntData」の部分に「2」という整数値を設定する
            SqlParameter sqlParameter = new SqlParameter(@"IntData", SqlDbType.Int);
            sqlParameter.Value = 2;
            sqlCommand.Parameters.Add(sqlParameter);

            // SQL文を実行して「結果セット読み取りオブジェクト」を取得する
            //  ⇒ 今回はPrimaryKeyを指定しての選択なので最大で1レコードしか取得されない。
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (true == reader.HasRows)
            {
                // 結果セット読み取りオブジェクトに「選択された最初のレコード」を読み込む
                if (true == reader.Read())
                {
                    // 整数型データを取得する
                    int intData = (int)reader["IntData"];
                    Console.WriteLine("IntData=" + intData);

                    // 浮動小数点型データを取得する
                    if (DBNull.Value != reader["DoubleData"])
                    {
                        double doubleData = (double)reader["DoubleData"];
                        Console.WriteLine("DoubleData=" + doubleData);
                    }
                    else
                    {
                        Console.WriteLine("DoubleData=null");
                    }

                    // 十進浮動小数点型データを取得する
                    if (DBNull.Value != reader["DecimalData"])
                    {
                        decimal decimalData = (decimal)reader["DecimalData"];
                        Console.WriteLine("DecimalData=" + decimalData);
                    }
                    else
                    {
                        Console.WriteLine("DecimalData=null");
                    }

                    // 文字列型データを取得する
                    if (DBNull.Value != reader["StringData"])
                    {
                        string stringData = (string)reader["StringData"];
                        Console.WriteLine("StringData=" + stringData);
                    }
                    else
                    {
                        Console.WriteLine("StringData=null");
                    }

                    // 日付時刻オブジェクトデータを取得する
                    if (DBNull.Value != reader["DatetimeData"])
                    {
                        DateTime dateTimeData = (DateTime)reader["DatetimeData"];
                        Console.WriteLine("DatetimeData=" + dateTimeData);
                    }
                    else
                    {
                        Console.WriteLine("DatetimeData=null");
                    }

                    // 論理型データを取得する
                    if (DBNull.Value != reader["BoolData"])
                    {
                        bool boolData = (bool)reader["BoolData"];
                        Console.WriteLine("BoolData=" + boolData);
                    }
                    else
                    {
                        Console.WriteLine("BoolData=null");
                    }
                }
            }
            // 指定された条件ではレコードが選択されなかった
            else
            {
                Console.WriteLine("レコードが選択されませんでした");
            }

            // 結果セット読み取りオブジェクトおよびSQL Serverへの接続を閉じる
            reader.Close();
            sqlConnection.Close();
        }
    }
}