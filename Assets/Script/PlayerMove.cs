using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] int _playerSpeed;
    float GetHor;
    float GetFor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetHor = Input.GetAxis("Horizontal");
        GetFor = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(GetHor, 0, GetFor);
        transform.Translate(Direction * _playerSpeed * Time.deltaTime);
    }
}
