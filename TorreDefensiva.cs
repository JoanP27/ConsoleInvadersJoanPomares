using System;

class TorreDefensiva : Sprite
{
    ParteDeTorre[,] partesDeTorre = new ParteDeTorre[3, 7];

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
            partesDeTorre[0,i] = new ParteDeTorre(x + i, y);
            partesDeTorre[1,i] = new ParteDeTorre(x + i, y + 1);
            partesDeTorre[2,i] = new ParteDeTorre(x + i, y + 2);
        }
    }
    public void Dibujar_Partes()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (partesDeTorre[i, j].GetActivo() == true) { partesDeTorre[i, j].Dibujar(); }
            }
        }
    }
    public ParteDeTorre[,] GetPartes()
    {
        return partesDeTorre;
    }
    public bool CollisionCon(Sprite sprite, bool destruirAlTocar = false)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (partesDeTorre[i,j].GetX() >= sprite.GetX() &&
                    partesDeTorre[i, j].GetX() <= sprite.GetX() + sprite.GetImg().Length - 1 &&
                    partesDeTorre[i, j].GetY() == sprite.GetY() &&
                    partesDeTorre[i, j].GetActivo() == true) {

                    partesDeTorre[i, j].SetActivo(false);
                    partesDeTorre[i, j].Desaparecer();

                    if (destruirAlTocar == true)
                    {
                        sprite.SetActivo(false);
                        sprite.Desaparecer();
                    }
                }
            }
        }
        return false;
    }
}