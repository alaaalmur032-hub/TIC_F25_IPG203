using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// هي الفئة المجردة يلي بتطبق الواجهة تبع الطلب التاني
 

namespace HomeRepairSystem
{
    public abstract class Device : IDevice
    {
         private string serialNumber;
        private string brand;
        private string status;
        private double baseCost;
        private DateTime receiveDate;
        private bool isRepaired;

        // الرقم التسلسلي    
        public string SerialNumber
        {
            get { return serialNumber; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        // الحالة   
        public string Status
        {
            get { return status; }
            private set { status = value; }
        }

        public double BaseCost
        {
            get { return baseCost; }
            set
            {
                // هي بس للتاكد اذا مالها سالبة
                if (value >= 0)
                    baseCost = value;
            }
        }

        // تاريخ الاستلام 
        public DateTime ReceiveDate
        {
            get { return receiveDate; }
        }

        // هي لنعرف اذا تم إصلاحه ؟ 
        public bool IsRepaired
        {
            get { return isRepaired; }
        }

         public Device(string serial, string brandName, double cost, DateTime date)
        {
            serialNumber = serial;
            brand = brandName;
            baseCost = cost;
            receiveDate = date;
            status = "Pending";
            isRepaired = false;
        }

         public abstract double CalculateRepairCost();

        //    عرض معلومات الجهاز
        public virtual string ShowInfo()
        {
            return "Serial: " + serialNumber +
                   " | Brand: " + brand +
                   " | Status: " + status +
                   " | Base Cost: " + baseCost +
                   " | Received: " + receiveDate.ToString("yyyy-MM-dd");
        }

        //   دالة لتغيير حالة الجهاز
        public void ChangeStatus(string newStatus)
        {
            status = newStatus;

             if (newStatus == "Repaired")
                isRepaired = true;
        }
    }
}