using System;

class Ovni : Enemigo
{
    int probabilidadAparecer;
    public Ovni()
    {
        probabilidadAparecer = 100;
        activo = false;
        img = "<=o=>";
        this.x = 2;
        this.y = 1;
        color = ConsoleColor.Red;
        puntuacionMorir = 1000;
    }

    public void Mover()
    {
        if (activo)
        {
            MoverA(this.x + 1, this.y);
        }
        else 
        {
            ProbabilidadActivar();
        }
    }

    private void ProbabilidadActivar()
    {
        Random rd = new Random();
        if (rd.Next(0, probabilidadAparecer) == 0) { this.SetActivo(true); }
    }
    

}