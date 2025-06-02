using System;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample01：SQL Server接続（1）
    ///  ⇒ 接続文字列を使用してSQL Serverに接続します。
    /// </summary>
    internal class Sample01
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample01 - SQL Server接続（1）");

            // SQL Serverに接続するための「接続文字列」を生成する
            //  接続先サーバー名：localhost
            //  初期接続先データベース名：ADONET
            //  ログイン名：sa
            //  パスワード：P@ssword
            string connectionString = "Data Source=localhost;Initial Catalog=ADONET;User ID=sa;Password=P@ssword";
            Console.WriteLine("SQL Server接続文字列 ⇒ " + connectionString);

            // 接続文字列を使用してSQL Serverに接続する
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            Console.WriteLine("SQL Serverに接続しました");

            // *** ここにSQL Serverに対する操作を行うプログラムを記述します ***
            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すとSQL Serverとの接続を切断します");
            Console.ReadKey();

            // SQL Serverとの接続を解除する
            connection.Close();
            Console.WriteLine("SQL Serverとの接続を解除しました");
        }
    }
}