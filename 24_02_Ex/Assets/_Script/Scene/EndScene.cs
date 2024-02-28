using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI textCount;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log(PlayerPrefs.GetInt("Count") + "변수 전달");
        textCount.text += PlayerPrefs.GetInt("Count");
    }
}
