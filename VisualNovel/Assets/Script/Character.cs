using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable] // to view in editor
public class Character
// this code represent all characters that will appear in the game
{
    public string characterName;
    // the root is the container for all images related to the character in the scene. The root object
    [HideInInspector] public RectTransform root;

    public bool isMultiLayerCharacter{get{ return renderers.singleLayerImage == null;}}

    // refer to the dialog system script which we don't have rn

    // DialogSystem dialogue;

    // public void Say(string speech)
    // {
    //     dialog.Say(speech, characterName);
    // }

    // create a new character 
    public Character (string _name){
        CharacterManager cm = CharacterManager.instance;
        // locate the character prefabs
        GameObject prefab = Resources.Load("Characters/Character["+ _name+"]") as GameObject;
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform> ();
        characterName = _name;

        // get the renderers ( either the raw image renderer or the sprite renderer)
        renderers.singleLayerImage = ob.GetComponentInChildren<RawImage>();
        if ( isMultiLayerCharacter )
        {
            renderers.bodyRenderer = ob.transform.Find("bodyLayer").GetComponent<Image> ();
            renderers.expressionRenderer = ob.transform.Find("facialExpression").GetComponent<Image> ();
        }

        // diaglog system enable when have
        // dialog = DialogSystem.instance;

    }

[System.Serializable]
    public class Renderers
    {
        // use as the only image for the single layer ( what we use )
        public RawImage singleLayerImage;

        // sprites use images
        // the body renderer for a multi layer character
        public Image bodyRenderer;
        // the expression renderer for a multi layer character
        public Image expressionRenderer;
    }

    public Renderers renderers = new Renderers();
}
