using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using Computer = LibreHardwareMonitor.Hardware.Computer;



namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class HardwardComponent:Entity,IAwake,IDestroy
    {
        public Computer m_Computer;
    }
}
