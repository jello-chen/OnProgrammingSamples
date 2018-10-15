//#define oject
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 场景：之前项目的消息模块是用C#写的，现在改用成熟的第三方Python库来实现，由于时间仓促，部分功能依然沿用C#实现
             * 
             * */
#if oject
            IMessageHandleWithPython messageHandleWithPythonAdapter = new ClassAdapter();
            messageHandleWithPythonAdapter.HandleMessageWithPython();
#else
            MessageModuleWithPython messageModuleWithPython = new ObjectAdapter();
            messageModuleWithPython.HandleMessageWithPython();
#endif
            Console.ReadKey();
        }
    }
    #region 类适配器模式实现
    /// <summary>
    /// 用C#实现的消息模块
    /// </summary>
    public class MessageModuleWithCSharp
    {
        public void HandleMessage()
        {
            Console.WriteLine("Handle Message With C#");
        }
    }
    /// <summary>
    /// 用Python实现的消息模块接口
    /// </summary>
    interface IMessageHandleWithPython
    {
        void HandleMessageWithPython();
    }
    /// <summary>
    /// 适配器
    /// </summary>
    class ClassAdapter : MessageModuleWithCSharp, IMessageHandleWithPython
    {

        #region IMessageHandleWithPython 成员

        public void HandleMessageWithPython()
        {
            base.HandleMessage();//
        }

        #endregion
    }
    #endregion


    #region 对象适配器模式实现
    public class MessageModuleWithPython
    {
        public virtual void HandleMessageWithPython()
        {

        }
    }

    class ObjectAdapter : MessageModuleWithPython
    {
        private MessageModuleWithCSharp messageModuleWithCSharp = new MessageModuleWithCSharp();

        public override void HandleMessageWithPython()
        {
            messageModuleWithCSharp.HandleMessage();
        }
    }
    #endregion
}
