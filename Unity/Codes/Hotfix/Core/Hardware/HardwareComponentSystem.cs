using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class HardwareComponent_Awake : AwakeSystem<HardwareComponent>
    {
        public override void Awake(HardwareComponent self)
        {
            
        }
    }
    public class HardwareComponent_Destroy : DestroySystem<HardwareComponent>
    {
        public override void Destroy(HardwareComponent self)
        {
            
        }
    }
    public static class HardwareComponentSystem
    {
    }
}
