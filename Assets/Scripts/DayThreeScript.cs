using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using System.Globalization;
using Unity.VisualScripting;

public class DayThreeScript : MonoBehaviour
{
    static readonly string inputFile = "Inputs/input3.txt";
    string input;
    public void run1()
    {
        readFiles();
        string pattern = @"mul\([0-9]{1,3}\,[0-9]{1,3}\)";
        string enabler = @"do()";
        string disabler = @"don't()";
        string numbers = @"[0-9]{1,3}\,[0-9]{1,3}";
        int counter = 0;
        int finalInt = 0;
        List<int> multiplyMap = new List<int>();
        MatchCollection matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {            
            string literal = match.ToString();
            MatchCollection numberMatches = Regex.Matches(literal, numbers);            
            foreach (Match numMatch in numberMatches)
            {
                string literalNumbers = numMatch.ToString();
                string[] nums = literalNumbers.Split(char.Parse(","));
                multiplyMap.Add(int.Parse(nums[0]) * int.Parse(nums[1]));
            }
            counter = counter + 1;
        }        
        for(int i = 0; i < multiplyMap.Count; i++)
        {
            finalInt = finalInt + multiplyMap[i];
        }
        Debug.Log(finalInt);
    }

    public void run2()
    {

    }

    void readFiles()
    {
        input = File.ReadAllText(inputFile);
        //Debug.Log(input);
    }
}
