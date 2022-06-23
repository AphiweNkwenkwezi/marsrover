using Serilog;
using System;

namespace MarsRover
{
    public class GridSize : IGridSize
    {
        private int width;
        private int height;
        public int Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"Grid width must be bigger than 0. Current width : {value}");
                width = value;
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"Grid height must be bigger than 0. Current height : {value}");
                height = value;
            }
        }
    }
}
