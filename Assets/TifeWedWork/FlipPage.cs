using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FlipPage : MonoBehaviour

{
   private enum ButtonType
    {
        NextButton,
        PreButton,
          //  ExitButton
    }

    [SerializeField] Button nextbutton;
     [SerializeField] Button prebutton;
    //[SerializeField] Button exitbutton; 

    private Vector3 rotationVector;
    private Vector3 startPosition;
    private Quaternion startRotation; 

    private bool isClicked;

    private DateTime startTime;
    private DateTime endTime;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;

        if (nextbutton != null)
        {
            nextbutton.onClick.AddListener(() => turnOnePagebutton_Click(ButtonType.NextButton));
        }


        if (prebutton != null)
        {
            prebutton.onClick.AddListener(() => turnOnePagebutton_Click(ButtonType.PreButton));
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);

            endTime = DateTime.Now;
            if((endTime - startTime).TotalSeconds >= 1)
            {
                isClicked = false;
                transform.rotation = startRotation;
                transform.position = startPosition;
            }
        }
    }

    private void turnOnePagebutton_Click(ButtonType type)
    {
        isClicked = true;
        startTime = DateTime.Now;

        if(type == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, 180, 0);
        }
        else if (type == ButtonType.PreButton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.z);
            transform.rotation = Quaternion.Euler(newRotation);

            rotationVector = new Vector3(0, -180, 0);
        }
    }
}
