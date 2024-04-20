using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{


    PlayerActions input;
    NavMeshAgent agent;
    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;
    public float lookRotationSpeed = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        input = new PlayerActions();
        AssignInputs();
    }
    
    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;
        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var distance = (transform.position - Camera.main.transform.position).magnitude;

        transform.LookAt(ray.origin + ray.direction * distance);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }


}


