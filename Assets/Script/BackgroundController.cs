using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed;
    public float startPosX;
    public float endPosX;
    public void Move()
    {
        Vector2 direction = transform.rotation.y >= 0 ? Vector2.left : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void RepeatBG()
    {
        if(transform.position.x <= endPosX)
        {
            float posX = startPosX + (transform.position.x - endPosX);
            Vector2 pos = new Vector2(posX, transform.position.y);
            transform.position = pos;
        }
    }
}
