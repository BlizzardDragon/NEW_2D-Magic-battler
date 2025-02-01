using System;
using JetBrains.Annotations;

namespace VampireSquid.Common.DependencyContainer
{
    [MeansImplicitUse, AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        /// AutoFind is a flag that indicates whether the class should be automatically added in SceneCompositeRoot for injection to happen. default is true.
        /// </summary>
        public bool AutoFind { get; set; } = true;
    }
}
