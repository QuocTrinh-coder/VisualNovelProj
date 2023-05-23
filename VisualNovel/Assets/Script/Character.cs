using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// By marking the class as serializable, instances of the Character class can be saved, loaded, or passed between different parts of the program.
[System.Serializable]
    // This line declares the Character class, which represents a character in the game.
    public class Character
// this code represent all characters that will appear in the game
{
    // It is used to store the name of the character.
    public string characterName;
    // the root is the container for all images related to the character in the scene. The root object
    // The [HideInInspector] attribute indicates that this variable should not be shown in the Unity editor's Inspector window. 
    // It is used as a container for all the images related to the character in the scene. 
    [HideInInspector] public RectTransform root;

    // This is a public read-only property named isMultiLayerCharacter. It returns a boolean value indicating whether the character is a multi-layer character or not. 
    // The implementation checks the singleLayerImage variable of the renderers object. If singleLayerImage is null, it means that the character is not a multi-layer character, and the property returns true. 
    // If singleLayerImage is not null, it means the character is a multi-layer character, and the property returns false.
    public bool isMultiLayerCharacter{get{ return renderers.singleLayerImage == null;}}

    // this is the space between the anchor of this character. Defines how much space the character take up on the canvas
    public Vector2 anchorPadding { get {return root.anchorMax - root.anchorMin;}}


    // turn chracter on and off depend on if they are speaking or not ( continue in the video from 25:54 character management video)
    // public bool enabled { get {return root.gameObject.activeInHierarchy;} set {root.gameObject.SetActive(value);}}


    // refer to the dialog system script which we don't have rn

    // DialogSystem dialogue;

    // public void Say(string speech)
    // {
    //     dialog.Say(speech, characterName);
    // }

    // create a new character 
    // This is a constructor for the Character class that takes a string parameter _name representing the character's name.
    public Character (string _name){
        // It first gets a reference to the CharacterManager instance
        CharacterManager cm = CharacterManager.instance;
        // locate the character prefabs
        GameObject prefab = Resources.Load("Characters/Character["+ _name+"]") as GameObject;
        // It instantiates the prefab as a game object under the characterPanel in the CharacterManager 
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);
        // assigns the instantiated object's RectTransform component to the root variable.
        root = ob.GetComponent<RectTransform> ();
        // Assigns the character's name based on the provided _name.
        characterName = _name;

        // get the renderers ( either the raw image renderer or the sprite renderer)
        renderers.singleLayerImage = ob.GetComponentInChildren<RawImage>();
        // It assigns the renderers based on whether the character is a multi-layer character or not. If it is a multi-layer character, it assigns the body renderer and expression renderer from the appropriate child objects of the instantiated character prefab.
        if ( isMultiLayerCharacter )
        {
            renderers.bodyRenderer = ob.transform.Find("bodyLayer").GetComponent<Image> ();
            renderers.expressionRenderer = ob.transform.Find("facialExpression").GetComponent<Image> ();
        }

        // diaglog system enable when have
        // dialog = DialogSystem.instance;

    }

[System.Serializable]
    // This is an inner class named Renderers that represents the renderers used by the character. 
    public class Renderers
    {
        // use as the only image for the single layer ( what we use )
        //  A RawImage variable used as the only image for a single-layer character.
        public RawImage singleLayerImage;

        // sprites use images
        // the body renderer for a multi layer character
        //  An Image variable representing the body renderer for a multi-layer character.
        public Image bodyRenderer;
        // the expression renderer for a multi layer character
        // An Image variable representing the expression renderer for a multi-layer character.
        public Image expressionRenderer;
    }
    // An instance of the Renderers class is created as the renderers variable of the Character class.
    public Renderers renderers = new Renderers();

    Vector2 targetPosition;
    Coroutine moving; // movement move through here
    bool isMoving{ get { return moving != null; }}

    public void Move(Vector2 Target, float speed, bool smooth = true)
    {
        // if we are moving, stop moving 
        StopMoving();
        // start moving coroutine
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }

    public void StopMoving( bool arriveAtTargetPositionImmidiately = false) {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine(moving);
            if (arriveAtTargetPositionImmidiately ){
                setPosition (targetPosition);
            }
        }
        moving = null ;
    }


// immidiately set the position of this character to the intended target
    public void setPosition(Vector2 target){
        // now we want to get the padding between the achors of this character so we know what their so we know what are the min and max position are
        Vector2 padding = anchorPadding;

        // now get the limitations for 0 to 100% movement
        // the farthest a character can move to the right before reaching 100% should be the 1 value to mulitple our target by
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        // now get the actual position target for the minimum anchors ( left/bottom bounds) of the character. because MaxX and maxY is just a percentage 
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY - targetPosition.y);
        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;
    }


    // control our character movement
    float speed; // Variable for controlling the movement speed
    bool smooth; // Variable for determining whether to use smooth movement
    IEnumerator Moving(Vector2 Target, float speed, bool smooth = true) // smooth movement use looping to accelerate and decelerate character
    {
        targetPosition = Target;
        // now we want to get the padding between the achors of this character so we know what their so we know what are the min and max position are
        Vector2 padding = anchorPadding;

        // now get the limitations for 0 to 100% movement
        // the farthest a character can move to the right before reaching 100% should be the 1 value to mulitple our target by
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        // now get the actual position target for the minimum anchors ( left/bottom bounds) of the character. because MaxX and maxY is just a percentage 
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY - targetPosition.y);

        // this is the loop to make the smooth transition. Move until we reach the target position
        speed *= Time.deltaTime;
        while (root.anchorMin != minAnchorTarget){
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame ();
        }

        StopMoving();
    }
}

// Summary
// It adds properties to determine if the character is a multi-layer character and to enable or disable the character. 
// It also includes a constructor to create a new character based on a given name, loading the appropriate prefab and assigning renderers.
// The Renderers class is used to store references to the character's renderers, either for a single-layer character or a multi-layer character.