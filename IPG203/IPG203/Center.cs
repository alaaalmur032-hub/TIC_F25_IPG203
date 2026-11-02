using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace HomeRepairSystem
{
    public class Center
    {
        

        // هون نعرف الحدث باستخدام التفويض
        public event MyDelegate OnRepairDone;

        //   حدث تاني لتأخر التسليم
        public event MyDelegate OnLateDelivery;

        // هي القائمة يلي رح نخزن فيها كل الأجهزة
        private List<Device> devices = new List<Device>();

        // هي خاصية ثابتة لحساب العدد الإجمالي لكل الأجهزة بالمركز
        // (بتخص الفئة نفسها مش كائن معين)
        public static int TotalDevicesCreated { get; private set; } = 0;

        // هي دالة لإضافة جهاز للقائمة
        public void AddDevice(Device device)
        {
            devices.Add(device);
            TotalDevicesCreated++; // منزيد العداد الثابت
        }

        // هي دالة بترجع عدد الأجهزة
        public int CountDevices()
        {
            return devices.Count;
        }

        // هي دالة بتعرض كل الأجهزة (هون نستخدم تعدد الأشكال)
        public void ShowAll()
        {
            Console.WriteLine("========== ALL DEVICES ==========");

            if (devices.Count == 0)
            {
                Console.WriteLine("No devices yet.");
                return;
            }

            foreach (Device d in devices)
            {
                // هون بينشغل تعدد الأشكال حسب نوع الجهاز
                Console.WriteLine(d.ShowInfo());
                Console.WriteLine("  Repair Cost: " + d.CalculateRepairCost());

                // هون نتحقق إذا الجهاز متأخر
                if (Check.IsLate(d.ReceiveDate, d.IsRepaired))
                {
                    Console.WriteLine("  ** LATE DELIVERY **");

                    // هون نطلق حدث التأخر
                    if (OnLateDelivery != null)
                    {
                        OnLateDelivery("Device " + d.SerialNumber + " is late!");
                    }
                }

                Console.WriteLine("---------------------------------");
            }
        }

        // هي دالة بتحسب التكلفة الإجمالية (تعدد الأشكال كمان)
        public double GetTotalCost()
        {
            double total = 0;
            foreach (Device d in devices)
            {
                total += d.CalculateRepairCost();
            }
            return total;
        }

        //   بترجع عدد الأجهزة حسب حالة معينة
        public int CountByStatus(string status)
        {
            int count = 0;
            foreach (Device d in devices)
            {
                if (d.Status == status)
                    count++;
            }
            return count;
        }

        // هي دالة لإصلاح جهاز، وإذا نجح نطلق الحدث
        public bool CompleteRepair(string serial)
        {
            foreach (Device d in devices)
            {
                if (d.SerialNumber == serial)
                {
                    d.ChangeStatus("Repaired");

                    // هون يعني  ننفذ حدث اكتمال الإصلاح
                    if (OnRepairDone != null)
                    {
                        OnRepairDone("Device " + serial + " has been repaired.");
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
