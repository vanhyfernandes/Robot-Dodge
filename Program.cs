using SplashKitSDK;
using System;
using System.Windows;

public class Program
{
    public static void Main()
    {
        Window w = new Window("Player window", 800, 600);
        RobotDodge robotDodge = new RobotDodge(w);
        Timer timer = new Timer("My Timer");
        timer.Start();

        while(!w.CloseRequested && !robotDodge.Quit && robotDodge.Lives!=0){
            robotDodge.HandleInput();
            robotDodge.Update(timer);
            robotDodge.Draw();
            SplashKit.Delay(100);
        }

        if(robotDodge.Lives==0)
            GameOver(w);

        w.Close();
        w = null;
    }

    public static void GameOver(Window w){
        w.Clear(Color.White);
        Bitmap gameover = new Bitmap("Game Over", "game-over.png");    
        w.DrawBitmap(gameover, (w.Width - gameover.Width) / 2, (w.Height - gameover.Height) / 2);
        w.Refresh(60);
        SplashKit.Delay(6000);
    }


}
