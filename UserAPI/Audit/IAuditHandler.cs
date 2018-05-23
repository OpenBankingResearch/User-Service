using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Audit
{
    public interface IAuditHandler
    {
        void Post(Audit audit);
    }
}
