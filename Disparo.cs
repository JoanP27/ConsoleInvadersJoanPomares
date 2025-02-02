using System;
class Disparo : Sprite
{
    int direccion = 0;
    bool perfora;
    int contadorVelocidad;
    int velocidad;
    bool explota;
    
    public Disparo(int x, int y, int direccion, int velocidad, bool explota = false, ConsoleColor color = ConsoleColor.White) {
        this.x = x;
        this.y = y;
        this.direccion = direccion;
        this.activo = false;
        this.velocidad = velocidad;
        img = "·";
        this.color = color;
        this.perfora = false;
        this.explota = explota;
    }
    public override void Desaparecer()
    {
        Console.SetCursorPosition(this.x, this.y);
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < img.Length; i++)
        {
            Console.Write(" ");
        }
        MoverA(1, 1);
    }
    
    public void Mover()
    {
        if (contadorVelocidad == velocidad) { MoverA(this.x, this.y + direccion); contadorVelocidad = 0; }
        else { contadorVelocidad++; }
    }

    public int GetTiempoDeCarga() { return contadorVelocidad;}

    public bool GetPerfora() { return this.perfora; }
    public void SetPerfora(bool perfora) { this.perfora = perfora; }

    public void SetVelocidad(int velocidad) { this.velocidad = velocidad; }
 
    public void Detonar(int y, int maxX)
    {
        Console.SetCursorPosition(0, y);
        for (int i=0; i<maxX -1; i++)
        {
            Console.SetCursorPosition(i, y);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
        }
        Thread.Sleep(1);
        for (int i = 0; i < maxX - 1; i++)
        {
            Console.SetCursorPosition(i, y);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }
    }
}