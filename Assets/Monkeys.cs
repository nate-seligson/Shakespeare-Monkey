using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monkey
{
    string vowels = "aeiou";
    string consonants = "bcdfghjklmnpqrstvwxyz";
    string name = "";
    public string Name
    {
        get{
            string first_letter = name[0].ToString();
            string name_local = name;
            name_local = name_local.Remove(0, 1);
            name_local = name_local.Insert(0, first_letter.ToUpper());
            return name_local;
        }
        set{
            name = value;
        }
    }
    public void GenerateName()
    {
        for (var i = 1; i < 5; i++)
        {
            string letter;
            if (i % 2 == 0)
            {
                letter = vowels[Random.Range(0, vowels.Length - 1)].ToString();
            }
            else
            {
                letter = consonants[Random.Range(0, consonants.Length - 1)].ToString();
            }
            name += letter;
        }
    }
    public string KeySmash()
    {
        string alphabet = vowels + consonants;
        string punctuation = ".,?!";
        string text = "";
        int sentences = Random.Range(10,20);
        for(var sentence_index = 0; sentence_index<sentences; sentence_index++)
        {
            string sentence = "";
            int max_words = 15;
            int words = Random.Range(1, max_words);
            for(var word_index = 0; word_index<words; word_index++)
            {
                string word = "";
                int max_letters = 15;
                int letters = Random.Range(1, max_letters);
                for(var letter_index = 0; letter_index<letters; letter_index++)
                {
                    string letter = alphabet[Random.Range(0, 26)].ToString(); // amt of letters in alphabet
                    word += letter;
                }
                sentence += word + " ";

            }
            text += sentence.Remove(sentence.Length - 1,1) + punctuation[Random.Range(0,punctuation.Length)].ToString() + " ";
        }
        return text;
    }

}
