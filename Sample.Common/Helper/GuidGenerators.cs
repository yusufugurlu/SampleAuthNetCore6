using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Helper
{
    public static class GuidGenerators
    {
        public static string GuidGenerator()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
