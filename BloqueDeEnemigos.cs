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
            enemigos[0, i] = new Enemigo((i + 1) * 4, 2, 0);
        }
        for (int i = 0; i < 10; i++)
        {
            enemigos[1, i] = new Enemigo(((i + 1) * 4), 4, 1);
        }
        for (int i = 0; i < 10; i++)
        {
            enemigos[2, i] = new Enemigo(((i + 1) * 4), 6, 2);
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
            int ultimoEnemigoY;
            int rd_enemigo = rd.Next(0, 10);
            int disparar_probabilidad = rd.Next(0, 10);

            if (enemigos[2,rd_enemigo].GetActivo() == true) { ultimoEnemigoY = 2; }
            else if (enemigos[1,rd_enemigo].GetActivo() == true) { ultimoEnemigoY = 1; }
            else if (enemigos[0,rd_enemigo].GetActivo() == true) { ultimoEnemigoY = 0; }
            else { ultimoEnemigoY = -1; }
        
            if (enemigoDisparos[i].GetActivo() == false && disparar_probabilidad == 1 && ultimoEnemigoY != -1) { 
                enemigos[ultimoEnemigoY, rd_enemigo].Disparar(enemigoDisparos[i]); 
            };
        }
    }
    
    public void CollisionaCon(Sprite sprite)
    {
        
    }
    public void CollisionaCon(TorreDefensiva torre)
    {
        for (int i=0; i<3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                torre.CollisionCon(enemigos[i,j]);
            }
        }
    }
    public bool ComprobarMuertos()
    {
        int contador = 0;
        for (int i=0; i<3; i++)
        {
            for (int j = 0; j< 10; j++)
            {
                if (enemigos[i, j].GetActivo() == false) { contador++; }
            }
        }
        if (contador == 30)
        {
            return true;
        }
        return false;
    }


}