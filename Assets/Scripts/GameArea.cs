using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Bola, GameManager
// no onTriggerExit verificar se a tag é 'Ball' se for chamar GameManager.AddPoint()

public class GameArea : MonoBehaviour
{
    [SerializeField] private Racket player1;
    [SerializeField] private Racket player2;
    [SerializeField] private GameManager manager;
    [SerializeField] private AreaNet net;
    [SerializeField] private AreaP1 areaP1;
    [SerializeField] private AreaP2 areaP2;


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("net.cont: " + net.cont + " |areaP1.cont: "+ areaP1.cont+ " |areaP2.cont: "+ areaP2.cont);
        if (other.CompareTag("Ball") && net.cont == 1 && areaP1.cont == 1 && areaP2.cont == 1)
        {
            manager.AddPointRef();
        }
        else if (other.CompareTag("Ball"))
        {
            manager.AddPointOther();
        }
    }
}
