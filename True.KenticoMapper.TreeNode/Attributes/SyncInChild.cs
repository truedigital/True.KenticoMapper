using System;
using True.KenticoMapper.TreeNode.Builders;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public class SyncInChild : Attribute, ISyncInChild
    {
        public string Name { get; }
        public Type Type { get; }

        public SyncInChild(Type type) :
            this(null, type)
        {
        }

        public SyncInChild(string name, Type type)
        {
            var interfaceType = typeof(IChildBuilder);
            if (interfaceType.IsAssignableFrom(type) == false)
            {
                throw new Exception(string.Format("{0} must implement {1}", type.Name, interfaceType.Name));
            }

            Name = name;
            Type = type;
        }
    }
}