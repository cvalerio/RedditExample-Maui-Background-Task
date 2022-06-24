using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Reddit.Maui.BackgroundTask
{
   public class MyHostedService
   {
      private  CancellationTokenSource _tokenSource;
      private PeriodicTimer timer;
      private Task timerTask;

      public void Start()
      {
         _tokenSource = new CancellationTokenSource();
         this.timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
         timerTask = DoWork();
      }

      private async Task DoWork()
      {
         try
         {
            while (await timer.WaitForNextTickAsync(_tokenSource.Token))
            {
               MessagingCenter.Send<MyHostedService, string>(this, "Tick", DateTime.Now.ToString("T"));
            }
         }
         catch (OperationCanceledException ex)
         {
         }
      }

      public async Task StopAsync()
      {
         if (timer == null)
         {
            return;
         }

         _tokenSource.Cancel();
         await timerTask;
         _tokenSource.Dispose();
      }
   }
}