---

title: How to make a game with C# and SplashKit
date: 2019-09-20 10:12 AEST
tags: graphics, color, audio, window
author: Nguyen Anh Tu
author_url: https://github.com/nguyenanhtuos/SkyAttack
summary: |
  This guide discusses how you can make a simple game with C# and SplashKit.

related_funcs:
- Window
- FillCircle
- FillTriangle
- FillTriangle
- ProcessEvents 
- Refresh
- CircleAt
- CirclesIntersect


---

# Sky Attack Game Project

## Description

The Sky Attack game will have a player, represented by a group of shapes on the screen, that can move UP, DOWN, LEFT and RIGHT.

<img src="/images/SkyAttack.png" width="300">
Figure: End game for Sky Attack

When run, the user will control the player. They can use the arrow keys to move the player around on the screen and use Space key to shoot bullets to destroy the flies that appear from the top of the screen. Each time the player is hit by a robot, the fly will disappear, and the player will lose one life. The player will start with 10 lives, so the game is over when they have been hit by ten Flies.

### Player (Player.cs)
The Player class contains Draw(), HandleInput() and StayOnWindow(). The Draw() method will draw the shape of the player. The  HandleInput() method will check if any of the arrow keys are held down, and will move the player by changing its X and Y location in response to these events. The StayOnWindow() method will keep the Player on the screen.

* Draw() method

```csharp
        double leftX = X + 40;
        double rightX = X + 80;

        _gameWindow.FillRectangle(Color.Red, leftX, Y+30, 40, 30);

        _gameWindow.FillRectangle(Color.Black, leftX-20, Y+30+30/4, 20, 15);
        _gameWindow.FillRectangle(Color.Black, rightX, Y+30+30/4, 20, 15);

        _gameWindow.FillTriangle(Color.Red, X, Y+60, X+20, Y, X+20, Y+80);
        _gameWindow.FillTriangle(Color.Red, X+120, Y+60, X+100, Y, X+100, Y+80);

        _gameWindow.FillTriangle(Color.Black, X+50, Y+30, X+70, Y+30, X+60, Y+15);        
        _gameWindow.FillTriangle(Color.Black, X+50, Y+60, X+70, Y+60, X+60, Y+120);

```

* Collision Circle for Player

```csharp
  public Circle PlayerCircle{

        get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 60); }
    }
```

### Fly (Fly.cs)
We create a Fly class which is an abstract class and this class will have three child classes.

* FlyOne
```csharp
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
```

* FlyTwo
```csharp
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
```

* Collision Circle for Fly
```csharp
   public Circle FlyCircle{

        get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 40); }
    }
```



### Bullet (Bullet.cs)
Bullet class is an abstract class as well and it will have two child classes include SingleBullet and DoubleBullet class.

* Single Bullet

```csharp
SplashKit.FillTriangle(Color.Red, X-5, Y,X + 5, Y, X, Y-10);
```

* Double Bullet

```csharp
SplashKit.FillTriangle(Color.Red, X-35, Y, X - 25, Y, X-30, Y-10);
        SplashKit.FillTriangle(Color.Red, X+35, Y, X + 45, Y, X+40, Y-10);
```

* Collision Circle for Bullet

Single Bullet
```csharp
    public Circle SingleBulletCircle{

        get { return SplashKit.CircleAt(X, Y, 5); }
    }
```

Double Bullet
```csharp
    public Circle DoubleBulletCircleLeft{

        get { return SplashKit.CircleAt(X-40, Y, 5); }
    }

    public Circle DoubleBulletCircleRight{

        get { return SplashKit.CircleAt(X+40, Y, 5); }
    }
```



### SkyAttack (SkyAttack.cs)
The SkyAttack class will be used to create an object that plays
the role of the game itself. It will keep track of the player, the flies, and the bullets.
It will need access to the SplashKitSDK namespace.


### CollidedWith Method

We need to check when the player collides with the fly or the bullet collides with the fly, we create a new fly.
* Player
```csharp
public bool CollidedWith(Fly other){

        return SplashKit.CirclesIntersect(PlayerCircle, other.FlyCircle);
    }
```
* Single Bullet
```csharp
     public override bool CollideWith (Fly other){
        return SplashKit.CirclesIntersect(SingleBulletCircle, other.FlyCircle);
    }
```
* Double Bullet
```csharp
public override bool CollideWith (Fly other){
        return SplashKit.CirclesIntersect(DoubleBulletCircleLeft, other.FlyCircle) || SplashKit.CirclesIntersect(DoubleBulletCircleRight, other.FlyCircle) ;
    }
```


