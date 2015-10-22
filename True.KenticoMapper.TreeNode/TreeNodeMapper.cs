using System;
using System.Reflection;
using CMS.EventLog;
using True.KenticoMapper.Core.Attributes;
using True.KenticoMapper.TreeNode.Attributes;
using True.KenticoMapper.TreeNode.Builders;
using True.KenticoMapper.TreeNode.Mapping;
using True.KenticoMapper.TreeNode.Wrappers;

namespace True.KenticoMapper.TreeNode
{
    public class TreeNodeMapper : ITreeNodeMapper
    {
        public T Create<T>(IKenticoTreeNode source) where T : class, new()
        {
            var target = new T();
            Map(source, target);
            return target;
        }

        public void Map<T>(IKenticoTreeNode source, T target) where T : class
        {
            if (source == null)
                return;
            
            SetProperties(source, target);
        }

        private void SetProperties<T>(IKenticoTreeNode source, T target) where T : class
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

                    if (PopulateTargetSimple(source, target, property))
                        continue;

                    if (PopulateTargetComplex(source, target, property))
                        continue;

                    if (PopulateTargetComplexCollection(source, target, property))
                        continue;

                    if (PopulateTargetChildren(source, target, property))
                        continue;

                    if (PopulateTargetChild(source, target, property))
                        continue;
                }
            }
            catch (Exception e)
            {
                EventLogProvider.LogException("InputMapper", "MAPPER_ERROR", e);
            }
        }

        private static bool IgnoreProperty(PropertyInfo property)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncIgnore;
            if (attribute == null)
                return false;

            return true;
        }

        private static bool PopulateTargetSimple<T>(IKenticoTreeNode source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncIn;
            if (attribute == null)
                return false;

            var provider = new SyncInTreeNodeProvider();
            var mapper = provider.GetMapper(source, attribute);
            mapper.Set(property, target);
            return true;
        }

        private bool PopulateTargetComplex<T>(IKenticoTreeNode source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncInComplex;
            if (attribute == null)
                return false;

            var builder = Activator.CreateInstance(attribute.Type) as IComplexBuilder;
            if (builder == null)
                return false;

            var result = builder.Build(source, attribute, this);
            property.SetValue(target, result, null);
            return true;
        }

        private bool PopulateTargetComplexCollection<T>(IKenticoTreeNode source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncInComplexCollection;
            if (attribute == null)
                return false;

            var builder = Activator.CreateInstance(attribute.Type) as IComplexCollectionBuilder;
            if (builder == null)
                return false;

            var result = builder.Build(source, attribute, this);
            property.SetValue(target, result, null);
            return true;
        }

        private bool PopulateTargetChildren<T>(IKenticoTreeNode source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncInChildren;
            if (attribute == null)
                return false;

            var builder = Activator.CreateInstance(attribute.Type) as IChildrenBuilder;
            if (builder == null)
                return false;

            var result = builder.Build(source, attribute, this);
            property.SetValue(target, result, null);
            return true;
        }

        private bool PopulateTargetChild<T>(IKenticoTreeNode source, T target, PropertyInfo property) where T : class
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(Attribute)) as ISyncInChild;
            if (attribute == null)
                return false;

            var builder = Activator.CreateInstance(attribute.Type) as IChildBuilder;
            if (builder == null)
                return false;

            var result = builder.Build(source, attribute, this);
            property.SetValue(target, result, null);
            return true;
        }
    }
}