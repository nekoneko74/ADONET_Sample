using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample09：データベーステーブルの参照処理（非接続型）（1）
    ///  ⇒ データベーステーブルから複数のレコードを参照します（非接続型）。
    ///  ⇒ SqlDataAdapterおよびDataTable／DataRowオブジェクトを使用してデータベーステーブルからレコードを参照します。
    /// </summary>
    internal class Sample09
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample09 - データベーステーブルの参照処理（非接続型）（1）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "himitu";
            string connectionString = connectionStringBuilder.ConnectionString;

            // MyTableテーブルから全レコードを取得するSQL文を作成する
            string selectQuery = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] ORDER BY [IntData] ASC";

            // SQL文の実行準備を行う
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connectionString);

            // データを受け取るDataTableクラスのオブジェクトインスタンスを準備する
            DataTable dataTable = new DataTable();

            // SQL文を実行して「データを受け取るDataTableオブジェクト」にデータを取得する
            dataAdapter.Fill(dataTable);

            // レコード件数の取得を行う
            int count = dataTable.Rows.Count;
            Console.WriteLine("取得できたレコード数：" + count);

            // 取得した全レコードの処理を行う
            //  ⇒ DataTableオブジェクトにはデータベーステーブルから選択された全レコードが格納されているので、
            //  ⇒ それを1行ずつ取り出して処理を行う
            foreach (DataRow row in dataTable.Rows)
            {
                // 整数型データを取得する
                int intData = (int)row[0];
                Console.Write("IntData=" + intData);

                // 浮動小数点型データを取得する
                double? doubleData = row["DoubleData"] as double?;
                if (doubleData is null)
                {
                    Console.Write(",DoubleData=null");
                }
                else
                {
                    Console.Write(",DoubleData=" + doubleData);
                }

                // 十進浮動小数点型データを取得する
                decimal? decimalData = row["DecimalData"] as decimal?;
                if (decimalData is null)
                {
                    Console.Write(",DecimalData=null");
                }
                else
                {
                    Console.Write(",DecimalData=" + decimalData);
                }

                // 文字列型データを取得する
                string stringData = row["StringData"] as string;
                if (stringData is null)
                {
                    Console.Write(",StringData=null");
                }
                else
                {
                    Console.Write(",StringData=" + stringData);
                }

                // 日付時刻オブジェクトデータを取得する
                DateTime? dateTimeData = row["DatetimeData"] as DateTime?;
                if (dateTimeData is null)
                {
                    Console.Write(",DatetimeData=null");
                }
                else
                {
                    Console.Write(",DatetimeData=" + dateTimeData);
                }

                // 論理型データを取得する
                bool? boolData = row["BoolData"] as bool?;
                if (boolData is null)
                {
                    Console.WriteLine(",BoolData=null");
                }
                else
                {
                    Console.WriteLine(",BoolData=" + boolData);
                }
            }

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}