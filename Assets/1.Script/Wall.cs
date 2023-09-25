using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public int n_hp = 1;
    // Start is called before the first frame update
    void Start()
    {
        bEngageOn = true;
        fEngageTime =0;
    }

    float fEngageTime;
    const float fFullEngageTime = 3f;
    bool bEngageOn;

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.CompareTag("wall_good"))
        {

           
            if (bEngageOn == false)
            {
                this.GetComponent<Collider2D>().enabled = false;
               // this.gameObject.GetComponent<Wall>().enabled = false;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0.1f, 0.1f, 0.3f);
                fEngageTime += Time.deltaTime;
                if (fEngageTime > fFullEngageTime)
                {
                    bEngageOn = true;
                    fEngageTime = 0; // Reset the engage time
                    n_hp += 1; // Increase the hp as the wall is charged
                }

            }
            else {
                this.GetComponent<Collider2D>().enabled = true;
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
           //     this.gameObject.GetComponent<Wall>().enabled = true;

            }






        }

        if (n_hp <= 0)
        {

           if(this.gameObject.CompareTag("wall_good"))
            {
                bEngageOn = false;
            }

                else

            Object.Destroy(this.gameObject);
        }



    }
}
