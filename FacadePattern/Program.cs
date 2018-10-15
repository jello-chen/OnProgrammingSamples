using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Patient patient = new Patient();
            patient.SeeDoctor();
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 接待员
    /// </summary>
    public class Receptionist
    {
        private RegistrationDepartment registrationDepartment;
        private OutpatientDepartment outpatientDepartment;
        private PayOffice payOffice;
        private Laboratory laboratory;
        private MedicineRoom medicineRoom;

        public Receptionist()
        {
            registrationDepartment = new RegistrationDepartment();
            outpatientDepartment = new OutpatientDepartment();
            payOffice = new PayOffice();
            laboratory = new Laboratory();
            medicineRoom = new MedicineRoom();
        }

        /// <summary>
        /// 接待
        /// </summary>
        public void Receive()
        {
            registrationDepartment.Regist();
            outpatientDepartment.SelectOutpatientSection();
            payOffice.Pay();
            laboratory.Test();
            medicineRoom.GetMedicine();
        }
    }

    /// <summary>
    /// 病人
    /// </summary>
    public class Patient
    {
        private static Receptionist Receptionist= new Receptionist();

        public void SeeDoctor()
        {
            Console.WriteLine("开始看病");
            Receptionist.Receive();
            Console.WriteLine("结束看病");
        }
    }

    /// <summary>
    /// 挂号科
    /// </summary>
    public class RegistrationDepartment
    {
        public void Regist()
        {
            Console.WriteLine("挂号");
        }
    }
    /// <summary>
    /// 门诊部
    /// </summary>
    public class OutpatientDepartment
    {
        public void SelectOutpatientSection()
        {
            Console.WriteLine("门诊");
        }
    }
    /// <summary>
    /// 缴费处
    /// </summary>
    public class PayOffice
    {
        public void Pay()
        {
            Console.WriteLine("缴费");
        }
    }
    /// <summary>
    /// 化验室
    /// </summary>
    public class Laboratory
    {
        public void Test()
        {
            Console.WriteLine("化验");
        }
    }
    /// <summary>
    /// 取药室
    /// </summary>
    public class MedicineRoom
    {
        public void GetMedicine()
        {
            Console.WriteLine("取药");
        }
    }
}
