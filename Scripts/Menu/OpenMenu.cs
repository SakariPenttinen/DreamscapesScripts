using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenMenu : MonoBehaviour {
    string fpsName = "FPSController";
    string eventName = "EventSystem";
    string mainName = "MainMenuPanel";
    string audioName = "AudioMenuPanel";
    string creditName = "CreditMenuPanel";
    string spawnName = "RespawnMenuPanel";
    string dotName = "DotCanvas";
    string lookForKey = "escape";
    FirstPersonController fpsC;
    MouseLook ml;
    GameObject events;
    GameObject mainMenu;
    GameObject audioMenu;
    GameObject creditMenu;
    GameObject spawnMenu;
    GameObject dotCanvas;
    AudioSource music;

    // Use this for initialization
    void Start () {
        music = GetComponent<AudioSource>();
        fpsC = GameObject.Find(fpsName).GetComponent<FirstPersonController>();
        ml = fpsC.GetMouseLook();
        events = gameObject.transform.Find(eventName).gameObject;
        mainMenu = gameObject.transform.Find(mainName).gameObject;
        audioMenu = gameObject.transform.Find(audioName).gameObject;
        creditMenu = gameObject.transform.Find(creditName).gameObject;
        spawnMenu = gameObject.transform.Find(spawnName).gameObject;
        dotCanvas = GameObject.Find(dotName);
    }
	
	// Update is called once per frame
	void Update () {
		if (!events.activeSelf && Input.GetKeyDown(lookForKey))
        {
            fpsC.gameObject.GetComponent<PlayerRayInteraction>().SetMenu(true);
            ml.SetCursorLock(false);
            ml.inMenu = true;
            events.SetActive(true);
            mainMenu.SetActive(true);
            dotCanvas.SetActive(false);
            music.Play();
            Time.timeScale = 0f;
        }
        if (!MenusState() && events.activeSelf)
        {
            fpsC.gameObject.GetComponent<PlayerRayInteraction>().SetMenu(false);
            ml.SetCursorLock(true);
            ml.inMenu = false;
            dotCanvas.SetActive(true);
            music.Stop();
            events.SetActive(false);
            Time.timeScale = 1f;
        }
	}

    bool MenusState()
    {
        return mainMenu.activeSelf || audioMenu.activeSelf || creditMenu.activeSelf || spawnMenu.activeSelf;
    }
}
