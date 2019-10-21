using HCFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    public BulletType type;
    bool selected=false;
    Vector3 addition;
    private void OnMouseDown()
    {
        if (!selected)
        {
            this.gameObject.transform.localScale = gameObject.transform.localScale +addition;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + .05f, this.gameObject.transform.position.z);
        }
        else
        {
            this.gameObject.transform.localScale = gameObject.transform.localScale - addition;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y-.05f, this.gameObject.transform.position.z);
        }
            selected = !selected;
        Utils.EventAsync(new ObjectClickedEvent(type, selected));
    }
  

    // Start is called before the first frame update
    void Start()
    {
        addition = gameObject.transform.localScale * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
