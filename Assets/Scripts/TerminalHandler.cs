using NUnit.Framework;
using RoslynCSharp.Demo;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TerminalHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _UI_Reference;

    private GameObject Canvas;
    private List<GameObject> Canvas_children;
    private GameObject PopoutPanel; 
    private void Awake()
    {
        if (_UI_Reference == null)
        {
            throw new System.ArgumentNullException("_UI_Reference", "No _UI_Reference found on gameobject");
        }
    }
    void Start()
    {
        //find the canvas child of the ui reference via a path reference
        //Debug.Log($"{_UI_Reference.name}/Canvas");
        Canvas = _UI_Reference.transform.Find($"Canvas").gameObject;
        PopoutPanel = _UI_Reference.transform.Find($"{Canvas.name}/PopoutPanel").gameObject;

        Canvas_children = new List<GameObject>();
        foreach (Transform child in Canvas.transform)
        {
            Debug.Log(child.name);
            Canvas_children.Add(child.gameObject); 
            // child.gameObject gives you the GameObject reference
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == null) { return; }
        //check to see if we have hit the player gameobject
        if(collision.gameObject.CompareTag("Player"))
        {
            //set all children active==true 
            foreach (GameObject child in Canvas_children)
            {
                child.SetActive(true);
                if(child.name == "CodeEditorPanel")
                {
                    CodeEditorPanel cep = child.GetComponent<CodeEditorPanel>();
                    if(cep != null) { 
                        cep.Hide(); 
                    }
                   
                }else if(child.name == "ConsolePanel")
                {
                    ConsolePanel cp = child.GetComponent<ConsolePanel>();
                    if(cp != null) { 
                        cp.Hide(); 
                    }
                }
            }
        }

        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null) { return; }
        //check to see if we have hit the player gameobject
        if (collision.gameObject.CompareTag("Player"))
        {
            //set all children active==false 
            foreach (GameObject child in Canvas_children)
            {
                child.SetActive(false);
            }

        }

    }
}
