using System;

class TorreDefensiva : Sprite
{
    ParteDeTorre[,] partes_de_torre = new ParteDeTorre[3, 7];

    public TorreDefensiva(int x, int y)
    {
        this.x = x;
        this.y = y;
        Popular();
    }
    public void Destruir()
    {

    }
    public void Popular()
    {
        for (int i = 0; i < 7; i++)
        {
            partes_de_torre[0,i] = new ParteDeTorre(x + i, y);
            partes_de_torre[1,i] = new ParteDeTorre(x + i, y + 1);
            partes_de_torre[2,i] = new ParteDeTorre(x + i, y + 2);
        }
    }
    public void Dibujar_Partes()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (partes_de_torre[i, j].GetActivo() == true) { partes_de_torre[i, j].Dibujar(); }
            }
        }
    }
    public ParteDeTorre[,] GetPartes()
    {
        return partes_de_torre;
    }
}