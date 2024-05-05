using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample_DaoDto：MyTableテーブルのDAOクラス
    /// </summary>
    internal class MyTableDao
    {
        /// <summary>
        /// データベース接続オブジェクト（フィールド）
        /// </summary>
        protected SqlConnection connection = null;

        /// <summary>
        /// データベース接続オブジェクト（プロパティ）
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                return this.connection;
            }
            set
            {
                this.connection = value;
            }
        }

        /// <summary>
        /// プライマリキーを指定してMyTableテーブルのレコードを削除する
        /// </summary>
        /// <param name="intData">削除したいレコードのIntDataの値（プライマリキー）</param>
        /// <returns>削除されたレコードの数</returns>
        /// <exception cref="Exception">レコードの削除処理中に例外が発生した</exception>
        public int Delete(int intData)
        {
            try
            {
                // SQL Serverへの接続が有効になっていることを確認する
                if (!(this.connection is SqlConnection))
                {
                    throw new Exception("SQL Server接続が無効です");
                }

                // MyTableテーブルから1レコードを削除するSQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.connection;
                sqlCommand.CommandText = "DELETE FROM [MyTable] WHERE [IntData] = @IntData";

                // SQLパラメータオブジェクトを生成してパラメータに値を設定する
                sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = intData;

                // SQL文を実行して「処理結果（削除された行数）」を返す
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("レコードの削除を行なうことができませんでした", ex);
            }
        }

        /// <summary>
        /// MyTableテーブルにレコードを挿入する
        /// </summary>
        /// <param name="myTableRecord">挿入されるレコードのデータを保持したDTO</param>
        /// <returns>挿入されたレコードの数</returns>
        /// <exception cref="Exception">レコードの挿入処理中に例外が発生した</exception>
        public int Insert(MyTableDto myTableRecord)
        {
            try
            {
                // SQL Serverへの接続が有効になっていることを確認する
                if (!(this.connection is SqlConnection))
                {
                    throw new Exception("SQL Server接続が無効です");
                }

                // MyTableテーブルに1レコードを挿入するSQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.connection;
                sqlCommand.CommandText = "INSERT INTO [MyTable] ( [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData]  ) VALUES ( @IntData, @DoubleData, @DecimalData, @StringData, @DatetimeData, @BoolData )";

                // SQLパラメータオブジェクトを生成してパラメータに値を設定する
                sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = myTableRecord.IntData;
                sqlCommand.Parameters.Add("@DoubleData", SqlDbType.Float).Value = (myTableRecord.DoubleData is double) ? myTableRecord.DoubleData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@DecimalData", SqlDbType.Decimal).Value = (myTableRecord.DecimalData is decimal) ? myTableRecord.DecimalData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@StringData", SqlDbType.NVarChar).Value = (myTableRecord.StringData is string) ? myTableRecord.StringData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@DatetimeData", SqlDbType.Date).Value = (myTableRecord.DateTimeData is DateTime) ? myTableRecord.DateTimeData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@BoolData", SqlDbType.Bit).Value = (myTableRecord.BoolData is bool) ? myTableRecord.BoolData : (object)DBNull.Value;

                // SQL文を実行して「処理結果（挿入された行数）」を返す
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("レコードの挿入を行なうことができませんでした", ex);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MyTableDao()
        {
            // メンバ変数の初期化を行う
            this.connection = null;
        }

        /// <summary>
        /// コンストラクタ（初期化あり）
        /// </summary>
        /// <param name="sqlConnection">SQL Server接続オブジェクト</param>
        public MyTableDao(SqlConnection sqlConnection)
        {
            // メンバ変数の初期化を行う
            this.connection = sqlConnection;
        }

        /// <summary>
        /// MyTableテーブルから指定されたプライマリキー（IntData）の値を持つレコードを取得する
        /// </summary>
        /// <param name="intData">プライマリキー（IntData）の値</param>
        /// <returns>取得されたレコードの値を保持しているDTOインスタンスまたはnull</returns>
        /// <exception cref="Exception">レコードの選択処理中に例外が発生した</exception>
        public MyTableDto Select(int intData)
        {
            try
            {
                MyTableDto result = null;

                // SQL Serverへの接続が有効になっていることを確認する
                if (!(this.connection is SqlConnection))
                {
                    throw new Exception("SQL Server接続が無効です");
                }

                // MyTableテーブルから1レコードを取得するSQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.connection;
                sqlCommand.CommandText = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] WHERE [IntData] = @IntData";

                // SQLパラメータオブジェクトを生成してパラメータに値を設定する
                sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = intData;

                // SQL文を実行して「結果セット読み取りオブジェクト」を取得する
                //  ⇒ 結果セット読み取りオブジェクトの後始末（SqlDataReader.Close()）をusingステートメントに任せています
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (true == reader.Read())
                    {
                        result = new MyTableDto();
                        result.IntData = (int)reader["IntData"];
                        result.DoubleData = reader["DoubleData"] as double?;
                        result.DecimalData = reader["DecimalData"] as decimal?;
                        result.StringData = reader["StringData"] as string;
                        result.DateTimeData = reader["DatetimeData"] as DateTime?;
                        result.BoolData = reader["BoolData"] as bool?;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("レコードの選択を行なうことができませんでした", ex);
            }
        }

        /// <summary>
        /// MyTableテーブルから全てのレコードを取得する
        /// </summary>
        /// <returns>取得されたレコードの値を保持しているDTOインスタンスのリスト</returns>
        /// <exception cref="Exception">レコードの選択処理中に例外が発生した</exception>
        public List<MyTableDto> Select()
        {
            try
            {
                List<MyTableDto> resultList = new List<MyTableDto>();

                // SQL Serverへの接続が有効になっていることを確認する
                if (!(this.connection is SqlConnection))
                {
                    throw new Exception("SQL Server接続が無効です");
                }

                // MyTableテーブルから全レコードを取得するSQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.connection;
                sqlCommand.CommandText = "SELECT [IntData], [DoubleData], [DecimalData], [StringData], [DatetimeData], [BoolData] FROM [MyTable] ORDER BY [IntData] ASC";

                // SQL文を実行して「結果セット読み取りオブジェクト」を取得する
                //  ⇒ 結果セット読み取りオブジェクトの後始末（SqlDataReader.Close()）をusingステートメントに任せています
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (true == reader.Read())
                    {
                        MyTableDto result = new MyTableDto();
                        result.IntData = (int)reader["IntData"];
                        result.DoubleData = reader["DoubleData"] as double?;
                        result.DecimalData = reader["DecimalData"] as decimal?;
                        result.StringData = reader["StringData"] as string;
                        result.DateTimeData = reader["DatetimeData"] as DateTime?;
                        result.BoolData = reader["BoolData"] as bool?;
                        resultList.Add(result);
                    }
                }

                return resultList;
            }
            catch (Exception ex)
            {
                throw new Exception("レコードの選択を行なうことができませんでした", ex);
            }
        }

        /// <summary>
        /// プライマリキーを指定してMyTableテーブルのレコードを更新する
        /// </summary>
        /// <param name="myTableRecord">更新されるレコードのデータを保持しているDTO</param>
        /// <param name="intData">更新したいレコードのIntDataの値（プライマリキー）</param>
        /// <returns>更新されたレコードの数</returns>
        /// <exception cref="Exception">レコードの更新処理中に例外が発生した</exception>
        public int Update(MyTableDto myTableRecord, int intData)
        {
            try
            {
                // SQL Serverへの接続が有効になっていることを確認する
                if (!(this.connection is SqlConnection))
                {
                    throw new Exception("SQL Server接続が無効です");
                }

                // MyTableテーブルの1レコードを更新するSQL文の実行準備を行う
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.connection;
                sqlCommand.CommandText = "UPDATE [MyTable] SET [DoubleData] = @DoubleData, [DecimalData] = @DecimalData, [StringData] = @StringData, [DatetimeData] = @DatetimeData, [BoolData] = @BoolData WHERE [IntData] = @IntData";

                // SQLパラメータオブジェクトを生成してパラメータに値を設定する
                sqlCommand.Parameters.Add("@DoubleData", SqlDbType.Float).Value = (myTableRecord.DoubleData is double) ? myTableRecord.DoubleData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@DecimalData", SqlDbType.Decimal).Value = (myTableRecord.DecimalData is decimal) ? myTableRecord.DecimalData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@StringData", SqlDbType.NVarChar).Value = (myTableRecord.StringData is string) ? myTableRecord.StringData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@DatetimeData", SqlDbType.Date).Value = (myTableRecord.DateTimeData is DateTime) ? myTableRecord.DateTimeData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@BoolData", SqlDbType.Bit).Value = (myTableRecord.BoolData is bool) ? myTableRecord.BoolData : (object)DBNull.Value;
                sqlCommand.Parameters.Add("@IntData", SqlDbType.Int).Value = intData;

                // SQL文を実行して「処理結果（更新された行数）」を返す
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("レコードの更新を行なうことができませんでした", ex);
            }
        }
    }
}