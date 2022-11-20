using System;

namespace HangfireContainer.Job
{
    public class Welcome
    {
        public void SendWelcome(string userName)
        {
            Console.WriteLine($"Welcome to our application, {userName}");
        }
    }
}
