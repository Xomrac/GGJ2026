using UnityEngine;

public class Crush : MonoBehaviour
{
    public float loveScore; // punteggio di favore nei confronti del giocatore, init con 0 e aumenta o diminuisce dopo ogni minigioco
    public float loveBar; // soglia dell'amore, se a fine partita viene raggiunta da loveScore la crush e' vinta
    public void updateLove(float love) => loveScore += love; // da chiamare dopo ogni minigioco per aggiornare il loveScore
    public bool loveSuccess = false;
    public void crushed(float loveScore) // controllo a fine partita, se il punteggio supera la soglia la crush e' crushata
    {
        if (loveScore >= loveBar)
            loveSuccess = true;
    }

}
