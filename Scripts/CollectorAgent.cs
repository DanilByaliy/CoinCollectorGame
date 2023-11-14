using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;

public class CollectorAgent : Agent
{
    [Tooltip("The target to be moved around")]
    public GameObject target;
    private Vector3 startPosition;
    private MyCharacterController characterController;
    new private Rigidbody rigidbody;
    private int actionsReceived;


    void Start() {
        if (target.tag == "Player") return;
        for (int i = 0; i < 4; i++) {
            PlaceTarget();
        }
    }

    public override void Initialize()
    {
        startPosition = transform.position;
        characterController = GetComponent<MyCharacterController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position, rotation
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
        rigidbody.velocity = Vector3.zero;

        // PlaceTarget();
        actionsReceived = 0;
    }

    private void PlaceTarget()
    {
        GameObject copy = Instantiate(target);
        copy.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 8.5f;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Read input values and round them. GetAxisRaw works better in this case
        // because of the DecisionRequester, which only gets new decisions periodically.
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        // Convert the actions to Discrete choices (0, 1, 2)
        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = vertical >= 0 ? vertical : 2;
        actions[1] = horizontal >= 0 ? horizontal : 2;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        actionsReceived++;
        // Punish and end episode if the agent strays too far
        // if (Vector3.Distance(startPosition, transform.position) > 10f)
        // {
        //     AddReward(-1f);
        //     EndEpisode();
        // }

        // Convert actions from Discrete (0, 1, 2) to expected input values (-1, 0, +1)
        // of the character controller
        float vertical = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
        float horizontal = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;

        characterController.ForwardInput = vertical;
        characterController.TurnInput = horizontal;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If the other object is a collectible, reward and end episode
        
        if (other.collider.tag == target.tag)
        {
            int timeReward = actionsReceived < 3000 ? (3000 - actionsReceived) / 500 : 0;
            // Debug.Log(timeReward);
            // // AddReward(timeReward);
            // // AddReward(10f);
            // // EndEpisode();
        }
    }
}
