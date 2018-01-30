using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    #region editor variable
    public List<DialogueScriptableObj> dialogues;
    public Image portraitPerso1;
    public Image portraitPerso2;
    public GameObject discutionPanel;

    public GameObject textPrefab;
    public GameObject choicePrefab;

    #endregion

    #region other variable

    int dialogueCurrent = 0;
    DialogueScriptableObj diaCurrent;
    int currentSentences = 0;
    Sentence sentences;
    #endregion

#region monobehaviour methods
    private void Start()
    {
        diaCurrent = dialogues[dialogueCurrent];
        sentences = diaCurrent.sentences[currentSentences++];
        foreach(string sentence in sentences.sentenceList)
        {
            GameObject obj = Instantiate(textPrefab);
            obj.transform.SetParent(discutionPanel.transform);
            Text txt = obj.GetComponent<Text>();
            txt.text = sentence;
            txt.color = diaCurrent.personnage[sentences.player-1].couleurDialogue;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentSentences < diaCurrent.sentences.Count)
        {
            changeSentence();
        }   
    }
    #endregion

    #region other methods

    public void changeSentence()
    {
        
        sentences = diaCurrent.sentences[currentSentences];
        if (!sentences.answer)
        {
            emptyBox();
            for (int i = 0; i < sentences.sentenceList.Count; i++)
            {
                string sentence = sentences.sentenceList[i];
                if(sentences.choice)
                    addChoice(sentence, diaCurrent.sentences[currentSentences +1].sentenceList[i], sentences.player - 1);
                else
                    addSentence(sentence, sentences.player - 1);

            }
            currentSentences++;
        }
        
    }

    public void addSentence(string sentence, int player)
    {
        GameObject obj = Instantiate(textPrefab);
        obj.transform.SetParent(discutionPanel.transform);
        Text txt = obj.GetComponent<Text>();
        txt.text = sentence;
        txt.color = diaCurrent.personnage[player].couleurDialogue;
    }

    public void addChoice(string choice, string answer, int player)
    {
        GameObject obj = Instantiate(choicePrefab);
        obj.transform.SetParent(discutionPanel.transform);
        Text txt = obj.GetComponent<Text>();
        txt.text = choice;
        txt.color = diaCurrent.personnage[player].couleurDialogue;
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(
            delegate {
                addAnswer(answer, diaCurrent.sentences[currentSentences].player-1);
            });
    }

    public void addAnswer(string sentence, int player)
    {
        emptyBox();
        addSentence(sentence, player);
    }

    public void emptyBox()
    {
        foreach (Transform child in discutionPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

#endregion
}
