using Serilog;
using System;

namespace MarsRover
{
    public class RoverPosition
    {
        private readonly GridSize _size;

        private string direction;
        private int posX;
        private int posY;

        public RoverPosition(GridSize size)
        {
            _size = size;
        }

        public int PositionX
        {
            get { return posX; }
            set
            {
                if (value >= 0 && value < _size.Width)
                    posX = value;
                else
                {
                    Log.Error($"Invalid position : width of the grid is {_size.Width} and current position is {value} ");
                    throw new ArgumentException($"Invalid position : width of the grid is {_size.Width} and current position is {value} ");
                }
            }
        }

        public int PositionY
        {
            get { return posY; }
            set
            {
                if (value >= 0 && value < _size.Height)
                    posY = value;
                else
                {
                    Log.Error($"Invalid position : height of the grid is {_size.Height} and current position is {value} ");
                    throw new ArgumentException($"Invalid position : height of the grid is {_size.Height} and current position is {value} ");
                }
            }
        }

        public string Direction
        {
            get { return direction; }
            set
            {
                if (value.Length == 1)
                    direction = value;
                else
                    Log.Error($"Invalid direction : {value}");
                    throw new ArgumentException($"Invalid direction : {value}");
            }
        }
    }
}
