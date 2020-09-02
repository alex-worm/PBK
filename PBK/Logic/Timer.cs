using System.Threading.Tasks;
using PBK.UI;

namespace PBK.Logic
{
    public class Timer
    {
        public const int millisecsInMinute = 60000;

        public async static Task Countdown(int time)
        {
            await Task.Delay(time*millisecsInMinute);

            Writer.ShowResult();
        }
    }
}
