using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentRegistry.Infrastructure;

namespace DocumentRegistry.Helpers
{
    public interface IDatabaseHelper
    {
        int ExecuteNoResult(string sqlSyntax);

        int ExecuteNoResult(string sqlSyntax, DynamicParameters parameters);

        int ExecuteNoResult(string sqlSyntax, DynamicParameters parameters, CommandType commandType);

        int ExecuteNoResult(DatabaseContext context);

        IEnumerable<T> ExecProcedure<T>(string procedureName);

        IEnumerable<T> ExecProcedure<T>(string procedureName, DynamicParameters parameters);

        IEnumerable<T> ExecProcedure<T>(DatabaseContext context);

        IEnumerable<T> Query<T>(string sqlSyntax);

        IEnumerable<T> Query<T>(string sqlSyntax, DynamicParameters parameters);

        IEnumerable<T> Query<T>(DatabaseContext context);

        T QueryFirst<T>(string sqlSyntax);

        T QueryFirst<T>(string sqlSyntax, DynamicParameters parameters);

        T QueryFirst<T>(DatabaseContext context);

        T GetValue<T>(string sqlSyntax);

        T GetValue<T>(string sqlSyntax, DynamicParameters parameters);

        T GetValue<T>(string sqlSyntax, DynamicParameters parameters, CommandType commandType);

        T GetValue<T>(DatabaseContext context);

        int Execute(DatabaseContext context);

        /// <summary>
        /// Inserts object or collection of objects into database. Type has to be the same as target table.
        /// </summary>
        /// <typeparam name="T">Klasa która odwzorowuje tabelę w bazie danych</typeparam>
        /// <param name="objectToInsert">Obiekt lub kolekcja obiektów danej klasy</param>
        /// <returns>Id pojedynczo wstawionego rekordu lub liczba wstawionych rekordów</returns>
        long Insert<T>(T objectToInsert) where T : class;
    }
}