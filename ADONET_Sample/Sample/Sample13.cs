using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample13：例外処理
    ///  ⇒ 例外処理を使用してデータベースで発生したエラーを検知して対応を行います。
    /// </summary>
    internal class Sample13
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample13 - 例外処理");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "P@ssword";
            string connectionString = connectionStringBuilder.ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                // SQL Serverに接続する
                sqlConnection.Open();

                // MyTableテーブルに1レコードを挿入するSQL文を作成する
                string insertQuery = "INSERT INTO [MyTable] ( [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData]  ) VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";

                // SQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = insertQuery;

                // SQLパラメータオブジェクトを生成してパラメータに値を設定する
                //  ⇒ SQL文にはエラーはありませんが、このプログラムを2回以上動かすと「プライマリキー重複」のエラーがデータベース側で発生します。
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
            }
            catch (Exception ex)
            {
                // 発生した例外の内容（メッセージ）を表示する
                Console.WriteLine("例外が発生しました：" + ex.Message);
            }
            finally
            {
                // SQL Serverへの接続を閉じる
                sqlConnection.Close();
            }
        }
    }
}