using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Awake()
    { 
        navMeshAgent = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(input.magnitude <= 0)
        {
            return;
        }

        if(Mathf.Abs(input.y) > 0.01f)
        {
            Move(input);
        }
        else
        {
            Rotate(input);
        }


    }
    private void Rotate(Vector2 input)
    {
        navMeshAgent.destination = transform.position;
        transform.Rotate(0, input.x * navMeshAgent.angularSpeed * Time.deltaTime, 0);
    }
    private void Move(Vector2 input)
    {
        Vector3 destination = transform.position + transform.right * input.x + transform.forward * input.y;
        navMeshAgent.destination = destination;
    }
}
