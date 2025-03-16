using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;


namespace ET
{
    [FriendClass(typeof(HardwardComponent))]
    public class HardwardComponent_Awake : AwakeSystem<HardwardComponent>
    {
        public override void Awake(HardwardComponent self)
        {
            Console.WriteLine("硬件初始化完成");
            self.m_Computer = self.NormalSetting();
            self.m_Computer.Open();
            MessageHelper.SendActor()

        }
    }
    [FriendClass(typeof(HardwardComponent))]
    public class HardwardComponent_Destroy : DestroySystem<HardwardComponent>
    {
        public override void Destroy(HardwardComponent self)
        {
            self.m_Computer?.Close();
            self.m_Computer = null;
        }
    }

    [FriendClass(typeof(HardwardComponent))]
    public static class HardwardComponentSystem
    {
        public static Computer NormalSetting(this HardwardComponent self)
        {
            return new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = false,
                IsControllerEnabled = false,
                IsNetworkEnabled = false,
                IsStorageEnabled = false,
                IsBatteryEnabled =false
            };
        }
        public static Computer AllSetting()
        {
            return new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true,
                IsBatteryEnabled = true
            };
        }

        public static void cd(this HardwardComponent self)
        {

        
        }
    }
}
