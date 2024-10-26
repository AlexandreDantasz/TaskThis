using System.Runtime.CompilerServices;
using System.Threading;
using TaskThis.Model;

namespace TaskThis.Controller
{
    class PomodoroController
    {
        public Pomodoro pomodoro;

        public PomodoroController()
        {
            pomodoro = new Pomodoro();
        }

        public bool SetWork(int minutes)
        {
            if (minutes <= 60 && minutes >= 0)
                pomodoro.Work = minutes;

            return minutes <= 60 && minutes >= 0;
        }

        public bool SetRest(int minutes)
        {
            if (minutes <= 60 && minutes >= 0)
                pomodoro.Rest = minutes;

            return minutes <= 60 && minutes >= 0;
        }

    }
}

