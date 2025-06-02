using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample15：トランザクション処理（1）
    ///  ⇒ SqlTransactionオブジェクトを使用してトランザクションを管理します。
    ///  ⇒ null条件演算子を使用すれば記述を簡略化することができます。
    /// </summary>
    internal class Sample15
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample15 - トランザクション処理（1）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "P@ssword";
            string connectionString = connectionStringBuilder.ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // トランザクションを管理するSqlTransactionオブジェクトを格納する変数を用意する
                SqlTransaction sqlTransaction = null;

                try
                {
                    // SQL Serverに接続する
                    sqlConnection.Open();

                    // トランザクション処理を開始する
                    sqlTransaction = sqlConnection.BeginTransaction();

                    // MyTableテーブルに1レコードを挿入するSQL文を作成する
                    string insertQuery = "INSERT INTO [MyTable] ( [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData]  ) VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";

                    // SQL文の実行準備を行う
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Transaction = sqlTransaction;
                    sqlCommand.CommandText = insertQuery;

                    // SQLパラメータオブジェクトを生成してパラメータに値を設定する
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

                    // トランザクションをコミットする
                    //  ⇒ ここでSqlTransaction.Commit() を呼び出さなかった場合にはどうなるか？
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // トランザクションをロールバックする
                    //  ⇒ null条件演算子を使用してsqlTransaction変数がnullかどうかをチェックしています
                    //
                    //  ↓ if文で記述したバージョン
                    //  // SqlTransactionが開始されていたらロールバックする
                    //  if (null != sqlTransaction)
                    //  {
                    //      sqlTransaction.Rollback();
                    //  }
                    sqlTransaction?.Rollback();

                    Console.WriteLine("例外が発生したためトランザクションをロールバックしました：" + ex.Message);
                }
            }
        }
    }
}