using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    private GameObject Panel;
    private PlayerController playerController;
    private bool inPause = false;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private AudioMixer musicMixer;

    void Start()
    {
        Panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player с тегом 'Player' не найден!");
        }
        musicSlider.value = 0.5f;
        soundSlider.value = 0.5f;
        soundMixer.SetFloat("SoundVolume", - 5);
        musicMixer.SetFloat("MusicVolume", - 5);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = !inPause;
            Panel.SetActive(inPause);
            if (playerController != null)
            {
                playerController.Playing = !inPause;
            }
        }
    }
    public void onClickBack()
    {
        inPause = false;
        Panel.SetActive(false);
        if (playerController != null)
        {
            playerController.Playing = true;
        }
    }

    public void onClickExit()
    {
        Application.Quit();
    }
    public void onChangeSoundSlider()
    {
        float value = soundSlider.value;
        Debug.Log("SoundSlider value:" + value);
        if (value == 0)
            soundMixer.SetFloat("SoundVolume", -80);
        else
            soundMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }

    public void onChangeMusicSlider()
    {
        float value = musicSlider.value;
        if(value ==0)
            musicMixer.SetFloat("MusicVolume", -80);
        else
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
}