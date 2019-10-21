using SplashKitSDK;
using System.Collections.Generic;
using System;

public class Player{

    private Bitmap _HeartBitmap;
    private Bitmap _PlayerBitmap;
    public double X { get; private set; }
    public double Y { get; private set; } 
    public bool Quit { get; private set;}  = false;
    private double _angle;
    public int lives { get; set; }

    public int score { get; set; }
    public List<Bullet> _bullets = new List<Bullet>();

    public int Width{
        get {
            return _PlayerBitmap.Width;
        }
    }

    public int Height{
        get {
            return _PlayerBitmap.Height;
        }
    }

    public double Angle{
        get { return _angle; }
        set { _angle = value; }
    }

    public Player(Window gameWindow){
        Angle = 360;
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        _HeartBitmap = new Bitmap("Heart", "images/heart.png");
        X = (gameWindow.Width - _PlayerBitmap.Width) / 2;//gameWindow.Width;//
        Y = (gameWindow.Height - _PlayerBitmap.Height) / 2;//gameWindow.Height / 2;
        lives = 5;
    }

    public void Rotate(double amount){
        _angle = (_angle + amount) % 360;
    }

    public void Draw(){
        _PlayerBitmap.Draw(X, Y, SplashKit.OptionRotateBmp(_angle));
        foreach(Bullet bullet in _bullets)
            bullet.Draw();
        DrawLives();
    }

    public void DrawLives(){
        int GAP = 10;
        int x = 800 - _HeartBitmap.Width - GAP;
        int y = 600 - _HeartBitmap.Height - GAP;
        for(int i=0; i<lives; i++){
            _HeartBitmap.Draw(x, y);
            x -= _HeartBitmap.Width + GAP;
        }
    }

    public void Update(){
        foreach(Bullet bullet in _bullets)
            bullet.Update();
    }

    public void Move(double amountForward, double amountStrafe){
        Vector2D movement = new Vector2D();
        Matrix2D rotation = SplashKit.RotationMatrix(_angle);
        movement.X += amountForward;movement.Y += amountStrafe;
        movement = SplashKit.MatrixMultiply(rotation, movement);
        X += movement.X;
        Y += movement.Y;
    }

    public void HandleInput(){

        //const int SPEED = 5;

        SplashKit.ProcessEvents();
        if(SplashKit.KeyDown(KeyCode.UpKey)){
            Move(0, -4);
        } else if(SplashKit.KeyDown(KeyCode.DownKey)){
            Move(0, 4);
        } else if(SplashKit.KeyDown(KeyCode.LeftKey)){
            //Rotate(4);
            Move(-4, 0);
        } else if(SplashKit.KeyDown(KeyCode.RightKey)){
            Move(4, 0);
            //Rotate(-4);
        } else if(SplashKit.KeyDown(KeyCode.LKey)){
            Rotate(-4);
        } else if(SplashKit.KeyDown(KeyCode.DKey)){
            Rotate(4);
        } else if(SplashKit.KeyTyped(KeyCode.SpaceKey)){
            Shoot();
        }else if(SplashKit.KeyDown(KeyCode.EscapeKey)){
            Quit = true;
        }
        Update();
    }

    public void StayOnWindow(Window limit){

        const int GAP = 10;

        if(X > (limit.Width - Width - GAP)){
            X = limit.Width - Width - GAP;
        } else if(X < GAP){
            X = GAP;
        } else if(Y > (limit.Height - Height - GAP)){
            Y = limit.Height - Height - GAP;
        } else if(Y < GAP){
            Y = GAP;
        }
    }

    public bool CollidedWith(Robot other){
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }

    public void Shoot()
    {
        Matrix2D anchorMatrix = SplashKit.TranslationMatrix(SplashKit.PointAt(_PlayerBitmap.Width / 2, _PlayerBitmap.Height / 2));

        // Move centre point of picture to origin
        Matrix2D result = SplashKit.MatrixMultiply(SplashKit.IdentityMatrix(), SplashKit.MatrixInverse(anchorMatrix));
        // Rotate around origin
        result = SplashKit.MatrixMultiply(result, SplashKit.RotationMatrix(_angle));
        // Move it back...
        result = SplashKit.MatrixMultiply(result, anchorMatrix);

        // Now move to location on screen...
        result = SplashKit.MatrixMultiply(result, SplashKit.TranslationMatrix(X, Y));

        // Result can now transform a point to the ship's location
        // Get right/centre
        Vector2D vector = new Vector2D();
        vector.X = _PlayerBitmap.Width / 2;
        vector.Y = _PlayerBitmap.Height / 2;
        // Transform it...
        vector = SplashKit.MatrixMultiply(result, vector);
        _bullets.Add(new Bullet(vector.X, vector.Y, Angle/2));
        Console.WriteLine(Angle);
    }
}
