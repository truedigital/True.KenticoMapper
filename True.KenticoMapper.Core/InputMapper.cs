using System;
using System.Reflection;
using CMS.EventLog;
using True.KenticoMapper.Core.Attributes;
using True.KenticoMapper.Core.Mapping;

namespace True.KenticoMapper.Core
{
    public class InputMapper : IInputMapper
    {
        public void SetProperties<T>(IIndexer source, T target) where T : class
        {
            var properties = target
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            try
            {
                foreach (var property in properties)
                {
                    if (IgnoreProperty(property))
                        continue;

                    if (Get(source, target, property))
                        continue;
                }
            }
            catch (Exception e)
            {
                EventLogProvider.LogException("CustomTableInputMapper", "MAPPER_ERROR", e);
            }
        }

        private static bool IgnoreProperty(PropertyInfo property)
        {
            var fieldAttribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncIgnore;
            if (fieldAttribute == null)
                return false;

            return true;
        }

        private static bool Get<T>(IIndexer source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncIn;
            if (attribute == null)
                return false;

            var provider = new SyncInIndexerProvider();
            var mapper = provider.GetMapper(source, attribute);
            mapper.Set(property, target);
            return true;
        }
    }
}