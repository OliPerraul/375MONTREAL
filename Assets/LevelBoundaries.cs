using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundaries : MonoBehaviour
{
    public float colDepth = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;
    private Transform topCollider;
    private Transform bottomCollider;
    private Transform leftCollider;
    private Transform rightCollider;
    private Vector3 cameraPos;


    [SerializeField]
    private float xoffset;
    
    [SerializeField]
    private float yoffset;


    // Use this for initialization
    void Start()
    {
        //Generate our empty objects
        topCollider = new GameObject().transform;
        topCollider.gameObject.tag = "topboundary";

        bottomCollider = new GameObject().transform;
        bottomCollider.gameObject.tag = "bottomboundary";
        
        rightCollider = new GameObject().transform;
        rightCollider.gameObject.tag = "rightboundary";

        leftCollider = new GameObject().transform;
        leftCollider.gameObject.tag = "leftboundary";

        //Name our objects 
        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        rightCollider.name = "RightCollider";
        leftCollider.name = "LeftCollider";

        //Add the colliders
        Rigidbody2D rg = topCollider.gameObject.AddComponent<Rigidbody2D>();
        rg.isKinematic = true;
        rg =bottomCollider.gameObject.AddComponent<Rigidbody2D>();
        rg.isKinematic = true;
        rg =rightCollider.gameObject.AddComponent<Rigidbody2D>();
        rg.isKinematic = true;
        rg = leftCollider.gameObject.AddComponent<Rigidbody2D>();
        rg.isKinematic = true;


        BoxCollider2D bc = topCollider.gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc = bottomCollider.gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc = rightCollider.gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc = leftCollider.gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;

        //Make them the child of whatever object this script is on, preferably on the Camera so the objects move with the camera without extra scripting
        topCollider.parent = transform;
        bottomCollider.parent = transform;
        rightCollider.parent = transform;
        leftCollider.parent = transform;

        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

       //  xoffset = 0;
        // yoffset = 0;

        //Change our scale and positions to match the edges of the screen...   
        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 4, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f)-xoffset, cameraPos.y, zPosition);
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 4, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f)+xoffset, cameraPos.y, zPosition);
        topCollider.localScale = new Vector3(screenSize.x * 4, colDepth, colDepth);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.localScale.y * 0.5f)-yoffset, zPosition);
        bottomCollider.localScale = new Vector3(screenSize.x * 4, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), zPosition+yoffset);
    }
}