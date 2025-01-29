using System;

class BloqueDeEnemigos
{
    Enemigo[,] enemigos = new Enemigo[3, 10];
    public BloqueDeEnemigos()
    {
        PopularEnemigos();
    }
    
    public void PopularEnemigos()
    {
        for (int i = 0; i < 10; i++)
        {
            enemigos[0, i] = new Enemigo((i + 1) * 5, 2, 0);
        }
        for (int i = 0; i < 10; i++)
        {
            enemigos[1, i] = new Enemigo(((i + 1) * 5), 4, 1);
        }
        for (int i = 0; i < 10; i++)
        {
            enemigos[2, i] = new Enemigo(((i + 1) * 5), 6, 2);
        }
    }
    public void Dibujar()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 10; j++)
                if (enemigos[i, j].GetActivo() == true) { enemigos[i, j].Dibujar(); }
    }
    public void Mover(int direccion)
    {
        

        if (direccion == 1)
        {
            for(int i=0;i<3;i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (enemigos[i, j].GetActivo() == true) { enemigos[i, j].MoverDerecha(); } 
                }
            }
        }
        else if (direccion == -1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++) if (enemigos[i, j].GetActivo() == true) { enemigos[i, j].MoverIzquierda(); }
            }
        }
    }

    public void MoverAbajo()
    {
        

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (enemigos[i, j].GetActivo() == true) 
                {
                    Console.SetCursorPosition(enemigos[i, j].GetX(), enemigos[i, j].GetY());
                    Console.Write("   ");
                    enemigos[i, j].MoverA(enemigos[i, j].GetX(), enemigos[i, j].GetY() + 1); 
                }
            }
        }
    }
    public Enemigo[,] GetEnemigos()
    {
        return enemigos;
    }

    public Enemigo GetUltimoEnemigo()
    {
        Enemigo lastenemigo = enemigos[0, 0];      
        for (int i = 0; i < 10; i++)
        {
            if (enemigos[0, i].GetActivo() == true) { lastenemigo = enemigos[0, i]; };
            if (enemigos[1, i].GetActivo() == true) { lastenemigo = enemigos[1, i]; };
            if (enemigos[2, i].GetActivo() == true) { lastenemigo = enemigos[2, i]; };
        }
        return lastenemigo;
    }
    public Enemigo GetPrimerEnemigo()
    {
        Enemigo firstenemigo = enemigos[0, 0];
        for (int i = 0; i < 10; i++)
        {
            if (enemigos[0, i].GetActivo() == true) { firstenemigo = enemigos[0, i]; return firstenemigo; };
            if (enemigos[1, i].GetActivo() == true) { firstenemigo = enemigos[1, i]; return firstenemigo; };
            if (enemigos[2, i].GetActivo() == true) { firstenemigo = enemigos[2, i]; return firstenemigo; };
        }
        return firstenemigo;
    }

    public void DisparoEnemigo(Disparo[] enemigoDisparos)
    {
        Random rd = new Random();
        
        for (int i = 0; i < enemigoDisparos.Length; i++)
        {
            int value = rd.Next(0, 10);
            int disparar_probabilidad = rd.Next(0, 2);
            
            if (enemigoDisparos[i].GetActivo() == false && disparar_probabilidad == 1 && enemigos[2, value].GetActivo() == true) { 
                enemigos[2, value].Disparar(enemigoDisparos[i]); 
            };
        }
    }
}