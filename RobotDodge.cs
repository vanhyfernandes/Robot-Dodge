using SplashKitSDK;
using System.Collections.Generic;
using System;

public class RobotDodge{

    private Player _Player;

    private Window _GameWindow;

    private List<Robot> _Robots;

    /*
    1 - Boxy
    2 - Roundy
    3 - Monster
    */
    private int type = 1;

    public bool Quit{
        get{
            return _Player.Quit;
        }
    } 

    public int Lives{
        get{
            return _Player.lives;
        }
    }

    public RobotDodge(Window gameWindow){
        _GameWindow = gameWindow;
        _Player = new Player(_GameWindow);
        _Robots = new List<Robot>();     
    }

    public void HandleInput(){
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }

    public void Update(Timer timer){
        _Player.score =  Convert.ToInt32(timer.Ticks / 1000);
        Console.WriteLine($"{timer.Ticks/1000} seconds");
       foreach(Robot robot in _Robots)
            robot.Update();
        
        CheckCollisions();

        if(_Robots.Count < 10)
            _Robots.Add(RandomRobot());
    
    }

    private void CheckCollisions(){
        List<Robot> remove = new List<Robot>();
        
        foreach(Robot robot in _Robots){

            // take one live if the robot collide to the player
            if(_Player.CollidedWith(robot)){
                Console.WriteLine(_Player.lives);
                remove.Add(robot);
                _Player.lives --;
            }

            // Verifies if the bullet has collided to the robot
            foreach(Bullet bullet in _Player._bullets){
                if(bullet.CollidedWith(robot))
                    remove.Add(robot);
            }
        }

        foreach(Robot robot in remove)
            _Robots.Remove(robot);

    }

    public void Draw(){
        _GameWindow.Clear(Color.White);
        _Player.Draw();
        foreach(Robot robot in _Robots)
            robot.Draw();
        _GameWindow.DrawText($"Score: {_Player.score}", Color.Black, "Bold Font", 30, 5, 5);
        _GameWindow.Refresh(60);
    }

    public Robot RandomRobot(){
        if(type == 1){
            type = 2;
            return new Boxy(_GameWindow, _Player);
        } else if(type == 2){
            type = 3;
            return new Roundy(_GameWindow, _Player);
        } else {
            type = 1;
            return new Monster(_GameWindow, _Player);
        }
    }

}