using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class typer : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI txt, wordsfoundtxt, completedtxt, lasttxt, openerText;
    string currentSmash;
    bool spaceToggle = true;
    bool checkDate = true;
    bool debug = false;
    int textLength = 0;
    int wordLength = 0;
    string word = "";
    string[] lines;
    Monkey m;
    // Start is called before the first frame update
    void Start()
    {
        m = new Monkey();
        if (PlayerPrefs.GetString("name").Length < 1)
        {
            m.GenerateName();
            PlayerPrefs.SetString("name", m.Name);
        }
        else
        {
            m.Name = PlayerPrefs.GetString("name");
        }
        //PlayerPrefs.SetString("Words Found", " ");
        currentSmash = m.KeySmash();
        openerText.text = m.Name + " Writes:";
        anim.SetBool("typing", false);
        string ham = HamletHandler.BuildHamlet();
        completedtxt.text = "<b>Completion</b>\n" + HamletHandler.PercentComplete() + "\n" + ham;
    }
    // Update is called once per frame
    void Update()
    {
        wordsfoundtxt.text = "<b>Words Found:</b>" + "\n" + PlayerPrefs.GetString("Words Found");
        lasttxt.text = "<b>Last Text:</b>" + "\n" + PlayerPrefs.GetString("Last Text");
        if (checkDate)
        {
            CheckDate();
            return;
        }
        if (Input.anyKeyDown && textLength < currentSmash.Length)
        {
            string letter = AddLetter();
            anim.SetBool("typing", true);
            if (letter == " ")
            {
                spaceToggle = !spaceToggle;
            }



            if (spaceToggle == true)
            {
                word += letter;
            }
            else
            {
                HandleWord();
            }


            textLength++;
            wordLength++;
        }
        else if(textLength >= currentSmash.Length)
        {
            LogTime();
            anim.SetBool("sleep", true);
            anim.SetBool("typing", false);
        }
        else
        {
            anim.SetBool("typing", false);
            PlayerPrefs.SetString("Last Text", currentSmash);
        }
    }
    void HandleWord()
    {
        if (".,?!".Contains(word[word.Length-1]))
        {
            word = word.Remove(word.Length - 1, 1);
        }
        CheckHamlet();
        Reset();
        spaceToggle = true;
    }
    string AddLetter()
    {
        string letter = currentSmash[textLength].ToString();
        txt.text += letter;
        return letter;
    }
    void Reset()
    {
        word = "";
        wordLength = 0;
    }
    void CheckHamlet()
    {
        if (HamletHandler.InHamlet(word))
        {
            PlayerPrefs.SetString(
                "Words Found",
                PlayerPrefs.GetString("Words Found") + word + "\n"
                );
            txt.text = txt.text.Insert(txt.text.Length - wordLength, "<color=\"red\">");
            txt.text += "</color>";
            completedtxt.text = "<b>Completion</b>\n" + HamletHandler.PercentComplete() + "\n" + HamletHandler.BuildHamlet();
        }
    }
    void LogTime()
    {
        PlayerPrefs.SetInt("day", int.Parse(DateTime.Now.ToString("dd")));
    }
    void CheckDate()
    {
        int Day = int.Parse(DateTime.Now.ToString("dd"));
        int Time = int.Parse(DateTime.Now.ToString("HHmm"));
        int LastDay = PlayerPrefs.GetInt("day");
        if(Day != LastDay || debug)
        {
            checkDate = false;
            anim.SetBool("sleep", false);
            Start();
        }
        else
        {
            anim.SetBool("sleep", true);
            string timeToWait;
            float timeRemaining = -Time;
            if (timeRemaining >= 100)
            {
                timeToWait = ((float)timeRemaining / 100).ToString() + "h";
            }
            else if(timeRemaining < 0)
            {
                timeToWait = ((float)(2400 - Time)/100).ToString() + "h";
            }
            else if(timeRemaining < 100 && timeRemaining > 60)
            {
                timeToWait = ((float)timeRemaining - 40).ToString() + "m";
            }
            else
            {
                timeToWait = timeRemaining.ToString() + "m";
            }
            openerText.text = m.Name + " is sleeping.";
            txt.text = "Time remaining: " + timeToWait;
        }
    }
}
