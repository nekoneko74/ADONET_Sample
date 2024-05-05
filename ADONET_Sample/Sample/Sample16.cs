using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample16：トランザクション処理（2）
    ///  ⇒ TransactionScopeオブジェクトを使用してトランザクションを管理します。
    /// </summary>
    internal class Sample16
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample16 - トランザクション処理（2）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "himitu";
            string connectionString = connectionStringBuilder.ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // TransactionScopeオブジェクトによるトランザクション管理を行う
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        // SQL Serverに接続する
                        // ※TransactionScopeオブジェクトがインスタンス化された後に開始されたSQL Server接続がトランザクション処理の対象となります
                        sqlConnection.Open();

                        // MyTableテーブルに1レコードを挿入するSQL文を作成する
                        string insertQuery = "INSERT INTO [MyTable] ( [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData]  ) VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";

                        // SQL文の実行準備を行う
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = sqlConnection;
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
                        transaction.Complete();
                    }

                    // TransactionScope.Complete()が呼び出されていない場合（どこかで例外が発生した場合など）には、
                    // usingステートメントを抜けてTransactionScopeオブジェクトが破棄されるときにトランザクションのロールバックが実行されます。
                }
                catch (Exception ex)
                {
                    Console.WriteLine("例外が発生しました：" + ex.Message);
                }
            }

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}