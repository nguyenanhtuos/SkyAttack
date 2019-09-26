using System;
using SplashKitSDK;

public abstract class Bullet {
    public double X { get; set; }
    public double Y { get; set; }
    public Circle SingleBulletCircle{

        get { return SplashKit.CircleAt(X, Y, 5); }
    }

    public Circle DoubleBulletCircleLeft{

        get { return SplashKit.CircleAt(X-40, Y, 5); }
    }

    public Circle DoubleBulletCircleRight{

        get { return SplashKit.CircleAt(X+40, Y, 5); }
    }


    
    public Bullet (Player aPlayer) {
        X = aPlayer.X + 60;
        Y = aPlayer.Y;
    }
    public abstract void Draw();
    public void Update () {
        int SPEED = 20;
        Y -= SPEED;
    }
    public abstract bool CollideWith (Fly other);
    public bool isOffScreen (Window gameWindow) {
        if (X < 0 || X > gameWindow.Width || Y < 0|| Y > gameWindow.Height){
           return true; 
        } 
        return false;
    }
}


class SingleBullet : Bullet {
    public SingleBullet (Player aPlayer) : base (aPlayer){

    }
    public override void Draw () {
        SplashKit.FillTriangle(Color.Red, X-5, Y,X + 5, Y, X, Y-10);
    }
     public override bool CollideWith (Fly other){
        return SplashKit.CirclesIntersect(SingleBulletCircle, other.FlyCircle);
    }
}

class DoubleBullet : Bullet {
    public DoubleBullet (Player aPlayer) : base (aPlayer){

    }
    public override void Draw () {
        SplashKit.FillTriangle(Color.Red, X-35, Y, X - 25, Y, X-30, Y-10);
        SplashKit.FillTriangle(Color.Red, X+35, Y, X + 45, Y, X+40, Y-10);
    }

    public override bool CollideWith (Fly other){
        return SplashKit.CirclesIntersect(DoubleBulletCircleLeft, other.FlyCircle) || SplashKit.CirclesIntersect(DoubleBulletCircleRight, other.FlyCircle) ;
    }
}