using System;
using True.KenticoMapper.TreeNode.Builders;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public class SyncInComplex : Attribute, ISyncInComplex
    {
        public string Name { get; }
        public Type Type { get; }

        public SyncInComplex(Type type) :
            this(null, type)
        {
        }

        public SyncInComplex(string name, Type type)
        {
            var interfaceType = typeof(IComplexBuilder);
            if (interfaceType.IsAssignableFrom(type) == false)
            {
                throw new Exception(string.Format("{0} must implement {1}", type.Name, interfaceType.Name));
            }

            Name = name;
            Type = type;
        }
    }
}