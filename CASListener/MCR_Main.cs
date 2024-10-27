using Sims3.Gameplay.Core;
using Sims3.SimIFace;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;
using System;

namespace Arro.MCR
{
    public class Main
    {
        private static void OnStartupApp(object sender, EventArgs e)
        {
            try
            {
                Commands.sGameCommands.Register("mcr", "Usage: Type MCR to edit the number of rows and columns.", Commands.CommandType.General, new CommandHandler(Configure_cheat));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnStartupApp");
            }
        }

        static Main()
        {
            //Simulator.AddObject(new Hair());
            //Simulator.AddObject(new Face());
            Simulator.AddObject(new Clothes());
            World.sOnStartupAppEventHandler += new EventHandler(OnStartupApp);
        }

        [Tunable]
        private static bool kInstantiator;
        private static int Configure_cheat(object[] parameters)
        {
            try
            {
                Simulator.AddObject(new OneShotFunctionTask(RCConfigure.Configure, StopWatch.TickStyles.Seconds, 1f));
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Configure_cheat");
                return 0;
            }
        }
    }

    public static class TinyUIFixForTS3Integration
    {
        public delegate float FloatGetter();

        public static FloatGetter getUIScale = () => 1f;
    }
}
//MCR will stand for more cas rows instead of more clothing rows.
//V1.1 will focus on adding more cas columns, and it will allow setting values by cheat.
//CTRL+K+C COMMENT CLTR+K+U UNCOMMENT