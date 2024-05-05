using System;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample02：SQL Server接続（2）
    ///  ⇒ SqlConnectionStringBuilderクラスを使用して接続文字列を生成します。
    ///  ⇒ 接続文字列をSqlConnectionクラスのコンストラクタに設定します。
    /// </summary>
    internal class Sample02
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample02 - SQL Server接続（2）");

            // SqlConnectionStringBuilderクラスを使用してSQL Serverに接続するための「接続文字列」を生成する
            //  接続先サーバー名：localhost
            //  初期接続先データベース名：ADONET
            //  ログイン名：sa
            //  パスワード：himitu
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "himitu";
            string connectionString = connectionStringBuilder.ConnectionString;
            Console.WriteLine("SQL Server接続文字列 ⇒ " + connectionString);

            // 接続文字列を使用してSQL Serverに接続する
            //  ⇒ SqlConnectionのコンストラクタに接続文字列を設定する
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("SQL Serverに接続しました");

            // *** ここにSQL Serverに対する操作を行うプログラムを記述します ***
            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すとSQL Serverとの接続を切断します");
            Console.ReadKey();

            // SQL Serverとの接続を解除する
            connection.Close();
            Console.WriteLine("SQL Serverとの接続を解除しました");

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}