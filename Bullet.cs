using SplashKitSDK;

public class Bullet
{
    private Bitmap _bulletBitmap;
    private double _x, _y, _angle;
    private bool _active = false;
    public Bullet(double x, double y, double angle){
        _bulletBitmap = SplashKit.BitmapNamed("images/Fire.png");
        _x = x - _bulletBitmap.Width / 2;
        _y = y - _bulletBitmap.Height / 2;
        _angle = angle;
        _active = true;
    }

    public Bullet(){
        _active = false;
    }

    public void Update(){
        const int TOAST = 8;

        Vector2D movement = new Vector2D();
        Matrix2D rotation = SplashKit.RotationMatrix(_angle);
        movement.X += TOAST;
        movement = SplashKit.MatrixMultiply(rotation, movement);
        _x += movement.X;
        _y += movement.Y;
        
        if ((_x > SplashKit.ScreenWidth() || _x < 0) || _y > SplashKit.ScreenHeight() || _y < 0){
            _active = false;
        }
    }

    public void Draw(){
        if (_active){
            DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
            _bulletBitmap.Draw(_x, _y, options);
        }
    }

    public bool CollidedWith(Robot other){
        return _bulletBitmap.CircleCollision(_x, _y, other.CollisionCircle);
    }
}