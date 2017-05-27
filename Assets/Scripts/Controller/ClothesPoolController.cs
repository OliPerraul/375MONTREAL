using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPoolController : MonoBehaviour
{
    [SerializeField]
    private int clothes_pool_size = 20;

    [SerializeField]
    private int spread_value = 4;
    
    // Use this for initialization
	void Start ()
    {
        //CreatePool();
	}


    public void CreatePool()
    {
        for (int i = 0; i < clothes_pool_size + 1; i++)
        {
            //determine year
            int years_val = Random.Range(0, 375);
            string years = "";

            if (isBetween(years_val, 0, 100))
                years = "(0-100)";

            if (isBetween(years_val, 100, 200))
                years = "(100-200)";

            if (isBetween(years_val, 200, 300))
                years = "(200-300)";

            if (isBetween(years_val, 300, 376))
                years = "(300-375)";

           // Debug.Log(years);

            
            GameObject gobj = null;

            if (isBetween(i, 0, clothes_pool_size / 4))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.HAT, years);
            else
            if (isBetween(i, clothes_pool_size / 4, clothes_pool_size / 2))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.SHIRT, years);
            else
            if (isBetween(i, clothes_pool_size / 2, (clothes_pool_size / 4) * 3))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.PANTS, years);
            else
            if (isBetween(i, ((clothes_pool_size / 4) * 3), clothes_pool_size + 1))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.SHOES, years);

            gobj.transform.position = findPosition();
            

            gobj.transform.SetParent(this.transform);//set as parent

        }

    }

    /// <summary>
    /// Removes all the childs which are not worn
    /// </summary>
    public void RemoveNotWorn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            Clothe clothe = child.GetComponent<Clothe>();

            if (!clothe.is_worn)
            {
                Destroy(child);
                
            }

        }

    }


    //helper method
    private bool isBetween(int input, int min, int max)
    {
            if (input >= min && input < max)
                return true;
       
        return false;
    }

    

    Vector3 findPosition()
    {
        Camera cam = Camera.main;

        int x_pos = Random.Range(cam.pixelWidth/spread_value, (spread_value-1)*cam.pixelWidth/spread_value);
        int y_pos = Random.Range(cam.pixelHeight/spread_value, (spread_value-1)*cam.pixelHeight/spread_value);

        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));
        pos.z = 0;//kill z value
        return pos;
    }




    // Update is called once per frame
    void Update ()
    {
		
	}
}
