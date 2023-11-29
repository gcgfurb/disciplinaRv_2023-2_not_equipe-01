using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaP2 : MonoBehaviour
{
    public int cont = 0;

    [SerializeField] private GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            cont += 1;
            if (cont > 1) { manager.AddPointOther(); }
        }
    }
}
