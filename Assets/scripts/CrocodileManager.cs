using UnityEngine;
using System;
using UnityEditor.Tilemaps;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;

public class CrocodileManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minDistance;
    [SerializeField] private float speed;

    private bool isFacingRight = false;


    void Update()
    {
        Follow();

        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
    }

    private void Follow()
    {
        if (Vector2.Distance(transform.position, player.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Crocodile attacks!");
    }

    private void Flip(bool isPlayerRight)
    {
        if((isPlayerRight && !isFacingRight) || (!isPlayerRight && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; 
            transform.localScale = scale;
        }
    }
}