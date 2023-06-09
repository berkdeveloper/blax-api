using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace BlaX.CryptoAutoTrading.Application.Utilities.Helpers
{
    public static class ObjectPropertiesHelper
    {
        public static bool CheckIsNullObjectProperties<T>(T entity) =>
            entity.GetType().GetProperties()
                .Where(p => p.GetValue(entity, null) as dynamic is null).ToList().Count > 0;


        /// <summary>
        /// Herhangi bir değer mevcut ise true döner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static bool AnyValue<T>(T entity) where T : class, new()
        {
            try
            {
                entity ??= new T();
                var properties = entity.GetType().GetProperties().ToList();
                string propertyName = string.Empty;
                dynamic value = null;
                bool check = default;

                foreach (var property in properties)
                {
                    propertyName = property.Name;
                    value = property.GetValue(entity, null);
                    if (value is not null || value != (object)string.Empty) return true;
                }
                return check;
            }
            catch { return default; }
        }

        public static bool IsArgumentsNullCheck<T>(T entity, params Expression<Func<T, dynamic>>[] expressions)
        {
            try
            {
                string propertyName = string.Empty;
                foreach (var expression in expressions)
                {
                    if (expression.Body is MemberExpression)
                    {
                        propertyName = ((MemberExpression)expression.Body).Member.Name;
                        var result = entity.GetType().GetProperty(propertyName).GetValue(entity, null);
                        if (result == null) return true;
                    }
                    else
                    {
                        var op = ((UnaryExpression)expression.Body).Operand;
                        propertyName = ((MemberExpression)op).Member.Name;
                        var result = entity.GetType().GetProperty(propertyName).GetValue(entity, null);
                        if (result == null) return true;
                    }
                }
                return default;
            }
            catch { return true; }
        }

        public static bool IsArgumentsNullOrEmptyCheck<T>(T entity, params Expression<Func<T, dynamic>>[] expressions) where T : new()
        {
            try
            {
                string propertyName = string.Empty;
                object result = null;
                foreach (var expression in expressions)
                {
                    if (expression.Body is MemberExpression)
                        propertyName = ((MemberExpression)expression.Body).Member.Name;
                    else
                    {
                        var op = ((UnaryExpression)expression.Body).Operand;
                        propertyName = ((MemberExpression)op).Member.Name;
                    }
                    result = entity.GetType().GetProperty(propertyName).GetValue(entity, null);
                    if (result == null || result == (object)string.Empty) return true;
                }
                return default;
            }
            catch { return true; }
        }

        private static bool CheckIsNullObjectProperties<T>(T entity, List<string> propertyNameList)
        {
            try
            {
                List<PropertyInfo> propertyInfo = new();
                foreach (var propertyName in propertyNameList)
                    if (entity.GetType().GetProperties().Where(p => p.Name == propertyName).Where(p => p.GetValue(entity, null) as dynamic is null).FirstOrDefault() != null)
                        propertyInfo.Add(entity.GetType().GetProperties().Where(p => p.Name == propertyName).Where(p => p.GetValue(entity, null) as dynamic is null).FirstOrDefault());
                return propertyInfo.Count > 0;
            }
            catch { return default; }
        }

        private static List<string> GetDisplayNameList<T>()
        {
            var info = TypeDescriptor.GetProperties(typeof(T))
                .Cast<PropertyDescriptor>()
                .Where(p => p.Attributes.Cast<Attribute>().Any(a => a.GetType() == typeof(RequiredAttribute)))
                .Select(p => p.Name).ToList();
            return info;
        }

        public static bool CheckIsNullObjectPropertiesWithRequiredAttribute<T>(T entity)
        {
            var requiredList = GetDisplayNameList<T>();

            return CheckIsNullObjectProperties(entity, requiredList);
        }

        public static bool CheckIsNullObjectPropertiesWithRequiredAttribute<T>(T entity, params Expression<Func<T, dynamic>>[] expressions)
        {
            var requiredList = GetDisplayNameList<T>();

            var manualParams = GetAllPropertyInfo(expressions);

            requiredList.AddRange(manualParams);

            return CheckIsNullObjectProperties(entity, requiredList);
        }

        private static List<string> GetAllPropertyInfo<T>(params Expression<Func<T, object>>[] expressions)
        {
            List<string> result = new();
            foreach (var expression in expressions)
            {
                if (expression.Body is MemberExpression)
                    result.Add(((MemberExpression)expression.Body).Member.Name);
                else
                {
                    var op = ((UnaryExpression)expression.Body).Operand;
                    result.Add(((MemberExpression)op).Member.Name);
                }
            }
            return result;
        }
    }
}
