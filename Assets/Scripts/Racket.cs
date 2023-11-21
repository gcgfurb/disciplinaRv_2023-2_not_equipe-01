using UnityEngine;

public class Racket : MonoBehaviour
{
    public Camera playerCamera;
    public bool currentPlayer;

    [SerializeField] private GameObject mockRacket;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPlayer = false;
    }

    public void SetCurrentPlayer()
    {
        currentPlayer = true;
        playerCamera.gameObject.SetActive(true);
    }

    public void UnsetCurrentPlayer()
    {
        currentPlayer = false;
        playerCamera.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (currentPlayer)
        {
            MoveRacket();
        }
    }

    private void MoveRacket()
    {
        rb.MovePosition(mockRacket.transform.position);
        rb.MoveRotation(mockRacket.transform.rotation);
    }
}
