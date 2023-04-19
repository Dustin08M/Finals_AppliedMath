using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPolyTree : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectRange;
    private Color detectedColor = Color.red;
    private Color neutralcolor = Color.cyan;

    Renderer render;


    private void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = neutralcolor;
    }
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                render.material.color = detectedColor;
            }
            else
                render.material.color = neutralcolor;
        }
    }
}
