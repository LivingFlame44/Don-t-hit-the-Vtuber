using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vtuber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(0, BounceManager.instance.vtuberPics.Length);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = BounceManager.instance.vtuberPics[randomNum];
    }

    // Update is called once per frame
    void Update()
    {
        if(BounceManager.instance.player.transform.position.z > 0)
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BounceManager.instance.GameOver();
        }
    }
}
