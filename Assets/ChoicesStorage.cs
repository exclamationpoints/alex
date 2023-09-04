using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesStorage : MonoBehaviour
{
    private List<string> ChosenActions; // actions chosen by the player
    private List<string> Toggled; // moods toggled on

    private bool userDecided; // check if player already decided on how to interact

    public InteractMenu interactMenu;

    void Start()
    {
        ChosenActions = new List<string>();
        Toggled = new List<string>();
        userDecided = false;
    }

    void Update()
    {
        if (userDecided){
            // insert code for dialogue, emote, etc.

            // user will choose again
            userDecided = false;

            // clear lists containing actions chosen
            ChosenActions.Clear();
        }
    }

    // methods used in other files
    
    public void AddChoice(string choice)
    {
        if (!userDecided)
            ChosenActions.Add(choice);
    }

    public void RemoveLastChoice()
    {
        if (!userDecided)
            ChosenActions.RemoveAt(ChosenActions.Count - 1);
    }

    public int ChosenActionsCount()
    {
        return ChosenActions.Count;
    }

    public void AddToggle(string toggle)
    {
        if (!userDecided){
            interactMenu.AddAction(toggle);
            Toggled.Add(toggle);
        }
    }

    public void RemoveToggle(string toggle)
    {
        if (!userDecided){
            interactMenu.RemoveAction(toggle);
            Toggled.Remove(toggle);
        }
    }

    public bool ToggledButtonsInclude(string toggle){
        return Toggled.Contains(toggle);
    }

    public void UserAlreadyDecided()
    {
        userDecided = true;
    }
}
