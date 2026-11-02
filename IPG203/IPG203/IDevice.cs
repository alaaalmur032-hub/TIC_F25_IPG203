using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


               // هي الواجهة يلي فيها العمليات الأساسية لكل الأجهزة
 
namespace HomeRepairSystem
{
    public interface IDevice
    {
        // هي لنعرض معلومات الجهاز
        string ShowInfo();

        // هي لحساب تكلفة الإصلاح
        double CalculateRepairCost();

        // هي لنغير حالة الجهاز
        void ChangeStatus(string newStatus);
    }
}