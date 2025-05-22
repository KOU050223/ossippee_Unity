using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{

    private float MoveSpeed = 2.0f;
    private int direction = 1;

    private float fixedX = -4.2f; // ← X座標を左寄りに固定

    void FixedUpdate()
    {
        if (transform.position.y >= 2.45)
            direction = -1;
        if (transform.position.y <= -2.45)
            direction = 1;

        transform.position = new Vector3(fixedX,
            transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, 0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.position.y >= -0.7 && transform.position.y <= 0.7)
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }
}
