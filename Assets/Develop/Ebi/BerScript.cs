using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{
    private float MoveSpeed = 0.5f;
    private float fixedX = -4.2f;
    private bool isMoving = true;
    public float targetY = 2.45f;

    private GameObject beerImage;
    private Vector3 beerOriginalScale;
    private Vector3 beerOriginalPos;

    private GameObject pittariImage;
    private GameObject yarinaosuImage;
    private void Start()
    {
        transform.position = new Vector3(fixedX, -2.45f, 0);

        beerImage = GameObject.Find("Beer");
        if (beerImage != null)
        {
            beerOriginalScale = beerImage.transform.localScale;
            beerOriginalPos = beerImage.transform.position;
        }

        pittariImage = GameObject.Find("PittariHaikei");
        yarinaosuImage = GameObject.Find("Yarinaosu");

        if (pittariImage != null) pittariImage.SetActive(false);
        if (yarinaosuImage != null) yarinaosuImage.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isMoving) return;

        float newY = transform.position.y + MoveSpeed * Time.fixedDeltaTime;

        if (newY >= targetY)
        {
            newY = targetY;
            isMoving = false;
        }

        transform.position = new Vector3(fixedX, newY, 0);

        if (beerImage != null)
        {
            float fillRatio = Mathf.InverseLerp(-3.0f, targetY, newY);
            float newScaleY = Mathf.Lerp(0.0f, beerOriginalScale.y, fillRatio);
            beerImage.transform.localScale = new Vector3(beerOriginalScale.x, newScaleY, beerOriginalScale.z);

            beerImage.transform.position = new Vector3(
                beerOriginalPos.x,
                beerOriginalPos.y + (newScaleY - beerOriginalScale.y) / 2f,
                beerOriginalPos.z
            );
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject.name == "Stop")
            {
                isMoving = false;

                // 判定
                if (transform.position.y >= 0.2f && transform.position.y <= 0.4f)
                {
                    Debug.Log("Hit");
                    if (pittariImage != null) pittariImage.SetActive(true);
                }
                else
                {
                    Debug.Log("Miss");
                    if (yarinaosuImage != null) yarinaosuImage.SetActive(true);
                }

                GameObject beerSosogu = GameObject.Find("BeerSosogu");
                if (beerSosogu != null)
                {
                    beerSosogu.SetActive(false);
                }
            }
        }
    }
}
