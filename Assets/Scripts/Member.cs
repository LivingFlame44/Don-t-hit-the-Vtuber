using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Member : MonoBehaviour
{
    public float speed;
    public bool goLeft;

    public Sprite memPic;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        switch (goLeft)
        {
            case true: transform.position += Vector3.left * speed * Time.deltaTime;
                break;
            case false: transform.position += Vector3.right * speed * Time.deltaTime;
                break;
        }
        
    }

    private void OnEnable()
    {
        ChangeSprite();
    }
    private void OnBecameInvisible()
    {
        GameManager.instance.score++;
        GameManager.instance.BackToPool(this.gameObject);
        this.gameObject.SetActive(false);

        GameManager.instance.inactivePics.Add(memPic);
        GameManager.instance.activePics.Remove(memPic);
    }

    private void OnDisable()
    {
        
    }
    public void ChangeSprite()
    {
        int picNum = Random.Range(0, GameManager.instance.inactivePics.Count);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.inactivePics[picNum];
        memPic = gameObject.GetComponent<SpriteRenderer>().sprite;

        GameManager.instance.activePics.Add(memPic);
        GameManager.instance.inactivePics.Remove(memPic);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();

        }
    }
}
