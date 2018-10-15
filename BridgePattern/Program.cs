using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new CommonRomoteControl();
            remoteControl.Tv = new HaierTV();
            remoteControl.OpenTV();
            remoteControl.SwitchTvChannel();
            remoteControl.CloseTV();
            remoteControl.Tv = new ChanghongTv();
            remoteControl.OpenTV();
            remoteControl.SwitchTvChannel();
            remoteControl.CloseTV();
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 电视，提供抽象化实现接口
    /// </summary>
    abstract class TV
    {
        public abstract void Open();
        public abstract void Close();
        public abstract void SwitchChannel();
    }
    /// <summary>
    /// 海尔电视，具体实现
    /// </summary>
    class HaierTV:TV
    {
        public override void Open()
        {
            Console.WriteLine("Open Haier");
        }

        public override void Close()
        {
            Console.WriteLine("Close Haier");
        }

        public override void SwitchChannel()
        {
            Console.WriteLine("Switch Haier Channel");
        }
    }
    /// <summary>
    /// 长虹电视，具体实现
    /// </summary>
    class ChanghongTv:TV
    {

        public override void Open()
        {
            Console.WriteLine("Open Changhong");
        }

        public override void Close()
        {
            Console.WriteLine("Close Changhong");
        }

        public override void SwitchChannel()
        {
            Console.WriteLine("Switch Changhong Channel");
        }
    }

    /// <summary>
    /// 遥控器，抽象角色
    /// </summary>
    class RemoteControl
    {
        public TV Tv { get; set; }

        public virtual void OpenTV()
        {
            Tv.Open();
        }

        public virtual void CloseTV()
        {
            Tv.Close();
        }

        public virtual void SwitchTvChannel()
        {
            Tv.SwitchChannel();
        }
    }
    /// <summary>
    /// 通用遥控器
    /// </summary>
    class CommonRomoteControl:RemoteControl
    {
        public override void SwitchTvChannel()
        {
            Console.WriteLine("-----------------");
            base.SwitchTvChannel();
            Console.WriteLine("-----------------");
        }
    }
}
