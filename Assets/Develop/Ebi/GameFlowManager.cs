using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [Header("UI Panels")]  // Inspector で各 Panel をアタッチ
    public GameObject KaisetuPanel;
    public GameObject MenuPanel;
    public GameObject GamePanel;

    void Start()
    {
        // 初期状態：解説のみ表示
        Debug.Log("GameFlowManager Start: setting initial panel states");
        if (KaisetuPanel == null) Debug.LogError("KaisetuPanel is not assigned in Inspector!");
        if (MenuPanel == null) Debug.LogError("MenuPanel is not assigned in Inspector!");
        if (GamePanel == null) Debug.LogError("GamePanel is not assigned in Inspector!");

        if (KaisetuPanel != null) KaisetuPanel.SetActive(true);
        if (MenuPanel != null) MenuPanel.SetActive(false);
        if (GamePanel != null) GamePanel.SetActive(false);
    }

    // OK ボタンの OnClick() に登録
    public void OnClick()
    {
        if (KaisetuPanel == null)
        {
            Debug.LogError("OnClick: KaisetuPanel が null です! Inspector を再チェックして下さい");
            return;
        }

        Debug.Log($"OnClick: Before Hide: {KaisetuPanel.name}.activeSelf = {KaisetuPanel.activeSelf}");
        KaisetuPanel.SetActive(false);
        Debug.Log($"OnClick: After Hide:  {KaisetuPanel.name}.activeSelf = {KaisetuPanel.activeSelf}");

        if (MenuPanel != null)
        {
            Debug.Log($"OnClick: Enabling MenuPanel: {MenuPanel.name}.activeSelf = {MenuPanel.activeSelf}");
            MenuPanel.SetActive(true);
            Debug.Log($"OnClick: MenuPanel now:        {MenuPanel.name}.activeSelf = {MenuPanel.activeSelf}");
        }
        else
        {
            Debug.LogError("OnClick: MenuPanel が null です!");
        }
    }

    // Start ボタンの OnClick() に登録
    public void StartGame()
    {
        Debug.Log("StartGame() called");

        if (MenuPanel != null)
        {
            MenuPanel.SetActive(false);
            Debug.Log("MenuPanel.SetActive(false)");
        }
        else
        {
            Debug.LogError("StartGame: MenuPanel is null");
        }

        if (GamePanel != null)
        {
            GamePanel.SetActive(true);
            Debug.Log("GamePanel.SetActive(true)");
        }
        else
        {
            Debug.LogError("StartGame: GamePanel is null");
        }

        // bar スクリプトの Begin() を呼び出し
        var bar = FindObjectOfType<Bar>();
        if (bar != null)
        {
            bar.Begin();
            Debug.Log("Bar.Begin() called");
        }
        else
        {
            Debug.LogError("StartGame: No Bar object found");
        }
    }

    public void RetryGame()
    {
        Debug.Log("RetryGame() called");

        // パネル制御：Menu を非表示、Game を表示（＝再開）
        if (MenuPanel != null) MenuPanel.SetActive(false);
        if (GamePanel != null) GamePanel.SetActive(true);

        // Bar を探してリセット → Begin() で再スタート
        var bar = FindObjectOfType<Bar>();
        if (bar != null)
        {
            // 位置とスケールを初期化（Bar 側に関数作るのが理想）
            bar.ResetBar();
            bar.Begin();
            Debug.Log("Bar.ResetBar() & Begin() called");
        }

        // やり直す画像を非表示に（演出後）
        GameObject yarinaosuImage = GameObject.Find("Yarinaosu");
        if (yarinaosuImage != null)
        {
            yarinaosuImage.SetActive(false);
        }
    }

}
