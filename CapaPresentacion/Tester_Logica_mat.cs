using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion
{
    public class Tester_Logica_mat
    {
        public decimal SumarSubtotales(List<decimal> subtotales)
        {
            decimal total = 0;
            foreach (decimal subtotal in subtotales)
            {
                total += subtotal;
            }
            return total;
        }

    }
}
