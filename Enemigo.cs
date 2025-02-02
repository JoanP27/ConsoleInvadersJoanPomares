using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Enemigo : Sprite
{
    protected int puntuacionMorir;
    public Enemigo(int x, int y, int tipo)
    {
        this.activo = true;
        this.x = x;
        this.y = y;

        if (tipo == 0)
        {
            img = "]|[";
            color = ConsoleColor.Cyan;
            puntuacionMorir = 200;
        }
        else if (tipo == 1)
        {
            img = "}|{";
            color = ConsoleColor.Red;
            puntuacionMorir = 100;
        }
        else {
            img = ")|(";
            color = ConsoleColor.Green;
            puntuacionMorir = 50;
        }
    }
    public Enemigo()
    {
        x = 0;
        y = 3;
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
        disparo.MoverA(this.x + 1, this.y + 1);
        disparo.SetActivo(true);
    }
    public override void Desaparecer()
    {
        base.Desaparecer();
        MoverA(1, 1);
    }
    public int GetPuntuacionMorir()
    {
        return puntuacionMorir;
    }


}