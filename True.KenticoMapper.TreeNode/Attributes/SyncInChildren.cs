using System;
using True.KenticoMapper.TreeNode.Builders;

namespace True.KenticoMapper.TreeNode.Attributes
{
    public class SyncInChildren : Attribute, ISyncInChildren
    {
        public string Name { get; }
        public Type Type { get; }

        public SyncInChildren(Type type) :
            this(null, type)
        {
        }

        public SyncInChildren(string name, Type type)
        {
            var interfaceType = typeof(IChildrenBuilder);
            if (interfaceType.IsAssignableFrom(type) == false)
            {
                throw new Exception(string.Format("{0} must implement {1}", type.Name, interfaceType.Name));
            }

            Name = name;
            Type = type;
        }
    }
}