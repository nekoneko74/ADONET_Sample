using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample08：データベーステーブルのレコードの削除処理（接続型）
    ///  ⇒ データベーステーブルのレコードを削除します（接続型）。
    /// </summary>
    internal class Sample08
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample08 - データベーステーブルのレコードの削除処理（接続型）");

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

            // MyTableテーブルから1レコードを削除するSQL文を作成する
            string deleteQuery = "DELETE FROM [MyTable] WHERE [IntData] = @IntData";

            // SQL文の実行準備を行う
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = deleteQuery;

            // SQLパラメータオブジェクトを生成してパラメータに値を設定する
            sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = 5;

            // SQL文を実行して「処理結果（削除された行数）」を取得する
            int deleted = sqlCommand.ExecuteNonQuery();
            if (0 < deleted)
            {
                Console.WriteLine(deleted + "レコードが削除されました");
            }
            else
            {
                Console.WriteLine("レコードが削除されませんでした");
            }

            // SQL Serverへの接続を閉じる
            sqlConnection.Close();

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}