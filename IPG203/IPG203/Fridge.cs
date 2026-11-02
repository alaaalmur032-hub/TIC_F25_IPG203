using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Device هي فئة الثلاجة  بتعمل وراثة من 

namespace HomeRepairSystem
{
    public class Fridge : Device
    {
         public int Drawers { get; set; }

        public Fridge(string serial, string brandName, double cost, DateTime date, int drawers)
            : base(serial, brandName, cost, date)
        {
            Drawers = drawers;
        }

        public override double CalculateRepairCost()
        {

            // الثلاجة تكلفتها = الأساسية + 50 لكل درج
            return BaseCost + (Drawers * 50);

        }

        public override string ShowInfo()
        {
            return base.ShowInfo() + " | Type: Fridge | Drawers: " + Drawers;
       
        
        }





    }
}