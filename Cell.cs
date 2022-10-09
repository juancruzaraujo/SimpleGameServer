using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameServer
{
    
    public class Cell
    {
        bool _isObstacle;
        string _texture;
        int _indexX;
        int _indexY;
        int _indexZ;


        public int SetIndexX
        {
            set
            {
                _indexX = value;
            }
        }

        public int GetIndeX
        {
            get
            {
                return _indexX;
            }
        }

        public int SetIndexY
        {
            set
            {
                _indexY = value;
            }
        }

        public int GetIndexY
        {
            get
            {
                return _indexY;
            }
        }

        public int SetIndexZ
        {
            set
            {
                _indexZ = value; 
            }
        }

        public int GetIndexZ
        {
            get
            {
                return _indexZ;
            }
        }

        public bool SetObstacle
        {
            set
            {
                _isObstacle = value;
            }
        }

        public bool IsObstacle
        {
            get
            {
                return _isObstacle;
            }
        }
        
        public string SetTexture
        {
            set
            {
                _texture = value;
            }
        }

        public string GetTexture
        {
            get
            {
                return _texture;
            }
        }


    }
}
