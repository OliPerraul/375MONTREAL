using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ClothesPoolController : MonoBehaviour
{
        
    [SerializeField]
    private int clothes_pool_size = 10;

    [SerializeField]
    private int spread_value = 4;
    
    // Use this for initialization
	void Start ()
    {
        //CreatePool();
	}


    public void CreatePool()
    {
        //creates a copy of the lookup theme list
        List<string> themes_list = CloneList<string>(Global.themes_lookup);
                
        for (int i = 0; i < clothes_pool_size + 1; i++)
        {
            string theme = themes_list[Random.Range(0,themes_list.Count-1)];//find item in the list
            //themes_list.RemoveAt(Random.Range(0, themes_list.Count));//remove theme from list
                        
            GameObject gobj = null;

            if (isBetween(i, 0, clothes_pool_size / 3))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.HAT, theme);
            else
            if (isBetween(i, clothes_pool_size/3, (clothes_pool_size/3)*2))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.SHIRT, theme);
            else
            if (isBetween(i, (clothes_pool_size / 3) * 2, clothes_pool_size+1))
                gobj = Clothe.Create(Clothe.CLOTHE_TYPE.PANTS, theme);
           
       
            gobj.transform.position = findPosition();
            

            gobj.transform.SetParent(this.transform);//set as parent

        }

    }

    /// <summary>
    /// HELPER METHOD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="oldList"></param>
    /// <returns></returns>
    public static List<T> CloneList<T>(List<T> oldList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, oldList);
        stream.Position = 0;
        return (List<T>)formatter.Deserialize(stream);
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

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent<Clothe>() != null)
            {
                Clothe c = g.GetComponent<Clothe>();

                if(!c.is_worn)
                Destroy(g);
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
