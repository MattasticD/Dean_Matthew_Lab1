//------------------------------------------------------------------------------------------------
// Author: Matthew Dean
// Date: 1/10/2016
// Credit: Unity Scripting API
// Credit: Full Sail University
// Purpose: Script used for animating background #1 and defining scroll speed variables
//------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class AnimatedBackground : MonoBehaviour {

    #region Variables
    [Tooltip("Material Used for background")]
    public Material Background;
    // Controls the rate of scrolling in the two dimensions
    public Vector2 ScrollingSpeed = Vector2.zero;
    private Vector2 TextureOffset = Vector2.zero;
    #endregion

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Multiply the amount of time since last update against
        // the user specified texture speed and store it in the
        // current texture offset.
        TextureOffset += ScrollingSpeed * Time.deltaTime;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", TextureOffset);
	}
}
