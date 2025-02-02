using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Nave : Sprite
{
    int vidas;
    public Nave(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.img = "/0\\";
        this.color = ConsoleColor.White;
        this.vidas = 3;
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
    public int GetVidas() { return this.vidas; }
    public void SetVidas(int vidas) { this.vidas = vidas; }
}