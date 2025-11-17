using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Buildings
    {
        
        private Func<Player, Player, bool> generateMoney(int amount)
        {
            return (Player receiver, Player giver = null) =>
            {
                return true;
            };
        }
    }
}
