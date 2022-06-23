using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsRover.Constants.Directions;
using static MarsRover.Constants.Movements;

namespace MarsRover
{
    internal class RoverMovementService : IRoverMovementService
    {
        private static GridSize gridSize = new GridSize();
        private RoverPosition position = new RoverPosition(gridSize);

        private char[] movements;
        private string[] start;
        private string[] size;

        public void Run()
        {
            GetRoverInstructions();
            SetGridSize(size);
            SetStartingPosition(start);

            Log.Debug("Moving Rover");
            MoveRover(movements);
            Log.Information($"Rover final position : {position.PositionX} {position.PositionY} {position.Direction}");
        }
        private void GetRoverInstructions()
        {
            try
            {
                Console.Write("Enter grid size in correct format i.e. \"8 10\" - ");
                size = Console.ReadLine().Split(' ');
                Console.Write("Enter rover starting position in correct format i.e. \"12 E\" - ");
                start = Console.ReadLine().Split(' ');
                Console.Write("Enter rover movements in correct format i.e. \"MMLMRMMRRMML\" - ");
                movements = Console.ReadLine().ToCharArray();
            }
            catch (Exception e)
            {
                Log.Error($"Invalid input from user with error: {e.Message}");
                throw new Exception("Invalid input!!!");
            }
        }
        private void MoveRover(char[] movements)
        {
            foreach (char movement in movements)
            {
                if (movement == Forward)
                {
                    switch (position.Direction)
                    {
                        case East:
                            position.PositionX++;
                            break;
                        case West:
                            position.PositionX--;
                            break;
                        case North:
                            position.PositionY++;
                            break;
                        default:
                            position.PositionY--;
                            break;
                    }
                }
                else if (movement == Right)
                {
                    switch (position.Direction)
                    {
                        case East:
                            position.Direction = South;
                            break;
                        case South:
                            position.Direction = West;
                            break;
                        case West:
                            position.Direction = North;
                            break;
                        case North:
                            position.Direction = East;
                            break;
                    }
                }
                else if (movement == Left)
                {
                    switch (position.Direction)
                    {
                        case East:
                            position.Direction = North;
                            break;
                        case North:
                            position.Direction = West;
                            break;
                        case West:
                            position.Direction = South;
                            break;
                        case South:
                            position.Direction = East;
                            break;
                    }

                }
                else
                {
                    Log.Error($"Invalid movement : {movement}");
                    continue;
                }

                Log.Information($"Rover current position : {position.PositionX} {position.PositionY} {position.Direction}");
            }
        }
        private void SetGridSize(string[] size)
        {
            Log.Debug("Initializing grid - started");
            gridSize.Width = Convert.ToInt32(size[0]);
            gridSize.Height = Convert.ToInt32(size[1]);
            Log.Debug("Initializing grid - completed");
        }
        private void SetStartingPosition(string[] start)
        {
            Log.Debug("Initializing starting position - started");
            position.PositionX = (int)char.GetNumericValue(start[0][0]);
            position.PositionY = (int)char.GetNumericValue(start[0][1]);
            position.Direction = start[1];
            Log.Debug("Initializing starting position - completed");
        }
    }
}
