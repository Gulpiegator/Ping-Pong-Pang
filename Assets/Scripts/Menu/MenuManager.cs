using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialised { get; private set;}
    public static GameObject TitleScreen, MainMenu, Credits, Info, Levels1to4;

    public static void Init()
    {
        GameObject menuManager = GameObject.Find("MenuManager");
        TitleScreen = menuManager.transform.Find("TitleScreen").gameObject;
        MainMenu = menuManager.transform.Find("MainMenu").gameObject;
        Credits = menuManager.transform.Find("Credits").gameObject;
        Info = menuManager.transform.Find("Info").gameObject;
        Levels1to4 = menuManager.transform.Find("Levels1to4").gameObject;
        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if(!IsInitialised)
            Init();
        switch (menu)
        {
            case Menu.TitleScreen:
                TitleScreen.SetActive(true);
                break;
            case Menu.MainMenu:
                MainMenu.SetActive(true);
                break;
            case Menu.Credits:
                Credits.SetActive(true);
                break;
            case Menu.Info:
                Info.SetActive(true);
                break;
            case Menu.Levels1to4:
                Levels1to4.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
