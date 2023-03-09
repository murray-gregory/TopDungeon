using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //unity hitbox's
    private BoxCollider2D boxCollider;

    //player position
    private Vector3 moveDelta;

    //used for collision
    private RaycastHit2D hity, hitx;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        //fixed update for frame with use of physics engine
        
    }

    //frame updater
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //reset move delta
        moveDelta = new Vector3(x, y, 0);
        moveDelta.Normalize();
    }

    //physics update
    private void FixedUpdate()
    {   
        //swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);



        hity = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Creatures","Blocking"));
        hitx = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Creatures","Blocking"));


        if(hitx.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
        
        if(hity.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


    }
}
