using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]     
    public float turnSpeed = 300f;
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 100;
    Bank bank;

    new private Rigidbody rigidbody;
    public GameObject target;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        bank = FindObjectOfType<Bank>();
    }

    private void FixedUpdate()
    {
        ProcessActions();
    }

    private void ProcessActions()
    {
        if (TurnInput != 0f)
        {
            float angle = Mathf.Clamp(TurnInput, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }

        if (!Mathf.Approximately(ForwardInput, 0f))
        {
            Vector3 verticalVelocity = Vector3.Project(rigidbody.velocity, Vector3.up);
            rigidbody.velocity = verticalVelocity + transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed / 2f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (target && other.collider.tag == target.tag)
        {
            Destroy(other.gameObject);
            
            switch (gameObject.tag)
            {
                case "Player":
                    bank.Deposite(goldReward);
                    Debug.Log("+25");
                    break;
                case "Enemy":
                    bank.Withdraw(goldPenalty);
                    Debug.Log("-100");
                    if (bank.CurrentBalance <= 0)
                    {
                        Debug.Log("You Lose!");
                        other.gameObject.GetComponent<DeathHandler>().HandleDeath();
                        return;
                    }
                    break;
            }
            
            PlaceTarget();
        }
    }

    private void PlaceTarget()
    {
        GameObject copy = Instantiate(target);
        Vector3 startPosition = new Vector3(0, 0.6f, 0);
        copy.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 8.5f;
    }
}