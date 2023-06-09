using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using Npgsql;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BlaX.CryptoAutoTrading.Persistence.Data.Builders
{
    public static class ModelBuilderExtension
    {
        static readonly Regex _keysRegex = new("^(PK|FK|IX)_", RegexOptions.Compiled);

        public static void UseSnakeCaseNames(this ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();

            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {

                ConvertToSnake(mapper, table);

                foreach (var property in table.GetProperties())
                    ConvertToSnake(mapper, property);

                foreach (var primaryKey in table.GetKeys())
                    ConvertToSnake(mapper, primaryKey);

                foreach (var foreignKey in table.GetForeignKeys())
                    ConvertToSnake(mapper, foreignKey);

                foreach (var indexKey in table.GetIndexes())
                    ConvertToSnake(mapper, indexKey);
            }
        }

        internal static string ToSnakeCase(this string value)
        {
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            var list = new List<string>();

            foreach (Match match in Regex.Matches(value, "([A-Z][^A-Z]+)").Cast<Match>())
                list.Add(match.Value.ToLower(cultureInfo));

            var result = string.Join("_", list);

            return result;
        }

        static void ConvertToSnake(INpgsqlNameTranslator mapper, object entity)
        {
            switch (entity)
            {
                case IMutableEntityType table:
                    table.SetTableName(ConvertGeneralToSnake(mapper, table.GetTableName().ToSnakeCase()));
                    break;
                case IMutableProperty property:
                    property.SetColumnName(ConvertGeneralToSnake(mapper, property.GetColumnName().ToSnakeCase()));
                    break;
                case IMutableKey primaryKey:
                    primaryKey.SetName(ConvertKeyToSnake(mapper, primaryKey.GetName()));
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.SetConstraintName(ConvertKeyToSnake(mapper, foreignKey.GetConstraintName()));
                    break;
                case IMutableIndex indexKey:
                    indexKey.SetDatabaseName(ConvertKeyToSnake(mapper, indexKey.GetDatabaseName()));
                    break;
                default:
                    throw new NotImplementedException("Unexpected type was provided to snake case converter");
            }
        }

        static string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName) =>
            ConvertGeneralToSnake(mapper, _keysRegex.Replace(keyName, match => match.Value.ToLower(CultureInfo.GetCultureInfo("en-US"))));

        static string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName) =>
            mapper.TranslateMemberName(entityName);
    }
}