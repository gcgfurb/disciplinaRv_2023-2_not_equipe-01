using UnityEngine;

public class Racket : MonoBehaviour
{
    public float hitForce;

    [SerializeField] private GameObject ball;

    private Rigidbody racketRb;
    private Rigidbody ballRb;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    private void Start()
    {
        racketRb = GetComponent<Rigidbody>();
        ballRb = ball.GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball")) { return; }

        Debug.Log("Hit ball");

        var contact = collision.contacts[0];
        var hitPoint = contact.point;

        var hitDirection = (hitPoint - transform.position).normalized;

        var force = hitDirection * hitForce + currentVelocity;
        force = -contact.normal.normalized * hitForce - currentVelocity;

        ballRb.AddForce(force, ForceMode.Impulse);
    }
}
