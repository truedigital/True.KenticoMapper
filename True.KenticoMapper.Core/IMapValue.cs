using System.Reflection;

namespace True.KenticoMapper.Core
{
    public interface IMapValue
    {
        void Set(PropertyInfo property, object target);
    }
}