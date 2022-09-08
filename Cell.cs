using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameServer
{
    class Cell
    {
        bool isObstacle;

        public bool SetObstacle
        {
            set
            {
                isObstacle = value;
            }
        }

        public bool IsObstacle
        {
            get
            {
                return isObstacle;
            }
        }
        


    }
}
