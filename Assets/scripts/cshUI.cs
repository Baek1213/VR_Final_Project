using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cshUI : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public TextMeshProUGUI hptext;
    [SerializeField] public TextMeshProUGUI Timetext;
    [SerializeField] public TextMeshProUGUI GameOver;
    [SerializeField] public TextMeshProUGUI Win;

    [SerializeField] public Button Restert;
    [SerializeField] public Button Restert2;
    public Image SKill;
    public Image HPbar;
    float gametime = 0;

    void Start()
    {

        Time.timeScale = 1;//재시작시 시간이 다시 흐름
         
        Win.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        

    }
    // Update is called once per frame

    void Update()
    {
        if (player == null) { player = GameObject.Find("Hero"); if (player == null) return; }
        
        gametime += Time.deltaTime;

        if (player.GetComponent<cshPlayerController>().HP >= 0)//죽지않고 플레이중인경우
        {
            //hptext.text = "HP : + " + player.GetComponent<cshPlayerController>().HP;
            HPbar.fillAmount = player.GetComponent<cshPlayerController>().HP / player.GetComponent<CharStats>().MaxHP;
            SKill.fillAmount = player.GetComponent<cshPlayerController>().skillTimer / player.GetComponent<CharStats>().SkillCooltime;
            

        }
        if (player.GetComponent<cshPlayerController>().HP <= 0 )
        {
            GameOver.gameObject.SetActive(true);//죽음
            Time.timeScale = 0;
        }
        /*if (player.GetComponent<cshPlayerController>().HP > 0 && )
        {
            Win.gameObject.SetActive(true);//승리
            Time.timeScale = 0;
        }*/

        Timetext.text = $"Time = { gametime.ToString("F2")}";
    }

}
