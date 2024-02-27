using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int life = 20;
    public float gameTime = 60.0f;
    public int count = 0;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textLife;
    public TextMeshProUGUI textCount;
    public static GameManager instance;

    public GameObject endLineObject;
    EndLine endLine;


    private void Awake()
    {
        endLine = endLineObject.GetComponent<EndLine>();
    }
    void Start()
    {
        instance = this;
        
        textTime.text = "Time: " + gameTime;
        textLife.text = "Life: " + life;
        textCount.text = "Count: " + count;
    }
    void Update()
    {
        if(life != endLine.GetLife())
        {
            life = endLine.GetLife();
            textLife.text = "Life: " + life;

            if(life <= 0)
            {
                life = 0;
                textLife.text = "Life: " + life;
            }
        }

        textCount.text = "Count: " + count;

        gameTime -= Time.deltaTime;
        if (gameTime < 0)
        {
            SceneManager.LoadScene("Fail");
        }
        textTime.text = "Time: " + (int)gameTime;

    }
}