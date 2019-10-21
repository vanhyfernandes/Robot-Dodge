using SplashKitSDK;
using System;
public abstract class Robot{
    public double X { get; set; }
    public double Y { get; set; } 

    public Vector2D Velocity { get; set; }

    public Color MainColor;

    public int Width{
        get {
            return 50;
        }
    }

    public int Height{
        get {
            return 50;
        }
    }

    public Circle CollisionCircle{
        get{
            return SplashKit.CircleAt(X, Y ,20);
        }
    }

    public Robot(Window gameWindow, Player player){
        if(SplashKit.Rnd() < 0.5){

            // 0 to 800
            X = SplashKit.Rnd(gameWindow.Width);

            if(SplashKit.Rnd() < 0.5)
                Y = -Height;
            else
                Y = gameWindow.Height;

        } else {

            Y = SplashKit.Rnd(gameWindow.Height);

            if(SplashKit.Rnd() < 0.5)
                X = -Width;
            else
                X = gameWindow.Width;
        }
        //X = (gameWindow.Width/2)*(-1);
        //Y = SplashKit.Rnd(gameWindow.Height);
        

        const int SPEED = 4;

        // Get a Point for the Robot
        Point2D fromPt = new Point2D(){
            X = X, Y = Y
        };
        
        // Get a Point for the Player
        Point2D toPt = new Point2D(){
            X = player.X, Y = player.Y
        };

        // Calculate the direction to head
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt,toPt));
        
        // Set the speed and assign to the Velocity
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
        
        MainColor = Color.RandomRGB(200);
    }

    public abstract void Draw();

    public void Update(){
        if(X > 800)
            X = 0;
        else if(X < 0)
            X = 800;
        else if(Y > 600)
            Y = 0;
        else if(Y < 0)
            Y = 600;
        
        X += (Velocity.X/2);
        Y += (Velocity.Y/2);
    }

    public bool IsOffScreen(Window screen){
        return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
    }
}

public class Boxy : Robot{

    public Boxy(Window gameWindow, Player player) : base(gameWindow, player){}

    public override void Draw(){
        double leftx, rightX, eyeY, mouthY;

        leftx = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        
        SplashKit.FillRectangle(Color.Gray, X, Y, 50, 50);
        SplashKit.FillRectangle(MainColor, leftx, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftx, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftx+2, mouthY+2, 21, 6);
    }

}

public class Roundy : Robot{

    public Roundy(Window gameWindow, Player player) : base(gameWindow, player){}

    public override void Draw() {
        double leftX, midX, rightX; 
        double midY, eyeY, mouthY;
        
        leftX = X + 17; 
        midX = X + 25; 
        rightX = X + 33;
        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;
        
        SplashKit.FillCircle(Color.White, midX, midY, 25); 
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25); 
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5); 
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5); 
        SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30); 
        SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }

}

public class Monster : Robot{
    private Bitmap _Monsterbitmap;
    public Monster(Window gameWindow, Player player) : base(gameWindow, player){
        _Monsterbitmap = new Bitmap("Monster", "images/monster.png");
    }

    public override void Draw() {
        _Monsterbitmap.Draw(X, Y);
    }

}