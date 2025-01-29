using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Nave : Sprite
{
    public Nave(int x, int y)
    {
        this.x = x;
        this.y = y;
        img = "/0\\";
        color = ConsoleColor.White;
    }
    public Nave() : this(18, 20)
    {
    }
    public void MoverDerecha()
    {
        MoverA(x + 1, y);
    }
    public void MoverIzquierda()
    {
        MoverA(x - 1, y);
    }
    public void Disparar(Disparo disparo)
    {
        disparo.MoverA(this.x + 1, this.y - 1);
        disparo.SetActivo(true);
    }
}