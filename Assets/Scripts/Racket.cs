using UnityEngine;

public class Racket : MonoBehaviour
{
    public Camera playerCamera;
    public bool currentPlayer;
    public int points = 0;

    [SerializeField] private GameObject mockRacket;
    [SerializeField] private GameManager manager;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        manager.SetRefPlayer(this);
    }

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
