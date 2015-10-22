using System;
using System.Reflection;
using CMS.EventLog;
using True.KenticoMapper.Core.Attributes;

namespace True.KenticoMapper.Core
{
    public class OutputMapper : IOutputMapper
    {
        public void GetProperties<T>(T source, IIndexer target)
        {
            var properties = source
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            try
            {
                foreach (var property in properties)
                {
                    if (IgnoreProperty(property))
                        continue;

                    Set(source, target, property);
                }
            }
            catch (Exception e)
            {
                EventLogProvider.LogException("OutputMapper", "MAPPER_ERROR", e);
            }
        }

        private static bool IgnoreProperty(PropertyInfo property)
        {
            var fieldAttribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncIgnore;
            if (fieldAttribute == null)
                return false;

            return true;
        }

        private static void Set<T>(T source, IIndexer target, PropertyInfo property)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as INamedSyncOut;
            if (attribute == null)
                return;

            target[attribute.Name] = property.GetValue(source);
        }
    }
}