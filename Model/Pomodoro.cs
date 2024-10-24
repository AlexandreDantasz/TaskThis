namespace TaskThis.Model
{
    class Pomodoro
    {
        public int Work = 50;
        public int Rest = 10;

        public Pomodoro(int work = 50, int rest = 10)
        {
            this.Work = work;
            this.Rest = rest;
        }
    }
}
