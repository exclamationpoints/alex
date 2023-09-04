using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    private List<Button> toggleGroup1;
    private List<Button> toggleGroup2;
    private List<Button> toggleGroup3;
    private List<Button> currentTG; // currently active toggle group

    public ChoicesStorage Storage;

    void Start()
    {
        toggleGroup1 = new List<Button>();
        toggleGroup2 = new List<Button>();
        toggleGroup3 = new List<Button>();

        foreach (Transform child in transform){
            Button toggle = child.GetComponent<Button>();

            // adding the toggles to their corresponding toggle groups
            if (toggle.name.Equals("Romantic") || toggle.name.Equals("Funny") || toggle.name.Equals("Friendly")){
                toggleGroup1.Add(toggle);
            } else if (toggle.name.Equals("Weird") || toggle.name.Equals("Sad")){
                toggleGroup2.Add(toggle);
            } else if (toggle.name.Equals("Mean")){
                toggleGroup3.Add(toggle);
            }

            // add listener to each toggle
            toggle.onClick.AddListener(delegate {ApplyChanges(toggle);});
        }
    }

    // apply effects of toggle groups
    void ApplyChanges(Button toggle)
    {
        // if button was not toggled on before, toggle it on
        if(!Storage.ToggledButtonsInclude(toggle.name)){
            // change toggle color to yellow
            toggle.GetComponent<Image>().color = Color.yellow;

            // determine currently active toggle group if there is none
            if (currentTG == null){
                if (toggleGroup1.Contains(toggle)){
                    currentTG = toggleGroup1;
                } else if (toggleGroup2.Contains(toggle)){
                    currentTG = toggleGroup2;
                } else if (toggleGroup3.Contains(toggle)){
                    currentTG = toggleGroup3;
                }
            }

            // disable toggles that are not in the currently active toggle group
            foreach (Transform child in transform){
                Button t = child.GetComponent<Button>();

                if(!currentTG.Contains(t)){
                    t.interactable = false;
                }
            }

            // add toggle to storage
            Storage.AddToggle(toggle.name);

        } else {
            // toggle button off

            // remove toggle from storage
            Storage.RemoveToggle(toggle.name);

            // change toggle color to white
            toggle.GetComponent<Image>().color = Color.white;

            // check if current toggle group is still active
            bool tgStillActive = false;

            foreach (Button t in currentTG){
                if (Storage.ToggledButtonsInclude(t.name)){
                    tgStillActive = true;
                    break;
                }
            }

            // if current toggle group is not active anymore,
            if (!tgStillActive){
                // set currentTG to null
                currentTG = null;

                // enable all toggles
                foreach (Transform child in transform){
                    Button t = child.GetComponent<Button>();

                    t.interactable = true;
                }
            }
        }
    }
}
