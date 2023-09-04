using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite neutral, annoyed, concerned, happy, surprised;
    
    void Start(){
    }
    
    public void MakeNeutral(){
        this.GetComponent<Image>().sprite = neutral;
    }

    public void MakeAnnoyed(){
        this.GetComponent<Image>().sprite= annoyed;
    }

    public void MakeConcerned(){
        this.GetComponent<Image>().sprite = concerned;
    }

    public void MakeHappy(){
        this.GetComponent<Image>().sprite = happy;
        Debug.Log("Changed.");
    }

    public void MakeSurprised(){
        this.GetComponent<Image>().sprite = surprised;
    }
}
