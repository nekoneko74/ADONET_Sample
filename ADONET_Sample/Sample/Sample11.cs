using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample11：データベーステーブルの更新処理（非接続型）（1）
    ///  ⇒ 非接続型でデータベーステーブルにレコードを挿入します。
    ///  ⇒ 非接続型でデータベーステーブルのレコードを更新します。
    ///  ⇒ 非接続型でデータベーステーブルのレコードを削除します。
    /// </summary>
    internal class Sample11
    {
        /// <summary>
        /// メインメソッド（プログラムのエントリポイント）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample11 - データベーステーブルの更新処理（非接続型）（1）");

            // SQL Serverへの接続情報を生成する
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "localhost";
            connectionStringBuilder.InitialCatalog = "ADONET";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "P@ssword";
            string connectionString = connectionStringBuilder.ConnectionString;

            //******************************************************************
            // SqlDataAdapterオブジェクトの準備を行う
            //******************************************************************
            // MyTableテーブルから全レコードを取得するSQL文をベースにSqlDataAdapterを生成する
            string selectQuery = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] ORDER BY [IntData] ASC";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connectionString);

            // DataTableオブジェクト内の更新されたレコード用のSqlCommandクラスをSqlDataAdapterに設定する
            //  ⇒ ここでは「StringData」カラムのみを変更するUPDATEクエリになっていることに注意します。
            //  ⇒ DataTableオブジェクト内の「StringData」以外のフィールドの値を書き換えても、データベースには反映されません。
            SqlCommand updateCommand = new SqlCommand();
            updateCommand.Connection = new SqlConnection(connectionString);
            updateCommand.CommandText = "UPDATE [MyTable] SET [StringData] = @StringData WHERE [IntData] = @IntData";
            updateCommand.Parameters.Add("@IntData", SqlDbType.Int, 4, "IntData");
            updateCommand.Parameters.Add("@StringData", SqlDbType.NVarChar, 50, "StringData");
            dataAdapter.UpdateCommand = updateCommand;

            // DataTableオブジェクト内の削除されたレコード用のSqlCommandクラスをSqlDataAdapterに設定する
            SqlCommand deleteCommand = new SqlCommand();
            deleteCommand.Connection = new SqlConnection(connectionString);
            deleteCommand.CommandText = "DELETE FROM [MyTable] WHERE [IntData] = @IntData";
            deleteCommand.Parameters.Add("@IntData", SqlDbType.Int, 4, "IntData");
            dataAdapter.DeleteCommand = deleteCommand;

            // DataTableオブジェクトに新たに挿入されたレコード用のSqlCommandクラスをSqlDataAdapterに設定する
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = new SqlConnection(connectionString);
            insertCommand.CommandText = "INSERT INTO [MyTable] VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";
            insertCommand.Parameters.Add("@IntData", SqlDbType.Int, 4, "IntData");
            insertCommand.Parameters.Add("@DoubleData", SqlDbType.Float, 8, "DoubleData");
            insertCommand.Parameters.Add("@DecimalData", SqlDbType.Decimal, 17, "DecimalData");
            insertCommand.Parameters.Add("@StringData", SqlDbType.NVarChar, 50, "StringData");
            insertCommand.Parameters.Add("@DatetimeData", SqlDbType.Date, 3, "DatetimeData");
            insertCommand.Parameters.Add("@BoolData", SqlDbType.Bit, 1, "BoolData");
            dataAdapter.InsertCommand = insertCommand;

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
            // ※1列目（DoubleData）の値を書き換えてもUPDATE文の対象にはならないことに注意します
            // dataTable.Rows[0][1] = 999.999;
            dataTable.Rows[0][3] = "xxx";

            // 新しいレコードを追加する
            DataRow newRow = dataTable.NewRow();
            newRow[0] = 7;
            newRow[1] = 77.7;
            newRow[2] = 77.7777;
            newRow[3] = "ggg";
            newRow[4] = new DateTime(2007, 7, 7);
            newRow[5] = true;
            dataTable.Rows.Add(newRow);

            // ex.0行目のレコードを削除する
            //dataTable.Rows[0].Delete();

            //******************************************************************
            // DataTableに対して行った変更処理をデータベーステーブルに反映する
            //******************************************************************
            // 非接続型の更新処理を行う
            dataAdapter.Update(dataTable);
        }
    }
}