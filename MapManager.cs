using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameServer
{
    [DataContract]
    public class MapManager
    {
        private static Random random;
        private static object syncObj = new object();

        private Cell[,,] _cells;

        public MapManager()
        {
            random = new Random(DateTime.Now.Second);
        }
        public void GenerateMap()
        {
            _cells = new Cell[ConstantValues.LEVEL_SIZE_X, ConstantValues.LEVEL_SIZE_Y, ConstantValues.LEVEL_SIZE_Z];
            
            for (int x = 0; x < ConstantValues.LEVEL_SIZE_X; x++)
            {
                for (int y = 0; y < ConstantValues.LEVEL_SIZE_Y; y++)
                {
                    for (int z = 0; z < ConstantValues.LEVEL_SIZE_Z-1; z++) //-1 así solo hace un nivel
                    {
                        _cells[x, y, z] = CreateCell(false,x,y,z);
                    }
                }
            }

            //Console.WriteLine("grilla creada");

        }

        public void GenerateWall()
        {

            int zValue = ConstantValues.LEVEL_SIZE_Z - 1;

            for (int x = 0; x < ConstantValues.LEVEL_SIZE_X; x++)
            {
                _cells[x, 0, zValue] = CreateCell(true, x, 0, zValue);
                _cells[x, ConstantValues.LEVEL_SIZE_Y - 1, zValue] = CreateCell(true,x, ConstantValues.LEVEL_SIZE_Y - 1, zValue);
            }

            for (int y = 0; y < ConstantValues.LEVEL_SIZE_Y; y++)
            {
                _cells[0, y, zValue] = CreateCell(true,0,y,zValue);
                _cells[ConstantValues.LEVEL_SIZE_X - 1, y, zValue] = CreateCell(true, ConstantValues.LEVEL_SIZE_X - 1,y,zValue);
            }

            //Console.WriteLine("pared creada");
        }

        public void GenerateObstacles()
        {

            lstObstacles = new List<obstacles>();
            int zValue = ConstantValues.LEVEL_SIZE_Z - 1;

            for (int x = 1; x < ConstantValues.LEVEL_SIZE_X - 1; x++)
            {
                for (int y = 1; y < ConstantValues.LEVEL_SIZE_Y - 1; y++)
                {
                    //for (int z = 1; z < ConstantValues.LEVEL_SIZE_Z; z++)
                    {

                        int rndVal = GenerateRandomNumber(ConstantValues.RANDOM_CONDITION_MAX_VALUE);

                        if (rndVal <= ConstantValues.RANDOM_CONDITION_VALUE_OBSTACLE)
                        {
                            _cells[x, y, zValue] = CreateCell(true,x,y, zValue);
                            obstacles obs = new obstacles();
                            obs.x = x;
                            obs.y = y;
                            obs.z = zValue;
                            lstObstacles.Add(obs);
                        }
                    }
                }
            }

            //Console.WriteLine("obstaculos creados " + lstObstacles.Count);

        }

        private static int GenerateRandomNumber(int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new Random(); // Or exception...
                return random.Next(max);
            }
        }

        public Cell CreateCell(bool isObstacle, int x, int y, int z)
        {
            Cell cell = new Cell();
            cell.SetObstacle = false;

            return cell;
        }

        public struct obstacles
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
        }

        [DataMember]
        public List<obstacles> lstObstacles { get; set; }


        //ok sí, todo lo de acá abajo tendría que estar en su propia clase....
        [DataMember]
        public int LevelSizeX
        {
            get
            {
                return ConstantValues.LEVEL_SIZE_X;
            }
            set { }
        }

        [DataMember]
        public int LevelSizeY
        {
            get
            {
                return ConstantValues.LEVEL_SIZE_Y;
            }
            set { }
        }

        [DataMember]
        public int LevelSizeZ
        {
            get
            {
                //HACER ESTO "VIEN"

                //le paso un valor menor al juego, así no me crea un piso de dos niveles
                //return ConstantValues.LEVEL_SIZE_Z-1; 
                return ConstantValues.LEVEL_SIZE_Z;
            }
            set { }
        }

        [DataMember]
        public int Coords
        {
            get
            {
                return ConstantValues.LEVEL_COORDS_2D;
            }
            set{}
        }

    }
}
