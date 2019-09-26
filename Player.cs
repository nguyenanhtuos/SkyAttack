using System;
using SplashKitSDK;

public class Player{
    public Window _gameWindow;
    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Shooting = false;
    public int LIVE = 10;
    public int Score = 0;
    public bool Quit { get; set; }

    public int Width{
        get { return 120; }
    }

    public int Height{
        get { return 120; }
    }
    
    // A collision circle for the Player.
    public Circle PlayerCircle{

        get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 60); }
    }

    public Player(Window gameWindow){

        _gameWindow = gameWindow;
        X = (gameWindow.Width - Width) / 2;
        Y = gameWindow.Height - Height - 50;
        Quit = false;
    }

    // Draw() method - Used to draw Player.
    public void Draw(){
        double leftX = X + 40;
        double rightX = X + 80;

        _gameWindow.FillRectangle(Color.Red, leftX, Y+30, 40, 30);

        _gameWindow.FillRectangle(Color.Black, leftX-20, Y+30+30/4, 20, 15);
        _gameWindow.FillRectangle(Color.Black, rightX, Y+30+30/4, 20, 15);

        _gameWindow.FillTriangle(Color.Red, X, Y+60, X+20, Y, X+20, Y+80);
        _gameWindow.FillTriangle(Color.Red, X+120, Y+60, X+100, Y, X+100, Y+80);

        _gameWindow.FillTriangle(Color.Black, X+50, Y+30, X+70, Y+30, X+60, Y+15);        
        _gameWindow.FillTriangle(Color.Black, X+50, Y+60, X+70, Y+60, X+60, Y+120);
    }


    public void Menu(){
        _gameWindow.FillRectangle(Color.Red, 0, _gameWindow.Height-50, _gameWindow.Width, 50);

        _gameWindow.DrawText("Live:", Color.White, "BoldFont", 20, _gameWindow.Width-180, _gameWindow.Height-25);
        _gameWindow.DrawText ($"{LIVE}", Color.White, "BoldFont", 20, _gameWindow.Width-140, _gameWindow.Height-25);

        _gameWindow.DrawText("Score:", Color.White, "BoldFont", 20, _gameWindow.Width-100, _gameWindow.Height-25);
        _gameWindow.DrawText ($"{Score}", Color.White, "BoldFont", 20, _gameWindow.Width-50, _gameWindow.Height-25);
    }


    // HandleInput method - Used to change X and Y location.
    public void HandleInput(){

        // Set 5 to Int SPEED.
        int SPEED = 5;

        // If hold Left or Right, the X location will minus or add 5.
        if (SplashKit.KeyDown(KeyCode.LeftKey)){
            X += -SPEED;
            Draw();
        }

        if (SplashKit.KeyDown(KeyCode.RightKey)){
            X += SPEED;
            Draw();
        }

        // If hold Up or Down, the Y location will minus or add 5.
        if (SplashKit.KeyDown(KeyCode.UpKey)){
            Y += -SPEED;
            Draw();
        }

        if (SplashKit.KeyDown(KeyCode.DownKey)){
            Y += SPEED;
            Draw();
        }

        // If hold Escape, Quit will set to true
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {   
            if (Quit == false){
                Quit = true;
            } else{
                Quit = false;
            }
            
        }

        if (SplashKit.KeyTyped(KeyCode.SpaceKey)){
            Shooting = true;
        } 


    }
        

    // StayOnWindow method - Used to method make sure the Player on the screen.
    public void StayOnWindow(){
        const int GAP = 0;

        // If X is less than GAP, then change X to be equal to GAP.
        if (X < GAP){
            X = GAP;
        }

        // If Y is less than GAP, then change Y to be equal to GAP.
        if (Y < GAP){
            Y = GAP;
        }


        // If X is larger than (gameWindow.Width - Width - GAP)
        // Then change X to be equal to (gameWindow.Width - Width - GAP).
        if (X > (_gameWindow.Width - Width - GAP)){
            X = _gameWindow.Width - Width - GAP;
        }


        // If X is larger than (Y > gameWindow.Height - Height - GAP)
        // Then change X to be equal to (Y > gameWindow.Height - Height - GAP).
        if ((Y > _gameWindow.Height - 50 - Height - GAP)){
            Y = _gameWindow.Height - 50 - Height - GAP;
        }
    }

    public bool CollidedWith(Fly other){

        return SplashKit.CirclesIntersect(PlayerCircle, other.FlyCircle);
    }
}