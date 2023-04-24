using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToTower : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Color PlayerNearColor;
    [SerializeField] private Color NeutralColor;
    [SerializeField] private float TowerRangeToPlayer;
    [SerializeField] private GameObject towerPrefab;

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Vector3 distance = player.transform.position - transform.position;
            float direction = distance.magnitude;
            if (direction < TowerRangeToPlayer)
            {

                renderer.material.color = PlayerNearColor;
                if (Input.GetKey(KeyCode.Space))
                {
                    Instantiate(towerPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    Debug.Log("Tower Ready");
                }
            }
            else
            {
                renderer.material.color = NeutralColor;
            }
        }
    }
}
