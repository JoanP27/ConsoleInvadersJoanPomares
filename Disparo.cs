using System;
class Disparo : Sprite
{
    int direccion = 0;
    public Disparo(int x, int y, int direccion) {
        this.x = x;
        this.y = y;
        this.direccion = direccion;
        this.activo = false;
        img = "·";
        color = ConsoleColor.White;
    }
    public override void Desaparecer()
    {
        Console.SetCursorPosition(this.x, this.y);
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < img.Length; i++)
        {
            Console.Write(" ");
        }
        MoverA(10, 10);
    }
    
    public void Mover()
    {
        MoverA(this.x, this.y + direccion);
    }

 
}