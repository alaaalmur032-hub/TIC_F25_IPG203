using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace HomeRepairSystem
{
    class Program
    {
        // هي خاصية ثابتة لعدّ الأجهزة يلي تم إصلاحها بهالجلسة
        public static int RepairedCount { get; private set; } = 0;

        static void Main(string[] args)
        {
            // منعمل مركز الصيانة
            Center center = new Center();

            // هون نربط الأحداث بدوال بتطبع رسائل
            center.OnRepairDone += ShowRepairMessage;
            center.OnLateDelivery += ShowLateMessage;

            // هون نعبّي بيانات افتراضية أول ما يشتغل البرنامج
            LoadDefaultData(center);

            // هي حلقة القائمة
            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                Console.WriteLine();

                if (choice == "1")
                {
                    center.ShowAll();
                }
                else if (choice == "2")
                {
                    AddNewDevice(center);
                }
                else if (choice == "3")
                {
                    RepairDevice(center);
                }
                else if (choice == "4")
                {
                    ShowTotal(center);
                }
                else if (choice == "5")
                {
                    ShowStats(center);
                }
                else if (choice == "0")
                {
                    running = false;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Wrong choice, try again.");
                }

                Console.WriteLine();
            }
        }

         //    بتطبع القائمة
        static void ShowMenu()
        {
            Console.WriteLine("----- Repair Center -----");
            Console.WriteLine("1- Show all devices");
            Console.WriteLine("2- Add new device");
            Console.WriteLine("3- Repair device");
            Console.WriteLine("4- Total cost");
            Console.WriteLine("5- Statistics");
            Console.WriteLine("0- Exit");
            Console.WriteLine("-------------------------");
            Console.Write("Enter number: ");
        }

        // هي دالة بتعبّي بيانات افتراضية
        static void LoadDefaultData(Center center)
        {
            // منتحقق من الأرقام التسلسلية قبل ما نضيف
            if (Check.CheckSerial("REF10001"))
                center.AddDevice(new Fridge("REF10001", "Samsung", 500, DateTime.Now.AddDays(-10), 4));

            if (Check.CheckSerial("REF10002"))
                center.AddDevice(new Fridge("REF10002", "LG", 450, DateTime.Now.AddDays(-40), 3));

            if (Check.CheckSerial("WAS20001"))
                center.AddDevice(new Washer("WAS20001", "Bosch", 400, DateTime.Now.AddDays(-5), 8));

            if (Check.CheckSerial("WAS20002"))
                center.AddDevice(new Washer("WAS20002", "Ariston", 380, DateTime.Now.AddDays(-45), 7));

            if (Check.CheckSerial("AIR30001"))
                center.AddDevice(new AC("AIR30001", "Carrier", 600, DateTime.Now.AddDays(-15), 2));

            if (Check.CheckSerial("AIR30002"))
                center.AddDevice(new AC("AIR30002", "Gree", 550, DateTime.Now.AddDays(-50), 3));

            Console.WriteLine("Default data loaded (" + center.CountDevices() + " devices).");
            Console.WriteLine();
        }

        // هي دالة لإضافة جهاز جديد من المستخدم
        static void AddNewDevice(Center center)
        {
            Console.WriteLine("--- Add New Device ---");
            Console.WriteLine("1. Fridge");
            Console.WriteLine("2. Washer");
            Console.WriteLine("3. AC");
            Console.Write("Type: ");
            string type = Console.ReadLine();

            Console.Write("Serial: ");
            string serial = Console.ReadLine();

            // هون نتحقق من الرقم التسلسلي
            if (!Check.CheckSerial(serial))
            {
                Console.WriteLine("Invalid serial (must be at least 5 chars).");
                return;
            }

            Console.Write("Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Base cost: ");
            double cost;
            bool okCost = double.TryParse(Console.ReadLine(), out cost);

            // هون نتحقق إنه التكلفة رقم موجب
            if (!okCost || !Check.CheckPositive(cost))
            {
                Console.WriteLine("Invalid cost.");
                return;
            }

            DateTime now = DateTime.Now;

            if (type == "1")
            {
                Console.Write("Drawers: ");
                int drawers = int.Parse(Console.ReadLine());
                center.AddDevice(new Fridge(serial, brand, cost, now, drawers));
                Console.WriteLine("Fridge added.");
            }
            else if (type == "2")
            {
                Console.Write("Capacity (kg): ");
                int cap = int.Parse(Console.ReadLine());
                center.AddDevice(new Washer(serial, brand, cost, now, cap));
                Console.WriteLine("Washer added.");
            }
            else if (type == "3")
            {
                Console.Write("Horse Power: ");
                int hp = int.Parse(Console.ReadLine());
                center.AddDevice(new AC(serial, brand, cost, now, hp));
                Console.WriteLine("AC added.");
            }
            else
            {
                Console.WriteLine("Unknown type.");
            }
        }

        // هي دالة لإصلاح جهاز
        static void RepairDevice(Center center)
        {
            Console.Write("Enter serial to repair: ");
            string serial = Console.ReadLine();

            bool done = center.CompleteRepair(serial);

            if (done)
            {
                // منزيد العداد الثابت
                RepairedCount++;
            }
            else
            {
                Console.WriteLine("Serial not found.");
            }
        }

        // هي دالة بتطبع التكلفة الإجمالية
        static void ShowTotal(Center center)
        {
            double total = center.GetTotalCost();
            Console.WriteLine("Total Repair Cost: " + total);
        }

        // هي دالة بتطبع إحصائيات
        static void ShowStats(Center center)
        {
            Console.WriteLine("--- Statistics ---");
            Console.WriteLine("Total devices in center : " + center.CountDevices());
            Console.WriteLine("Total created (static)  : " + Center.TotalDevicesCreated);
            Console.WriteLine("Pending devices         : " + center.CountByStatus("Pending"));
            Console.WriteLine("Repaired devices        : " + center.CountByStatus("Repaired"));
            Console.WriteLine("Repaired this session   : " + RepairedCount);
        }

        // هي الدالة يلي رح تنادى لما يصير حدث الإصلاح
        static void ShowRepairMessage(string message)
        {
            Console.WriteLine(">>> REPAIR NOTICE: " + message);
        }

        // هي الدالة يلي رح تنادى لما يصير حدث التأخر
        static void ShowLateMessage(string message)
        {
            Console.WriteLine(">>> LATE NOTICE: " + message);
        }
    }
}