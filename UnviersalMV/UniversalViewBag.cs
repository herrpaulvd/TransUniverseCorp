using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnviersalMV
{
    public class UniversalViewBag(CommonModel model, string action, string? error = null)
    {
        public CommonModel Model { get; } = model;
        public string Action { get; } = action;
        public string? Error { get; } = error;
        public bool PrintDefault { get; set; } = false;
    }
}
