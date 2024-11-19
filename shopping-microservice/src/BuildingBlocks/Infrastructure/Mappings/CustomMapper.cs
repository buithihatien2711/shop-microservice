using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;

namespace Infrastructure.Mappings
{
    public class CustomMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentException("Source cannot be null");
            }

            if (source is IEnumerable sourceCollection)
            {
                Type sourceItemType = typeof(TSource).GetGenericArguments().Single();
                Type destinationItemType = typeof(TDestination).GetGenericArguments().Single();

                var destinationList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(destinationItemType));

                foreach (var sourceItem in sourceCollection)
                {
                    var destinationItem = Activator.CreateInstance(destinationItemType);
                    destinationItem = MapSingleUsingRuntimeType(sourceItem, destinationItem, sourceItemType, destinationItemType);
                    destinationList.Add(destinationItem);
                }

                return (TDestination)destinationList;
            }
            else
            {
                var sourceType = typeof(TSource);
                var destinationType = typeof(TDestination);
                TDestination destinationObject = Activator.CreateInstance<TDestination>();
                destinationObject = (TDestination)MapSingleUsingRuntimeType(source, destinationObject, sourceType, destinationType);

                return destinationObject;
            }
        }

        private TDestination MapSingle<TSource, TDestination>(TSource source)
        {
            // Get type of sourve and destination
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var flags = BindingFlags.Public | BindingFlags.Instance;
            TDestination destinationObject = Activator.CreateInstance<TDestination>();

            if (source != null)
            {
                var destinationProperties = destinationType.GetProperties();
                foreach (var property in destinationProperties)
                {
                    var sourceProperty = sourceType.GetProperty(property.Name, flags);
                    if (sourceProperty != null)
                    {
                        var value = sourceProperty.GetValue(source);
                        property.SetValue(destinationObject, value);
                    }
                }
            }
            return destinationObject;
        }

        private object MapSingleUsingRuntimeType(object source, object destinationObject, Type sourceType, Type destinationType)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;

            if (source != null)
            {
                var destinationProperties = destinationType.GetProperties();
                foreach (var property in destinationProperties)
                {
                    var sourceProperty = sourceType.GetProperty(property.Name, flags);
                    if (sourceProperty != null)
                    {
                        var value = sourceProperty.GetValue(source);
                        property.SetValue(destinationObject, value);
                    }
                }
            }
            return destinationObject;
        }
    }
}
