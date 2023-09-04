using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEmote : MonoBehaviour
{
    public Sprite annoyed, blank, blush, confused, happy, huh, laugh, sad, surprised;
    
    void Start(){
    }
    
    public void MakeAnnoyed(){
        this.GetComponent<Image>().sprite = annoyed;
    }
    public void MakeBlank(){
        this.GetComponent<Image>().sprite = blank;
    }

    public void MakeBlush(){
        this.GetComponent<Image>().sprite= blush;
    }

    public void MakeConfused(){
        this.GetComponent<Image>().sprite = confused;
    }

    public void MakeHappy(){
        this.GetComponent<Image>().sprite = happy;
        Debug.Log("Changed.");
    }

    public void MakeHuh(){
        this.GetComponent<Image>().sprite = huh;
    }

    public void MakeLaugh(){
        this.GetComponent<Image>().sprite = laugh;
    }

    public void MakeSad(){
        this.GetComponent<Image>().sprite = sad;
    }

    public void MakeSurprised(){
        this.GetComponent<Image>().sprite = surprised;
    }
}
