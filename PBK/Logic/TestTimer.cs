using System.Threading.Tasks;
using PBK.UI;
using PBK.Test_setup;
using System.Threading;

namespace PBK.Logic
{
    public class TestTimer
    {
        public const int millisecsInMinute = 60000;

        public static void Countdown(Test test)
        {
            var tm = new TimerCallback(Writer.ShowResult);

            var testTimer = new Timer(tm, test, test.TimerValue * millisecsInMinute, 0);
        }
    }
}
