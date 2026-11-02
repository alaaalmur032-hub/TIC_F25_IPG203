using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Device هون فئة الغسالة... بترث من 
 
namespace HomeRepairSystem
{

    public class Washer : Device 
    {
        // سعة الغسالة بالكيلو
        public int Capacity { get; set; }

        public Washer(string serial, string brandName, double cost, DateTime date, int capacity)
            : base(serial, brandName, cost, date)
        {
            Capacity = capacity;
        }

        public override double CalculateRepairCost()
        {
            // الغسالة تكلفتها = الأساسية + 100 لكل كيلو
            double x = BaseCost + (Capacity * 100);


            return x;
        }

        public override string ShowInfo()
        {

            return base.ShowInfo() + " | Type: Washer | Capacity: " + Capacity + " kg";
        }
    }
}