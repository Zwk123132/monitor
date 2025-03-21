using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    public class M2C_FPSChangeHandler : AMHandler<M2C_FPSChange>
    {
        protected override void Run(Session session, M2C_FPSChange message)
        {
            session.DomainScene().GetComponent<FPSManagerComponent>().SetNewFPS( message.nNewFPS);
        }
    }
}
