using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
 
namespace HomeRepairSystem
{

    //   فئة ثابتة   

    public static class Check  
    {
         public static bool CheckSerial(string serial)
        {
            if (string.IsNullOrWhiteSpace(serial))
                return false;

            // طوله 5 على الأقل
            if (serial.Length < 5)
                return false;

            return true;


        }

        // هي لدالة بتحقق إنه التاريخ موو ب المستقبل
        public static bool CheckDate(DateTime date)
        {

            if (date > DateTime.Now)
                return false;

            return true;
        }

        // هون لدالة تتحقق إنه الرقم موجب
        public static bool CheckPositive(double number)
        {
            if (number < 0)
            {
                return false;
            }

            return true;

        }

        // هي دالة تتحقق إنه الجهاز متأخر  ... أكتر من 30 يوم وما تم إصلاحه 
        public static bool IsLate(DateTime receiveDate, bool isRepaired)
        {
            if (isRepaired)
                return false;

            double days = (DateTime.Now - receiveDate).TotalDays;

            if (days > 30)
            {
                return true;

            }

            return false;
        }


    }
}