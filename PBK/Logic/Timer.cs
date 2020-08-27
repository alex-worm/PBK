using System.Threading.Tasks;

namespace PBK.Logic
{
    class Timer
    {
        protected async static Task Countdown(int time)
        {
            await Task.Delay(time*60*1000);

            //return Writer.ShowResult();
        }
    }
}
