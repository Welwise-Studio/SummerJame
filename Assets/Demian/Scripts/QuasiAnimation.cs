using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuasiAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] quads;
    [SerializeField] float[] quasiAnimationTimes;
    public float currentTimeForAnimation;
    //[SerializeField] float durationOfAnimation_only_if_random_animation;
    //float currentDurationOfAnimation;

    [SerializeField] bool isRandomFrames;
    [SerializeField] bool isOnlyOneTime;
    [SerializeField] bool isAnimatingNow;

    
    int maxNumPointer;
    [SerializeField] int currentNumPointer;



    private void Awake()
    {
        StartAnimation();

    }


    void Start()
    {
        maxNumPointer = quads.Length;
        currentNumPointer = 0;
        currentTimeForAnimation = quasiAnimationTimes[currentNumPointer];
        MakeQuadsInvisible();

        quads[currentNumPointer].gameObject.SetActive(true);
        //currentDurationOfAnimation = durationOfAnimation_only_if_random_animation;
        
    }

    
    void Update()
    {
        if (isAnimatingNow)
        {
            currentTimeForAnimation -= Time.deltaTime;
            if (currentTimeForAnimation < 0)
            {
                if (isRandomFrames)
                {
                    int veryCurrentNum = Random.Range(0, maxNumPointer);
                    if (veryCurrentNum != currentNumPointer)
                    {
                        currentNumPointer = veryCurrentNum;
                    }
                    else
                    {
                        veryCurrentNum = Random.Range(0, maxNumPointer);
                        if(veryCurrentNum != currentNumPointer)
                        {
                            currentNumPointer = veryCurrentNum;
                        }
                        else
                        {
                            veryCurrentNum = Random.Range(0, maxNumPointer);
                            currentNumPointer = veryCurrentNum;
                        }
                    }

                    /*
                    currentDurationOfAnimation -= Time.deltaTime;
                    if (currentDurationOfAnimation < 0)
                    {
                        StopAnimation();
                    }
                    */


                }
                else
                {
                    
                    if (currentNumPointer < maxNumPointer - 1)
                    {
                        currentNumPointer++;
                    }
                    else
                    {
                        //Debug.Log("currentNumPointer: " + currentNumPointer);
                        //Debug.Log("maxNumPointer: " + maxNumPointer);
                        

                        currentNumPointer = 0;
                        
                        if (isOnlyOneTime)
                        {

                            
                            //Debug.Log(gameObject.name + ':' + currentNumPointer);
                            MakeQuadsInvisible();
                            StopAnimation();
                            return;
                        }
                        
                    }
                    


                }

                MakeQuadsInvisible();
                if (quads[currentNumPointer])
                {
                    quads[currentNumPointer].gameObject.SetActive(true);
                }

                if (quasiAnimationTimes.Length == quads.Length)
                {
                    currentTimeForAnimation = quasiAnimationTimes[currentNumPointer];
                }
                else
                {
                    currentTimeForAnimation = quasiAnimationTimes[0];
                }
                currentTimeForAnimation += Random.Range(-currentTimeForAnimation / 2, currentTimeForAnimation / 2);
            }
        }


        

        
    }





    void MakeQuadsInvisible()
    {
        for (int i = 0; i < quads.Length; i++)
        {
            if (quads[i])
            {
                quads[i].gameObject.SetActive(false);
            }
        }
    }





    public void ChangeRotatingSpeed(float _speed)
    {
        quasiAnimationTimes[0] = _speed;
    }




    public void StartAnimation()
    {
        isAnimatingNow = true;
        currentNumPointer = 0;
        currentTimeForAnimation = quasiAnimationTimes[currentNumPointer];
        MakeQuadsInvisible();

        quads[currentNumPointer].gameObject.SetActive(true);

        //currentDurationOfAnimation = durationOfAnimation_only_if_random_animation;
    }


    public void StopAnimation()
    {
        isAnimatingNow = false;
    }


    public void CanAnimatingNow(bool _canAnimating)
    {
        isAnimatingNow = _canAnimating;
    }
}
