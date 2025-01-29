using System;

class ParteDeTorre : Sprite
{
    public ParteDeTorre(int x, int y)
    {
        this.x = x;
        this.y = y;
        img = ".";
        activo = true;
        color = ConsoleColor.White;
        colorFondo = ConsoleColor.White;
    }
}