using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractMenu : Menu
{
    public GameObject Actions;

    public List<Button> BaseActions;
    private List<Button> SoloActions;
    private List<Button> OtherActions;
    private List<string> Moods;
    private List<string> ToggledMoods;

    public List<Menu> SoloSubMenus;
    public List<Menu> OtherSubMenus;

    void Start()
    {
        Choices = new List<Button>();
        SoloActions = new List<Button>();
        OtherActions = new List<Button>();

        ConfigureChoices();

        Moods = new List<string>();
        Moods.Add("Romantic");
        Moods.Add("Funny");
        Moods.Add("Mean");
        Moods.Add("Weird");
        Moods.Add("Sad");
        Moods.Add("Friendly");

        ToggledMoods = new List<string>();
    }

    void ConfigureChoices()
    {
        foreach (Transform action in Actions.transform){
            Button btn = action.GetComponent<Button>();

            if (SoloActions.Count < 6){
                SoloActions.Add(btn);
                btn.onClick.AddListener(delegate {OpenSubMenu(Choices.IndexOf(btn));});
            } else {
                OtherActions.Add(btn);
                btn.onClick.AddListener(delegate {OpenSubMenu(Choices.IndexOf(btn));});
            }
        }

        foreach(Button btn in BaseActions){
            Choices.Add(btn);
            btn.transform.SetParent(this.gameObject.transform);
            btn.onClick.AddListener(delegate {TriggerEvent(btn.name);});
        }
    }

    public void AddAction(string toggle)
    {
        ToggledMoods.Add(toggle);

        if (ToggledMoods.Count == 1){
            this.gameObject.transform.DetachChildren();

            Choices.Clear();
        } else if(ToggledMoods.Count == 2) {

            string actionName1 = null;
            string actionName2 = null;
            Menu subMenu1 = null;
            Menu subMenu2 = null;

            if ((ToggledMoods[0].Equals("Romantic") && toggle.Equals("Friendly")) || (ToggledMoods[0].Equals("Friendly") && toggle.Equals("Romantic"))){
                actionName1 = "AskDeep";
                actionName2 = "MakeAMove";
                subMenu1 = OtherSubMenus[0];
                subMenu2 = OtherSubMenus[1];
            } else if ((ToggledMoods[0].Equals("Romantic") && toggle.Equals("Funny")) || (ToggledMoods[0].Equals("Funny") && toggle.Equals("Romantic"))){
                actionName1 = "Rizz";
                subMenu1 = OtherSubMenus[2];
            } else if ((ToggledMoods[0].Equals("Funny") && toggle.Equals("Friendly")) || (ToggledMoods[0].Equals("Friendly") && toggle.Equals("Funny"))){
                actionName1 = "Prank";
                subMenu1 = OtherSubMenus[3];
            } else if ((ToggledMoods[0].Equals("Weird") && toggle.Equals("Sad")) || (ToggledMoods[0].Equals("Sad") && toggle.Equals("Weird"))){
                actionName1 = "ExistentialCrisis";
                subMenu1 = null;
            }

            foreach(Button btn in OtherActions){
                if(btn.name.Equals(actionName1)){
                    Choices.Add(btn);
                    btn.transform.SetParent(this.gameObject.transform);
                    if (subMenu1 != null){
                        SubMenus.Add(subMenu1.gameObject);
                    } else {
                        btn.onClick.RemoveListener(delegate {OpenSubMenu(Choices.IndexOf(btn));});
                        btn.onClick.AddListener(delegate {TriggerEvent(btn.name);});
                        SubMenus.Add(null);
                    }
                } else if(btn.name.Equals(actionName2)){
                    Choices.Add(btn);
                    btn.transform.SetParent(this.gameObject.transform);
                    if (subMenu2 != null)
                        SubMenus.Add(subMenu2.gameObject);
                }
            }
        } else {
            foreach(Button btn in OtherActions){
                if(btn.name.Equals("MagicTrick")){
                    Choices.Add(btn);
                    btn.transform.SetParent(this.gameObject.transform);

                    btn.onClick.RemoveListener(delegate {OpenSubMenu(Choices.IndexOf(btn));});
                    btn.onClick.AddListener(delegate {TriggerEvent(btn.name);});
                    SubMenus.Add(null);
                    break;
                }
            }
        }

        Button action = SoloActions[Moods.IndexOf(toggle)];
        Choices.Add(action);
        action.transform.SetParent(this.gameObject.transform);

        Menu subMenu = SoloSubMenus[Moods.IndexOf(toggle)];

        if (subMenu == null){
            action.onClick.RemoveListener(delegate {OpenSubMenu(Choices.IndexOf(action));});
            action.onClick.AddListener(delegate {TriggerEvent(action.name);});
            SubMenus.Add(null);
        } else {
            SubMenus.Add(subMenu.gameObject);
        }
    }

    public void RemoveAction(string toggle)
    {
        ToggledMoods.Remove(toggle);

        this.gameObject.transform.DetachChildren();

        if (ToggledMoods.Count == 0){
            foreach(Button btn in BaseActions){
                Choices.Add(btn);
                btn.transform.SetParent(this.gameObject.transform);
            }

            SubMenus.Clear();
        } else {
            List<string> tempList = new List<string>();
            foreach (string t in ToggledMoods){
                tempList.Add(t);
            }

            ToggledMoods.Clear();
            SubMenus.Clear();

            foreach(string t in tempList){
                this.AddAction(t);
            }
        }
    }
}
