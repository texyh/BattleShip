using BattleShip.Core;
using System;
using System.Threading;

namespace BattleShip.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationToken = new CancellationTokenSource();

            var firingBoard = new Game();
            Console.WriteLine("==========================================================");
            Console.WriteLine("=========== Enter a coordinate from A1 through J10 ========");
            Console.WriteLine("==========================================================");

            while (!cancellationToken.IsCancellationRequested)
            {
                var input = Console.ReadLine();

                try
                {
                    var result = firingBoard.FireMissile(input);

                    if (result == ShotResultStatus.SinkedAllShips)
                    {
                        cancellationToken.Cancel();
                    }

                    Console.WriteLine(result.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Game Ended: Congratulations!!! you sunk all ships, Press any key to exit");
            Console.ReadKey();
        }
    }
}
