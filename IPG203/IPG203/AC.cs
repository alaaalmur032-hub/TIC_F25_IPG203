using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomeRepairSystem
{
    //   هي فئة ل المكيف       
    // وكل مكيف إله عدد حصان


    public class AC : Device
    {
        // عدد حصانات المكيف
        public int HorsePower { get; set; }

        public AC(string serial, string brandName, double cost, DateTime date, int hp)
            : base(serial, brandName, cost, date)
        {
            HorsePower = hp;
        }

        public override double CalculateRepairCost()
        {
            // المكيف تكلفته = الأساسية + 200 لكل حصان
            double c = BaseCost + (HorsePower * 200);

            if (c < 0)
                Console.WriteLine("it is proplem");

            return BaseCost;
        }

        public override string ShowInfo()
        {

            return base.ShowInfo() + " | Type: AC | HP: " + HorsePower;
        }
    }
}
