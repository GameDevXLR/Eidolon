using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public static DialogueController instance;

    #region editor variable
    public List<DialogueScriptableObj> dialogues;
    public Image portraitPerso1;
    public Image portraitPerso2;

	public Sprite portraitPerso1SpriteNormal;
	public Sprite portraitPerso1SpriteInactive;

	public Sprite portraitPerso2SpriteNormal;
	public Sprite portraitPerso2SpriteInactive;

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

    private void Awake()
    {
        instance = this;
    }

    //private void Start()
    //{
    //    diaCurrent = dialogues[dialogueCurrent];
    //    changeSentence();
    //}

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentSentences < diaCurrent.sentences.Count)
                changeSentence();
            else if (currentSentences == diaCurrent.sentences.Count)
                currentSentences++;
            else if (currentSentences > diaCurrent.sentences.Count)
            {
                gameObject.SetActive(false);
                GameManager.instance.activePA();
            }
                
        }   
        
    }
    #endregion

    #region other methods

    public void Initialize(int dialogueIndex)
    {

        diaCurrent = dialogues[dialogueIndex];
        changeSentence();
        gameObject.SetActive(true);
    }

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
                    addChoice(sentence, diaCurrent.sentences[currentSentences +1].sentenceList[i], sentences.player);
                else
                    addSentence(sentence, sentences.player - 1);

            }
            setAlphaPortrait(sentences.player);
            currentSentences++;
        }
        
    }

    public void addSentence(string sentence, int player)
    {
        GameObject obj = Instantiate(textPrefab);
        obj.transform.SetParent(discutionPanel.transform);
		obj.transform.localScale = Vector3.one;

		Text txt = obj.GetComponent<Text>();
        txt.text = sentence;
//        txt.color = diaCurrent.personnage[player].couleurDialogue;
    }

    public void addChoice(string choice, string answer, int player)
    {
        GameObject obj = Instantiate(choicePrefab);
        obj.transform.SetParent(discutionPanel.transform);
		obj.transform.localScale = Vector3.one;
        Text txt = obj.GetComponent<Text>();
        txt.text = choice;
//        txt.color = diaCurrent.personnage[player-1].couleurDialogue;
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(
            delegate {
                addAnswer(answer, diaCurrent.sentences[currentSentences].player);
            });
    }

    public void addAnswer(string sentence, int player)
    {
        emptyBox();
        addSentence(sentence, player-1);
        setAlphaPortrait(player);
        currentSentences++;
    }

    public void emptyBox()
    {
        foreach (Transform child in discutionPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setAlphaPortrait(int perso)
    {
		if (perso == 1) 
		{
			portraitPerso1.sprite = portraitPerso1SpriteNormal;
			portraitPerso2.sprite = portraitPerso2SpriteInactive;
		} else 
		{
			portraitPerso1.sprite = portraitPerso1SpriteInactive;
			portraitPerso2.sprite = portraitPerso2SpriteNormal;
		}
//        if(perso == 1)
//        {
//            Color c = portraitPerso1.color;
//            c.a = 1f;
//            portraitPerso1.color = c;
//            Color c2 = portraitPerso2.color;
//            c2.a = 0.2f;
//            portraitPerso2.color = c2;
//        }
//        else if(perso == 2)
//        {
//            Color c = portraitPerso1.color;
//            c.a = 0.2f;
//            portraitPerso1.color = c;
//            c = portraitPerso2.color;
//            c.a = 1f;
//            portraitPerso2.color = c;
//        }

    }

#endregion
}
