using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MyCharacterController charController;
    void Start()
    {
        charController = GetComponent<MyCharacterController>();
    }

    void Update()
    {
        charController.ForwardInput = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        charController.TurnInput = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
    }
}
