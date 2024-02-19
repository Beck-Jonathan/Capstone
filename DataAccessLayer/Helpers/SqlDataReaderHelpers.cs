using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{   
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Provides helpful extension methods for SqlDataReader objects
    /// </summary>
    public static class SqlDataReaderHelpers
    {
        /// <summary>
        ///     Read a cell from a SQL result set as an Int32, or read null if the cell contains a null value
        /// </summary>
        /// <param name="sqlDataReader">
        ///    The data reader containing the result set
        /// </param>
        /// <param name="resultSetIndex">
        ///    The column index of the cell containing the desired data
        /// </param>
        /// <returns>
        ///    <see cref="int">int?</see>: The nullable casted value of the Int32 cell
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="SqlDataReader">SqlDataReader</see> sqlDataReader: The data reader containing the result set
        /// <br />
        ///    <see cref="int">int</see> resultSetIndex: The column index of the cell containing the desired data
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-11
        /// </remarks>
        public static int? GetInt32Nullable(this SqlDataReader sqlDataReader, int resultSetIndex)
        {
            if (sqlDataReader.IsDBNull(resultSetIndex))
            {
                return null;
            }

            return sqlDataReader.GetInt32(resultSetIndex);
        }

        /// <summary>
        ///     Read a cell from a SQL result set as a string, or read null if the cell contains a null value
        /// </summary>
        /// <param name="sqlDataReader">
        ///    The data reader containing the result set
        /// </param>
        /// <param name="resultSetIndex">
        ///    The column index of the cell containing the desired data
        /// </param>
        /// <returns>
        ///    <see cref="string">string</see>: The nullable casted value of the string cell
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="SqlDataReader">SqlDataReader</see> sqlDataReader: The data reader containing the result set
        /// <br />
        ///    <see cref="int">int</see> resultSetIndex: The column index of the cell containing the desired data
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-11
        /// </remarks>
       public static string GetStringNullable(this SqlDataReader sqlDataReader, int resultSetIndex)
        {
            if (sqlDataReader.IsDBNull(resultSetIndex))
            {
                return null;
            }

            return sqlDataReader.GetString(resultSetIndex);
        }

        /// <summary>
        ///     Read a cell from a SQL result set as a DateTime, or read null if the cell contains a null value
        /// </summary>
        /// <param name="sqlDataReader">
        ///    The data reader containing the result set
        /// </param>
        /// <param name="resultSetIndex">
        ///    The column index of the cell containing the desired data
        /// </param>
        /// <returns>
        ///    <see cref="DateTime">DateTime</see>: The nullable casted value of the DateTime cell
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="SqlDataReader">SqlDataReader</see> sqlDataReader: The data reader containing the result set
        /// <br />
        ///    <see cref="int">int</see> resultSetIndex: The column index of the cell containing the desired data
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-11
        /// </remarks>
        public static DateTime? GetDateTimeNullable(this SqlDataReader sqlDataReader, int resultSetIndex)
        {
            if (sqlDataReader.IsDBNull(resultSetIndex))
            {
                return null;
            }

            return sqlDataReader.GetDateTime(resultSetIndex);
        }
    }
}
