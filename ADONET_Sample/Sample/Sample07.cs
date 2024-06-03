using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample07：データベーステーブルのレコードの更新処理（接続型）
    ///  ⇒ データベーステーブルのレコードを更新します（接続型）。
    /// </summary>
    internal class Sample07
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample07 - データベーステーブルのレコードの更新処理（接続型）");

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

            // MyTableテーブルの1レコードを更新するSQL文を作成する
            string updateQuery = "UPDATE [MyTable] SET [DoubleData] = @DoubleData, [DecimalData] = @DecimalData, [StringData] = @StringData, [DatetimeData] = @DatetimeData, [BoolData] = @BoolData WHERE [IntData] = @IntData";

            // SQL文の実行準備を行う
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = updateQuery;

            // SQLパラメータオブジェクトを生成してパラメータに値を設定する
            sqlCommand.Parameters.Add("@DoubleData", SqlDbType.Float).Value = 888.88;
            sqlCommand.Parameters.Add("@DecimalData", SqlDbType.Decimal).Value = 999.99;
            sqlCommand.Parameters.Add("@StringData", SqlDbType.VarChar).Value = "変更してみた！";
            sqlCommand.Parameters.Add("@DatetimeData", SqlDbType.Date).Value = new DateTime();
            sqlCommand.Parameters.Add("@BoolData", SqlDbType.Bit).Value = true;
            sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = 5;

            // SQL文を実行して「処理結果（更新された行数）」を取得する
            int updated = sqlCommand.ExecuteNonQuery();
            if (0 < updated)
            {
                Console.WriteLine(updated + "レコードが更新されました");
            }
            else
            {
                Console.WriteLine("レコードが更新されませんでした");
            }

            // SQL Serverへの接続を閉じる
            sqlConnection.Close();

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}