using AutoMapper;
using System.Reflection;

namespace Infrastructure.Mappings
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExsiting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}