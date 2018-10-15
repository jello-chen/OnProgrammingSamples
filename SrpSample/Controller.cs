using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpSample
{
    public class Controller
    {
        public virtual ViewResult View() =>
            new ViewResult();

        public virtual ViewResult View(object model) =>
            new ViewResult(model);
    }
}
