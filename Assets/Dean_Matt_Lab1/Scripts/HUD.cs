//------------------------------------------------------------------------------------------------
// Author: Matthew Dean
// Date: 1/10/2016
// Credit: Unity Scripting API
// Credit: Full Sail University
// Purpose: Script used to create HUD and create dynamic values of defined variables
//------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

    #region Variables
    // Contains a reference to the Text Mesh used
    // for the score text. Remember to drag drop
    // the ScoreText game object on this variable
    // in the editor;s inspector window.
    public TextMesh scoreTextMesh;
    // Contains a reference to the Text Mesh used
    // for the score text. Remember to drag drop
    // the ScoreText game object on this variable
    // in the editor;s inspector window.
    public TextMesh shieldTextMesh;
    // Contains a reference to the Text Mesh used
    // for the score text. Remember to drag drop
    // the ScoreText game object on this variable
    // in the editor;s inspector window.
    public TextMesh ammoTextMesh;
    // This will contain a copy of the original text.
    // When the text mesh is modified with dynamic
    // values, we concatenate the numeric value with
    // this string value.
    private string scoreText;
    private string shieldText;
    private string ammoText;
    // Contains a refernce to the Life icon used
    // to instance other life icons dynamically.
    public LifeIcon lifeIconObj;
    public HealthIcon healthIconObj;
    // Stores the dynamically created instance of
    // the life icons.
    private List<LifeIcon> lifeIconInstances;
    private List<HealthIcon> healthIconInstances;
    #endregion

    // Use this for initialization
    void Start () {
        //Store a copy of the text mesh's text
        scoreText = scoreTextMesh.text;
        //Store a copy of the text mesh's text
        shieldText = shieldTextMesh.text;
        //Store a copy of the text mesh's text
        ammoText = ammoTextMesh.text;
        // A simple test to verify the function works
        // once game-play interactions are added, this
        // will be passed values based on the results
        // of game-play activities
        //updateScore(10);
        //updateShield(10);
        //updateAmmo(10);
        //A simple test to verify the function works
        //CreateLifeIcons(4);
        //CreateHealthIcons(6);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Used to update Score dynamic value
    public void updateScore (int value)
    {
        // Reset the text mesh's text to the original so
        // the previous value is overwritten with the new one
        scoreTextMesh.text = scoreText;
        // Add the value to the text
        scoreTextMesh.text += value;
    }

    public void updateLives(int numLivesRemaining)
    {
        for( int i = 0; i < lifeIconInstances.Count; ++i)
        {
            bool bActive = i < numLivesRemaining;
            lifeIconInstances[i].gameObject.SetActive(bActive);
        }
    }

    // Used to update Shield dynamic value
    public void updateShield(int value)
    {
        // Reset the text mesh's text to the original so
        // the previous value is overwritten with the new one
        shieldTextMesh.text = shieldText;
        // Add the value to the text
        shieldTextMesh.text += value;
    }

    // Used to update Ammo dynamic value
    public void updateAmmo(int value)
    {
        // Reset the text mesh's text to the original so
        // the previous value is overwritten with the new one
        ammoTextMesh.text = ammoText;
        // Add the value to the text
        ammoTextMesh.text += value;
    }

    // Used to create multiple instances of the life icon
    public void CreateLifeIcons (int numLives)
    {
        // Has the life icon instance list already been created?
        if (lifeIconInstances == null)
        {
            // Create our list of life icons then populate
            // it with the one visually places in the editor
            lifeIconInstances = new List<LifeIcon>();
            // Life Icon reference already exists so simply
            // place it in the list.
            lifeIconInstances.Add(lifeIconObj);
            // The positional step to offset each instanced life icon
            Vector3 positionOffset = new Vector3(0.5f, 0, 0);
            // Loop for each life past the first one and create a new 
            // icon for every life passed the first one
            for(int i = 1; i < numLives; ++i)
            {
                // Create new life icon
                LifeIcon newlifeIcon;
                newlifeIcon = Instantiate(lifeIconObj,
                                          lifeIconObj.transform.position,
                                          lifeIconObj.transform.rotation) as LifeIcon;
                // Apply positional offset
                Vector3 newPosition = newlifeIcon.transform.position;
                newPosition.x += (positionOffset.x * i);
                newlifeIcon.transform.position = newPosition;
                //Reset the scale of the icon to its identity
                newlifeIcon.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                // Store it in our list of instances
                lifeIconInstances.Add(newlifeIcon);
            }
        }
    }

    // Used to create multiple instances of the health icon
    public void CreateHealthIcons(int numHealth)
    {
        // Has the life icon instance list already been created?
        if (healthIconInstances == null)
        {
            // Create our list of life icons then populate
            // it with the one visually places in the editor
            healthIconInstances = new List<HealthIcon>();
            // Life Icon reference already exists so simply
            // place it in the list.
            healthIconInstances.Add(healthIconObj);
            // The positional step to offset each instanced life icon
            Vector3 positionOffset = new Vector3(0.7f, 0, 0);
            // Loop for each life past the first one and create a new 
            // icon for every life passed the first one
            for (int i = 1; i < numHealth; ++i)
            {
                // Create new life icon
                HealthIcon newhealthIcon;
                newhealthIcon = Instantiate(healthIconObj,
                                            healthIconObj.transform.position,
                                            healthIconObj.transform.rotation) as HealthIcon;
                // Apply positional offset
                Vector3 newPosition = newhealthIcon.transform.position;
                newPosition.x += (positionOffset.x * i);
                newhealthIcon.transform.position = newPosition;
                //Reset the scale of the icon to its identity
                newhealthIcon.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                // Store it in our list of instances
                healthIconInstances.Add(newhealthIcon);
            }
        }
    }
}
