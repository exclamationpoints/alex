using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<Button> Choices; // choices found in this menu
    public ChoicesStorage Storage; // this stores the choices made by the player in each menu
    public List<GameObject> SubMenus; // list of submenus opened by each choice
    private GameObject subMenu; // submenu to be opened
    private bool resizing; // if true, the submenu to be open is opening
    private float time; // time spent in opening the submenu

    public InteractMenu interactMenu;

    void Start()
    {
        if (this.gameObject.name.Equals("Menu")){
            ConfigureMenu(this.gameObject);
        }

        // if this menu has any submenus, add a listener to each button
        if (SubMenus.Count > 0) {
            foreach (Button choice in Choices)
            {
                // open the corresponding submenu based on the button clicked
                choice.onClick.AddListener(delegate {OpenSubMenu(Choices.IndexOf(choice));});
            }

            resizing = false;
            time = 0;
        } else {
            // if this menu has no submenus, clicking a button should trigger an event
            foreach (Button choice in Choices)
            {
                // add a listener to each button to trigger an event
                choice.onClick.AddListener(delegate {TriggerEvent(choice.name);});
            }
        }
    }

    protected void OpenSubMenu(int choiceIndex)
    {
        // if there is a submenu that was opened, close it immediately
        if (subMenu != null){
            ResetMenu();

            // remove the last choice that opened the submenu from the storage
            Storage.RemoveLastChoice();

            if(interactMenu.subMenu != null){
                interactMenu.ResetMenu();
            }
        }

        // add the choice to the storage
        Storage.AddChoice(Choices[choiceIndex].name);

        subMenu = SubMenus[choiceIndex];

        if(subMenu != null){
            // change the choice's color to another color (to indicate that it was chosen)
            ColorBlock newCB = Choices[choiceIndex].colors;
            newCB.normalColor = new Color(0.698f, 0.651f, 0.541f, 1f);
            Choices[choiceIndex].colors = newCB;

            ConfigureMenu(subMenu);
    
            // initiate resizing to be done in 0.25 sec
            time = 0.25f;
            resizing = true;
        }
    }

    // configure menu's position and size, as well as the choices' positions
    protected void ConfigureMenu(GameObject Menu)
    {
        int spacePerButton = 32; // space allotted for each button

        RectTransform rt = Menu.GetComponent<RectTransform>();

        if(Menu.name.Equals("Menu")){
            // set the position of the main menu
            rt.SetLocalPositionAndRotation(new Vector3(-328, 130f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        } else {
            // set the position of the submenu based on the menu before it
            rt.SetLocalPositionAndRotation(this.gameObject.transform.localPosition + new Vector3(128/2 + 10,
                                        ((1.1f - SubMenus.IndexOf(Menu)) * spacePerButton), 0.0f),
                                        new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        }

        if (Choices.Count == 1){
            rt.position += new Vector3(0.0f, -1 * spacePerButton, 0.0f);
        } else if (Choices.Count == 4){            
            rt.position += new Vector3(0.0f, 0.5f * spacePerButton, 0.0f);
        } else if (Choices.Count == 6){            
            rt.position += new Vector3(0.0f, 1.5f * spacePerButton, 0.0f);
        }

        // set the size of the menu (width = 128, height = based on number of choices + allowance of 10)
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 128);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spacePerButton * Menu.transform.childCount + 10);

        // configure positions of each button / choice in the menu
        int count = 0;

        foreach (Transform choice in Menu.transform){
            choice.SetLocalPositionAndRotation(new Vector3(0f,
                                                spacePerButton / 2 * (Menu.transform.childCount - 1) - count * spacePerButton, 0.0f),
                                                new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            count += 1;
        }
}

    void Update()
    {
        if(resizing){
            // enlargen the submenu to full size, and applying appropriate padding
            subMenu.transform.Translate(256 * Time.deltaTime, -100 * subMenu.transform.childCount/2 * Time.deltaTime, 0.0f);
            subMenu.transform.localScale += new Vector3(3.996f * Time.deltaTime, 3.996f * Time.deltaTime, 0.0f);

            time -= Time.deltaTime;

            if (time <= 0){
                resizing = false;
            }
        }

        if (Storage.ChosenActionsCount() == 0)
        {
            ResetMenu();
        }
    }

    protected void TriggerEvent(string choiceName)
    {
        // add choice to storage
        Storage.AddChoice(choiceName);

        // tell storage that the player already finished choosing
        Storage.UserAlreadyDecided();
    }

    protected void ResetMenu()
    {
        if(subMenu != null){
            subMenu.transform.Translate(-64, 50 * subMenu.transform.childCount / 4, 0.0f);
            subMenu.transform.localScale += -subMenu.transform.localScale;
            subMenu.transform.localScale += new Vector3(0.001f, 0.001f, 0.0f);

            // change the choice's color back to white
            ColorBlock prevCB = Choices[SubMenus.IndexOf(subMenu)].colors;
            prevCB.normalColor = Color.white;
            Choices[SubMenus.IndexOf(subMenu)].colors = prevCB;

            subMenu = null;
        }
    }
}