using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAmmoslide : MonoBehaviour
{
    bool dir = false;
    RectTransform rectTransform;
    float speed = 2;

    float keyDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(rectTransform.position.x + " " + rectTransform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F) && keyDelay <= 0)
        {
            Move(dir);
            dir = !dir;
            keyDelay = 0.3f;
        }
        keyDelay -= Time.deltaTime;
    }

    void Move(bool upwards)
    {
        Vector3 destination;
        if(upwards)
        {
            destination = new Vector3(rectTransform.position.x, rectTransform.position.y + 10, rectTransform.position.z);
        }else
        {
            destination = new Vector3(rectTransform.position.x, rectTransform.position.y - 10, rectTransform.position.z);

        }
        Vector3 moveDirection;
        if (Vector2.Distance(rectTransform.position, destination) >= 0.1f)
        {
            moveDirection = destination - rectTransform.position;
            rectTransform.Translate(rectTransform.position + (moveDirection.normalized * speed * Time.deltaTime));
        }
    }
}
