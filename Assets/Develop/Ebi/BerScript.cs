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
    private GameObject beerSosogu;

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
        beerSosogu = GameObject.Find("BeerSosogu");

        if (pittariImage != null) pittariImage.SetActive(false);
        if (yarinaosuImage != null) yarinaosuImage.SetActive(false);
        if (beerSosogu != null)
        {
            beerSosogu.SetActive(true);
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

    // ← UIのStopボタンから呼ばれる
public void OnStopButtonClicked()
{
    isMoving = false;

    if (transform.position.y >= 0.2f && transform.position.y <= 0.4f)
    {
        Debug.Log("Hit");
        if (pittariImage != null) pittariImage.SetActive(true);

        GameObject startButton = GameObject.Find("Start");
        if (startButton != null)
        {
            startButton.SetActive(false);
            Debug.Log("Startボタンを非表示にしました");
        }
        else
        {
            Debug.LogWarning("StartButton が見つかりません");
        }
    }
    else
    {
        Debug.Log("Miss");
        if (yarinaosuImage != null) yarinaosuImage.SetActive(true);
    }

    if (beerSosogu != null)
    {
        beerSosogu.SetActive(false);
    }

    // ✅ Stopボタンを非表示
    GameObject stopButton = GameObject.Find("Stop");
    if (stopButton != null)
    {
        stopButton.SetActive(false);
        Debug.Log("Stopボタンを非表示にしました");
    }
    else
    {
        Debug.LogWarning("Stopボタンが見つかりません");
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
