using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] HeartIcons;
    public GameObject fireballIcon, shieldIcon, lightningIcon;
    public Slider fireballSlider, shieldSlider, lightningSlider;
    GameObject player;
    float fireballSliderValue, shieldSliderValue, lightningSliderValue;

    void Start()
    {
        GameManager.instance.ThisUIManager(this);
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("UpdateLifeUI", 0.5f * Time.deltaTime);
        Invoke("EnableAbilityIcons", 2f * Time.deltaTime);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }

    //Cambia la escena
    public void ChangeScene(string Scene)
    {
        GameManager.instance.ChangeScene(Scene);
    }

    //Cierra el juego (informa por consola)
    public void QuitGame()
    {
        Debug.Log("Se ha cerrado el juego.");
        Application.Quit();
    }

    public GameObject GetActiveMenu()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true && gameObject.transform.GetChild(i).tag == "Menu")
            {
                return gameObject.transform.GetChild(i).gameObject;
            }
        }

        return null;
    }

    public void Pausa()
    {
        if (GetActiveMenu() != null) GetActiveMenu().SetActive(false);
        else gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.instance.Pausa();
    }

    public void ChangeMenu(GameObject menu)
    {
        GetActiveMenu().SetActive(false);
        menu.SetActive(true);
    }

    public void UpdateLifeUI()
    {
        if (player !=null)
        {
            int playerActualLife = player.GetComponent<Life>().GetActualLife();

            for (int i = 0; i < HeartIcons.Length; i++)
            {
                if (i <= playerActualLife - 1) HeartIcons[i].enabled = true;

                else HeartIcons[i].enabled = false;
            }
        }
        
    }

    public void EnableAbilityIcons()
    {
        if (fireballIcon != null) fireballIcon.SetActive(GameManager.instance.ReturnAbilityValue("Fireball"));
        if (shieldIcon != null) shieldIcon.SetActive(GameManager.instance.ReturnAbilityValue("Shield"));
        if (lightningIcon != null) lightningIcon.SetActive(GameManager.instance.ReturnAbilityValue("Lightning"));
    }

    public void SetSliderValue(float value, string slider)
    {
        switch (slider)
        {
            case "Fireball":
                fireballSliderValue = value;
                fireballSlider.value = fireballSliderValue;
                break;
            case "Shield":
                shieldSliderValue = value;
                shieldSlider.value = shieldSliderValue;
                break;
            case "Lightning":
                lightningSliderValue = value;
                lightningSlider.value = lightningSliderValue;
                break;
        }
    }

    public float ReturnSliderValue(string slider)
    {
        switch (slider)
        {
            case "Fireball":
                return fireballSliderValue;
            case "Shield":
                return shieldSliderValue;
            case "Lightning":
                return lightningSliderValue;
            default:
                return 0;
        }
    }
}
