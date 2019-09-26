using System;
using SplashKitSDK;

public class Program{

    public static Color WingColor, HeadColor, BodyColor, CirColor;
    public static void Main(){
        Window gameWindow = new Window("Sky Attack", 600, 800);
        SkyAttack SkyAttack = new SkyAttack(gameWindow);

        do{
            SplashKit.ProcessEvents();
            SkyAttack.HandleInput();
            if (SkyAttack.Quit == false){
                
                SkyAttack.Update();
                SkyAttack.Draw();
            }
        } while (!gameWindow.CloseRequested);
    }
}