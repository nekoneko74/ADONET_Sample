using System;

namespace ASPNET_Sample
{
    /// <summary>
    /// Sample_DaoDto：MyTableテーブルのレコードを格納するDTOクラス
    /// </summary>
    internal class MyTableDto
    {
        /// <summary>
        /// 整数型データ（プライマリキー）
        /// </summary>
        protected int intData;

        /// <summary>
        /// 浮動小数点型データ
        /// </summary>
        protected double? doubleData;

        /// <summary>
        /// 10進数値データ
        /// </summary>
        protected Decimal? decimalData;

        /// <summary>
        /// 文字列データ
        /// </summary>
        protected string stringData;

        /// <summary>
        /// 日付時刻型データ
        /// </summary>
        protected DateTime? dateTimeData;

        /// <summary>
        /// 真偽値型データ
        /// </summary>
        protected bool? boolData;

        /// <summary>
        /// 整数型データ（プロパティ）
        /// </summary>
        public int IntData
        {
            get
            {
                return this.intData;
            }
            set
            {
                this.intData = value;
            }
        }

        /// <summary>
        /// 浮動小数点型データ（プロパティ）
        /// </summary>
        public double? DoubleData
        {
            get
            {
                return this.doubleData;
            }
            set
            {
                this.doubleData = value;
            }
        }

        /// <summary>
        /// 10進数の数値データ（プロパティ）
        /// </summary>
        public Decimal? DecimalData
        {
            get
            {
                return this.decimalData;
            }
            set
            {
                this.decimalData = value;
            }
        }

        /// <summary>
        /// 文字列データ（プロパティ）
        /// </summary>
        public string StringData
        {
            get
            {
                return this.stringData;
            }
            set
            {
                this.stringData = value;
            }
        }

        /// <summary>
        /// 日付時刻型データ（プロパティ）
        /// </summary>
        public DateTime? DateTimeData
        {
            get
            {
                return this.dateTimeData;
            }
            set
            {
                this.dateTimeData = value;
            }
        }

        /// <summary>
        /// 真偽値型データ（プロパティ）
        /// </summary>
        public bool? BoolData
        {
            get
            {
                return this.boolData;
            }
            set
            {
                this.boolData = value;
            }
        }
    }
}