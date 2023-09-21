using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OpenBook : MonoBehaviour
{
    [SerializeField] Button openbutton;

    [SerializeField] GameObject openedBook;
    [SerializeField] GameObject insideBackCover;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip openBook;

    private Vector3 rotationVector;

    private bool isOpneClicked;

    private DateTime startTime;
    private DateTime endTime; 

    // Start is called before the first frame update
    void Start()
    {
        if(openbutton != null)
        {
            openbutton.onClick.AddListener(() => openbutton_Click());
        }

       // AppEvents.CloseBook += new EventHandler(closeBook_Click);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpneClicked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if((endTime - startTime).TotalSeconds >= 1)
            {
                isOpneClicked = false;

                gameObject.SetActive(false);
                //insideBackCover.SetActive(false);
                openedBook.SetActive(true);
            }

            
        } 
    }

    private void openbutton_Click()
    {
        isOpneClicked = true;
        startTime = DateTime.Now;
        rotationVector = new Vector3(0, 180, 0);

        PlaySound();
    }
     
    public  void PlaySound()
    {
       if((audioSource != null) && (openedBook != null))
        {
            audioSource.PlayOneShot(openBook);
        }
    }

    //private void closeBook_Click (object sender, EventArgs e)
    //{
    //    Debug.Log("close book clicked in openbook");
    //}
}
