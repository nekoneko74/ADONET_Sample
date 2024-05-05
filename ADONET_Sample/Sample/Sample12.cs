using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample12：データベーステーブルの更新処理（非接続型）（2）
    ///  ⇒ SqlCommandBuilderを使用して非接続型でのデータベーステーブルの更新処理を行います。
    /// </summary>
    internal class Sample12
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample12 - データベーステーブルの更新処理（非接続型）（2）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "himitu";
            string connectionString = connectionStringBuilder.ConnectionString;

            //******************************************************************
            // SqlDataAdapterオブジェクトの準備を行う
            //******************************************************************
            // MyTableテーブルから全レコードを取得するSQL文をベースにSqlDataAdapterを生成する
            string selectQuery = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] ORDER BY [IntData] ASC";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connectionString);

            // 削除／挿入／更新を行うSqlCommandを自動的に作成／設定するSqlCommandBuilderオブジェクトを生成する
            //  ⇒ レコードデータが競合した場合には「上書き」を行うように指示をします
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            builder.ConflictOption = ConflictOption.OverwriteChanges;

            //******************************************************************
            // DataTableに対してデータを読み込む
            //******************************************************************
            // データを受け取るDataTableクラスのオブジェクトインスタンスを準備する
            DataTable dataTable = new DataTable();

            // SQL文を実行して「データを受け取るDataTableオブジェクト」にデータを取得する
            dataAdapter.Fill(dataTable);

            //******************************************************************
            // DataTableに対して変更を行う
            //******************************************************************
            // 0行目の3列目のデータを書き換える
            dataTable.Rows[0][1] = 999.999;
            dataTable.Rows[0][3] = "yyy";

            // 新しいレコードを追加する
            DataRow newRow = dataTable.NewRow();
            newRow[0] = 8;
            newRow[1] = 88.8;
            newRow[2] = 88.8888;
            newRow[3] = "hhh";
            newRow[4] = new DateTime(2008, 8, 8);
            newRow[5] = true;
            dataTable.Rows.Add(newRow);

            // ex.0行目のレコードを削除する
            dataTable.Rows[0].Delete();

            //******************************************************************
            // DataTableに対して行った変更処理をデータベーステーブルに反映する
            //******************************************************************
            // 非接続型の更新処理を行う
            dataAdapter.Update(dataTable);

            // 何かキーが入力されるまで待機する
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}