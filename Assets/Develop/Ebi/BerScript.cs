using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    private float MoveSpeed = 0.5f;
    private float fixedX = -4.2f;
    private bool isMoving = false;
    public float targetY = 2.45f;

    private GameObject beerImage;
    private Vector3 beerOriginalScale;
    private Vector3 beerOriginalPos;

    private GameObject pittariImage;
    private GameObject yarinaosuImage;
    private GameObject beerSosogu; // ← キャッシュする変数

    void Start()
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
        beerSosogu = GameObject.Find("BeerSosogu"); // ← 一度だけ取得

        if (pittariImage != null) pittariImage.SetActive(false);
        if (yarinaosuImage != null) yarinaosuImage.SetActive(false);
        if (beerSosogu != null)
        {
            beerSosogu.SetActive(true); // 初期状態で表示
        }
        else
        {
            Debug.LogError("Start: BeerSosogu が見つかりません！");
        }
    }

    public void Begin()
    {
        isMoving = true;
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

            beerImage.transform.localScale = new Vector3(
                beerOriginalScale.x, newScaleY, beerOriginalScale.z);

            beerImage.transform.position = new Vector3(
                beerOriginalPos.x,
                beerOriginalPos.y + (newScaleY - beerOriginalScale.y) / 2f,
                beerOriginalPos.z
            );
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null && hit.collider.gameObject.name == "Stop")
            {
                isMoving = false;

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

                // キャッシュを使って非表示にする
                if (beerSosogu != null)
                {
                    beerSosogu.SetActive(false);
                }
            }
        }
    }

    public void ResetBar()
    {
        transform.position = new Vector3(fixedX, -2.45f, 0);
        isMoving = false;

        if (beerImage != null)
        {
            beerImage.transform.localScale = beerOriginalScale;
            beerImage.transform.position = beerOriginalPos;
        }

        if (pittariImage != null) pittariImage.SetActive(false);
        if (yarinaosuImage != null) yarinaosuImage.SetActive(false);

        // キャッシュを使って再表示
        if (beerSosogu != null)
        {
            beerSosogu.SetActive(true);
        }
        else
        {
            Debug.LogError("ResetBar: beerSosogu が null です！");
        }
    }
}
