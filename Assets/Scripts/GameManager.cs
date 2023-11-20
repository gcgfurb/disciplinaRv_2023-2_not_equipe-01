using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float serveUpwardForce = 3f;

    [SerializeField] private GameObject racket;
    [SerializeField] private GameObject mockRacket;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject mockBall;

    private bool isServing;
    private PlayerInputActions playerActions;
    private InputAction serving;
    private InputAction restart;

    private void Awake()
    {
        playerActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        serving = playerActions.Actions.Serving;
        restart = playerActions.Actions.Reset;
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        isServing = true;
    }

    private void Update()
    {
        if (isServing)
        {
            HandleServe();
        }

        if (serving.IsPressed() && restart.IsPressed())
        {
            Reset();
        }
    }

    private void HandleServe()
    {
        var ballRb = ball.GetComponent<Rigidbody>();
        ball.transform.position = mockBall.transform.position;

        if (serving.IsPressed())
        {
            isServing = false;

            ball.transform.rotation = Quaternion.identity;
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;

            ballRb.AddForce(Vector3.up * serveUpwardForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        MoveRacket();
    }

    private void MoveRacket()
    {
        var racketRb = racket.GetComponent<Rigidbody>();

        //racketRb.transform.position = mockRacket.transform.position;
        //racketRb.transform.rotation = mockRacket.transform.rotation;
        racketRb.MovePosition(mockRacket.transform.position);
        racketRb.MoveRotation(mockRacket.transform.rotation);
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.LeaveRoom();
    }
}