using System.Collections.Generic;
using VampireSquid.Common.Utils.Helpers;

namespace VampireSquid.Common.UI.Window.Common
{
    public class CommonWindowManager : WindowManager<CommonWindowType>
    {
        public CommonWindowManager(IEnumerable<SerializableMap<CommonWindowType, WindowBase>> windows) : base(windows) { }
    }
}