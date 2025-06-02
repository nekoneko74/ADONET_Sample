using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample06：データベーステーブルへのレコードの挿入処理（接続型）
    ///  ⇒ データベーステーブルにレコードを挿入します（接続型）。
    ///  ⇒ SqlCommand.ExecuteNonQuery() メソッドを使用してデータベースレコードを更新します。
    /// </summary>
    internal class Sample06
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample06 - データベーステーブルへのレコードの挿入処理（接続型）");

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

            // MyTableテーブルに1レコードを挿入するSQL文を作成する
            string insertQuery = "INSERT INTO [MyTable] ( [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData]  ) VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";

            // SQL文の実行準備を行う
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = insertQuery;

            // SQLパラメータオブジェクトを生成してパラメータに値を設定する
            //  ⇒ SqlParameterCollection.Add() メソッドのオーバーロードを使用した書き方です。
            sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = 5;
            sqlCommand.Parameters.Add("@DoubleData", SqlDbType.Float).Value = 55.5;
            sqlCommand.Parameters.Add("@DecimalData", SqlDbType.Decimal).Value = 5.5555;
            sqlCommand.Parameters.Add("@StringData", SqlDbType.NVarChar).Value = "eee";
            sqlCommand.Parameters.Add("@DatetimeData", SqlDbType.Date).Value = "2005-05-05";
            sqlCommand.Parameters.Add("@BoolData", SqlDbType.Bit).Value = DBNull.Value;

            // SQL文を実行して「処理結果（挿入された行数）」を取得する
            int inserted = sqlCommand.ExecuteNonQuery();
            if (0 < inserted)
            {
                Console.WriteLine(inserted + "レコードが挿入されました");
            }
            else
            {
                Console.WriteLine("レコードが挿入されませんでした");
            }

            // SQL Serverへの接続を閉じる
            sqlConnection.Close();
        }
    }
}