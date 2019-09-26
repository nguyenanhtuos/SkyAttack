using System;
using SplashKitSDK;


public abstract class Fly{
    
    public double X { get; private set; }
    public double Y { get; private set; }

    public Color BodyColor { get; private set; }
    public Color HeadColor { get; private set; }
    public Color WingColor { get; private set; }
    public Color CirColor { get; private set; }

    public int Width{
        get { return 80; }
    }

    public int Height{
        get { return 80; }
        
    }

    // A collision circle for the Fly.
    public Circle FlyCircle{

        get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 40); }
    }

    public Fly(Window gameWindow){

        X = SplashKit.Rnd(gameWindow.Width - Width);
        Y = 50;

        BodyColor = Color.RandomRGB(200);
        HeadColor = Color.RandomRGB(200);
        WingColor = Color.RandomRGB(200);
        CirColor = Color.RandomRGB(200);

    }


    // Draw() method - Used to draw ramdom Fly
    public abstract void Draw();


    // Update() method - Move Fly.
    public void Update(){
        const int Speed = 3;
        Y += Speed;
    }

    // IsOffscreen() Method - Return a bool value.
    public bool IsOffscreen(Window screen){

        bool result = false;

        if ((X < -Width) || (X > screen.Width) || (Y < -Height) || (Y > screen.Height-50-Height)){
            result = true;
        }

        return result;
    }

}

public class FlyOne : Fly{
    public FlyOne (Window gameWindow) : base (gameWindow){

    }

    public override void Draw(){

        double leftX = X + 20;
        double rightX = X + 60;
        
        SplashKit.FillRectangle(BodyColor, leftX, Y+40, 40, 20);

        SplashKit.FillTriangle(WingColor, X, Y+40, X+20, Y+80, X+20, Y+20);
        SplashKit.FillTriangle(WingColor, rightX+20, Y+40, rightX, Y+80, rightX, Y+20);

        SplashKit.FillCircle(CirColor, X+40, Y+50, 4);


        SplashKit.FillTriangle(Color.Blue, leftX+10, Y+60, leftX+30, Y+60, leftX+20, Y+80);
        SplashKit.FillTriangle(HeadColor, leftX+10, Y+60, leftX+30, Y+60, leftX+20, Y);
        

        SplashKit.FillCircle(CirColor, leftX-10, Y+40, 4);
        SplashKit.FillCircle(CirColor, rightX+10, Y+40, 4);
    }

}

public class FlyTwo : Fly{
    public FlyTwo (Window gameWindow) : base (gameWindow){

    }

    public override void Draw(){

        double leftX = X + 20;
        double rightX = X + 60;
        
        SplashKit.FillEllipse(BodyColor, leftX, Y+40, 40, 20);

        SplashKit.FillEllipse(WingColor, X, Y+20, 20, 60);
        SplashKit.FillEllipse(WingColor, rightX, Y+20, 20, 60);

        SplashKit.FillCircle(CirColor, X+40, Y+50, 4);


        SplashKit.FillEllipse(Color.Red, leftX+10, Y+60, 20, 25);
        SplashKit.FillTriangle(HeadColor, leftX+10, Y+60, leftX+30, Y+60, leftX+20, Y);
        

        SplashKit.FillCircle(CirColor, leftX-10, Y+40, 4);
        SplashKit.FillCircle(CirColor, rightX+10, Y+40, 4);
    }

}

public class GiftBox : Fly{
    public GiftBox (Window gameWindow) : base (gameWindow){

    }

    public override void Draw(){
        SplashKit.FillRectangle(Color.Orange, X, Y, 50, 50);
    }

}