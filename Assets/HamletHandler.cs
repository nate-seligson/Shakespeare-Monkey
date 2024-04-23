using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEditor;

public class HamletHandler : MonoBehaviour
{
    [SerializeField]
    TextAsset text;
    [SerializeField]
    TextAsset incomplete;
    public static string[] words;
    public static string[] lines;
    public static string raw;
    public static string rawlower;
    public static string incompletestring;
    public static int wordsFound = 0;
    // Start is called before the first frame update
    void Awake()
    {
        raw = text.ToString();
        rawlower = raw.ToLower();
        words = rawlower.Split(" ");
        incompletestring = incomplete.ToString();
    }

    public static bool InHamlet(string word)
    {
        string[] wordlist = PlayerPrefs.GetString("Words Found").Split("\n");
        return (words.Contains(word) && !wordlist.Contains(word));
    }
    public static string PercentComplete()
    {
        string[] wordlist = PlayerPrefs.GetString("Words Found").Split("\n");
        int wordsToGo = words.Length;
        return wordsFound.ToString() + "/" + wordsToGo.ToString();
    }
    public static string BuildHamlet()
    { 
        string[] wordlist = PlayerPrefs.GetString("Words Found").Split("\n");
        string final = incompletestring; // text file of underscores-- missing words that will be replaced when found
        foreach (string word in wordlist)
        {
            string temp = rawlower; //full string with all lowercase letters
            int startPoint = 0;
            int i = 0;
            while (temp != null && temp.Contains(word) && i<200) {
                int index = temp.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                int finalindex = index + startPoint; //index in complete string, starting from current iteration
                string currentword = raw.Substring(finalindex, word.Length); // word with proper capitalization
                bool isWord = true;
                if (temp[index + word.Length] != ' ' || temp[index - 1] != ' ') //check if its contained within a word
                {
                    isWord = false;
                }
                if (isWord)
                {
                    if (".,-:;".Contains(temp[index + word.Length])) // check if word ends with punctuation
                    {
                        currentword += temp[index + word.Length]; //add any punctuation
                    }
                    wordsFound++;
                    final = final.Remove(finalindex, currentword.Length).Insert(finalindex, currentword); //replace underscores with word
                }
                temp = temp.Remove(0, index + currentword.Length); // remove all text up to that point, to find next index
                startPoint += index + currentword.Length; // set starting index offset
                i++; //iterator to break infinite loops (of which i have not solved)
            }
        }
        return final;
    }
}
