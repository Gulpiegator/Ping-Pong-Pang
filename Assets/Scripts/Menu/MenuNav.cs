using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    //public AIStrategy strategy = AIStrategy.FavorLoss;

    public void OnClick_TitleScreen()
    {
        MenuManager.OpenMenu(Menu.TitleScreen, gameObject);
    }

    public void OnClick_MainMenu()
    {
        MenuManager.OpenMenu(Menu.MainMenu, gameObject);
    }

    public void OnClick_Info()
    {
        MenuManager.OpenMenu(Menu.Info, gameObject);
    }

    public void OnClick_Credits()
    {
        MenuManager.OpenMenu(Menu.Credits, gameObject);
    }

    public void OnClick_Levels1to4()
    {
        MenuManager.OpenMenu(Menu.Levels1to4, gameObject);
    }


    public void OnClick_LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    
    public void OnClick_LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }

    public void OnClick_LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }

    public void OnClick_LoadLevel4(){
        SceneManager.LoadScene("Level4");
    }
    
}
