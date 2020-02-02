using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : Singleton<GuiManager>
{
    // Start is called before the first frame update
    public GameObject inGameGui;
    public GameObject researchTreeGui;


    public void showReasearchTree(){
        researchTreeGui.gameObject.SetActive(true);
        inGameGui.gameObject.SetActive(false);
    }
    
    public void showInGameGui(){
        researchTreeGui.gameObject.SetActive(false);
        inGameGui.gameObject.SetActive(true);
    }
}
