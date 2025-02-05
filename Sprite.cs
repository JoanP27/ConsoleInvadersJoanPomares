﻿using System;
class Sprite
{
    protected int x; protected int y;
    protected string img;
    protected ConsoleColor color;
    protected ConsoleColor colorFondo = ConsoleColor.Black;
    protected bool activo = false;


    public void Dibujar()
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.BackgroundColor = colorFondo;
        Console.Write(img);
    }
    public void MoverA(int x, int y)
    {
        Console.SetCursorPosition(this.x, this.y);
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < img.Length; i++)
        {
            Console.Write(" ");
        }
        this.x = x; 
        this.y = y;
    }
    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }

    public string GetImg()
    {
        return img;
    }
    public void SetImagen(string imagen)
    {
        this.img = imagen;
    }
    public virtual void Desaparecer()
    {
        Console.SetCursorPosition(this.x, this.y);
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < img.Length; i++)
        {
            Console.Write(" ");
        }
    }
    public void SetActivo(bool value)
    {
        this.activo = value;
    }
    public bool GetActivo()
    {
        return this.activo;
    }

    // Funcion base de sprite collision con, comprueba el tamaño de la "imagen" para saber donde la bala o otro sprite collisiona con el sprite del parametro
    public virtual bool CollisionaCon(Sprite sprite)
    {
        if (this.x >= sprite.GetX() && this.x <= sprite.GetX() + sprite.GetImg().Length - 1 && this.y == sprite.GetY()) { return true; }
        return false;
    }
   
}