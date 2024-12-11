using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventScript : MonoBehaviour
{
    private DayOneScript dayOne;
    private DayTwoScript dayTwo;
    private DayThreeScript dayThree;
    private DayFourScript dayFour;
    // Start is called before the first frame update
    void Start()
    {
        dayOne = GameObject.Find("ScriptHolder").GetComponent<DayOneScript>();
        dayTwo = GameObject.Find("ScriptHolder").GetComponent<DayTwoScript>();
        dayThree = GameObject.Find("ScriptHolder").GetComponent<DayThreeScript>();
        dayFour = GameObject.Find("ScriptHolder").GetComponent<DayFourScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void runDay(int day)
    {
        switch(day)
        {
            case 1:
                dayOne.run1();
                dayOne.run2();
            break;
            case 2:
                dayTwo.run1();
                dayTwo.run2();
            break;
            case 3:
                dayThree.run1();
                dayThree.run2();
            break;
            case 4:
                dayFour.run1();
                dayFour.run2();
            break;
            case 5:

            break;
            case 6:

            break;
            case 7:

            break;
            case 8:

            break;
            case 9:

            break;
            case 10:

            break;
            case 11:

            break;
            case 12:

            break;
            case 13:

            break;
            case 14:

            break;
            case 15:

            break;
            case 16:

            break;
            case 17:

            break;
            case 18:

            break;
            case 19:

            break;
            case 20:

            break;
            case 21:

            break;
            case 22:

            break;
            case 23:

            break;
            case 24:

            break;
            
        }
    }    
}
