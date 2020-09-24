using PBK.Logic.Command;

namespace PBK
{
    public class Program
    {
        static void Main(string[] args)
        {
            var executor = new CommandExecutor();

            executor.GetStarted();
        }
    }
}
