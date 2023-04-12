using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour
{   
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((isRight? Vector3.right : Vector3.left) * 5f * Time.deltaTime);
    }

    public void SetIsRight(bool isRight){
        this.isRight = isRight;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "trai")
        {   
            //kill dan
            Destroy(gameObject);    
            var name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
        }
    }
}
