using System;
using SplashKitSDK;
using System.Collections.Generic;
public class SkyAttack{
    private Window _GameWindow;
    private Player _Player;
    private List<Fly> _Flies = new List<Fly>();
    private List<Bullet> _Bullets = new List<Bullet>();

    public bool Quit{  get { return _Player.Quit; }}

    private int totalScore, ScoreRemainder;
    public bool GiftVerify {get; set;}
    public bool IsSingleBullet {get; set;}

    public int TotalScore
    {
        get { return totalScore; }
        set
        {
            int increment = (value - totalScore);
            CheckCrement(increment);
            totalScore = value;
        }
    }

    // CheckCrement() Method - Check if the score increase by 5.
    private void CheckCrement(int increment)
    {
        if (increment > 0)
        {
            this.ScoreRemainder += increment;
            int rem;
            int quotient = Math.DivRem(ScoreRemainder, 5, out rem);
            if (rem == 0){
                GiftVerify = true;
            }else{
                GiftVerify = false;
            }
        }
    }

    public SkyAttack(Window GameWindow){

        _GameWindow = GameWindow;
        _Player = new Player(_GameWindow);
        IsSingleBullet = true;
        RandomFly();
    }

    // HandleInput() method - Used to call HandleInput() and StayOnWindow() from Player Class.
    public void HandleInput(){

        _Player.HandleInput();
        _Player.StayOnWindow();
        if (_Player.Shooting == true){
            _Player.Shooting = false;
            _Bullets.Add(Shoot(_Player));
        }
    }

    // Draw() method - Used to call Draw() method from Player Class and Fly Class.
    public void Draw(){
    
        _GameWindow.Clear(Color.White);
        
        _Player.Menu();
        foreach (Fly aFly in _Flies){
            aFly.Draw();
        }
        _Player.Draw();
        if (_Bullets != null) {
            foreach (var aBullet in _Bullets) {
                aBullet.Draw();
            }
        }
        _GameWindow.Refresh(60);
        
    }


    // Update() method - If Player hit a Fly, Draw a new Fly.
    public void Update(){
        for (int i = 5; i > _Flies.Count; i--){
            _Flies.Add(RandomFly());

            if (IsSingleBullet == true){
                if (GiftVerify == true){
                    _Flies.Add(GiftBox());
                } 
            }
        }


        foreach (Fly aFly in _Flies){
            aFly.Update();
        }

        if (_Bullets != null){
            foreach (var aBullet in _Bullets) {
                aBullet.Update();
            }
        }

        CheckCollisions();
    }


    private void CheckCollisions(){

        List<Fly> _RemoveFlies = new List<Fly>();
        List<Bullet> _RemoveBullet = new List<Bullet>();

        foreach (Fly aFly in _Flies){
            if ((_Player.CollidedWith(aFly)) || (aFly.IsOffscreen(_GameWindow))){
                _RemoveFlies.Add(aFly);

                if (_Player.CollidedWith(aFly) == true) {
                    _Player.LIVE -= 1;
                    IsSingleBullet = true;
                    if (_Player.LIVE <= 0){
                        _Player.Quit = true;
                    } 
                }
            }


            if (_Bullets != null) {
                foreach (var aBullet in _Bullets) {

                    if (aFly is GiftBox){
                        if (aBullet.CollideWith(aFly) == true) {
                        IsSingleBullet = false;
                        _RemoveFlies.Add(aFly);
                        _RemoveBullet.Add(aBullet);
                        }
                    }else{
                        if (aBullet.CollideWith(aFly) == true) {
                            _Player.Score += 1;
                            TotalScore += 1;
                            _RemoveFlies.Add(aFly);
                            _RemoveBullet.Add(aBullet);
                        }
                    }

                    if (aBullet.isOffScreen(_GameWindow) == true){
                        _RemoveBullet.Add(aBullet);
                    } 
                }
            }
        }

        foreach (Fly removeFly in _RemoveFlies){
                _Flies.Remove(removeFly);
        }

        foreach (Bullet aBullet in _RemoveBullet){
                _Bullets.Remove(aBullet);
        }
    }

    // RandomFly() Method - Return a random kind of Fly.
    public Fly RandomFly(){
        Fly FlyOne = new FlyOne(_GameWindow);
        Fly FlyTwo = new FlyTwo(_GameWindow);

        Random random = new Random();
        int percent = random.Next(100);

        if (percent <= (100/2)){
            return FlyOne;
        } else{
            return FlyTwo;
        }
    }

    // GiftBox() Method - Return a GiftBox
    public Fly GiftBox(){
        Fly GiftBox = new GiftBox(_GameWindow);
        return GiftBox;
    }
    
    // Shoot() Method - Return a type of Bullet.
    public Bullet Shoot (Player Player){
        Bullet singleBullet = new SingleBullet(Player);
        Bullet doubleBullet = new DoubleBullet(Player);
        
        if (IsSingleBullet == true){
            return singleBullet;
        }else{
            return doubleBullet;
        }
    }
}