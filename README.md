

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

![Sky Attack Screen](/images/SkyAttack.png 300)

Figure: End game for Sky Attack

When run, the user will control the player. They can use the arrow keys to move the player around on the screen and use Space key to shoot bullets to destroy the flies that appear from the top of the screen. Each time the player is hit by a robot, the fly will disappear, and the player will lose one life. The player will start with 10 lives, so the game is over when they have been hit by ten Flies.
