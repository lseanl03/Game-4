using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoin;
    private int Index = 0;
    [SerializeField] private float speed = 3f;

    private void Update()
    {

        if (Vector2.Distance(Waypoin[Index].transform.position, transform.position) < .1f)
        {
            Index++;
            if (Index >= Waypoin.Length)
            {
                Index = 0;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, Waypoin[Index].transform.position, speed);
    }
}