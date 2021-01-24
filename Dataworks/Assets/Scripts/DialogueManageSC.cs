using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class DialogueManageSC : MonoBehaviour
{
    private Queue<string> sentences;

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator DialogueBoxAnimator;
    public TMP_Text ButtonText;

    public bool BeginOnEntry;
    public bool ExitOnCompletion;

    public Animator PointerAnimator;
    private int SentenceIndex = 0;
    private int SentenceCount;


    void Start()
    {
        sentences = new Queue<string>();

        if (BeginOnEntry)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }

    void Update()
    {

        if (SentenceIndex == SentenceCount &&
            (!FindObjectOfType<TrainingWheels>().getPipesState || !FindObjectOfType<TrainingWheels>().getButtonState))
        {
            FindObjectOfType<TrainingWheels>().PipesUnlocked(true);
            FindObjectOfType<TrainingWheels>().ButtonsUnlocked(true);
        }
    }


    public void StartDialogue (Dialogue dialogue)
    {
        DialogueBoxAnimator.SetBool("IsOpen", true);
        nameText.text = dialogue.NPC;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); 
        }

        SentenceCount = sentences.Count;
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {

        if (sentences.Count == 1)
        {
            ButtonText.text = "Finish";
        }
        else if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        SentenceIndex++;

        if(PointerAnimator != null)
            PointerAnimator.SetInteger("currentSentence",SentenceIndex);


    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (!(char.IsWhiteSpace(letter)))
            {
                FindObjectOfType<AudioManager>().Play("DiaType");
            }
            dialogueText.text += letter;
            yield return null;
            FindObjectOfType<AudioManager>().Stop("DiaType");
        }
    }

    void EndDialogue()
    {
        DialogueBoxAnimator.SetBool("IsOpen", false);
        

        if (ExitOnCompletion)
            FindObjectOfType<Level_Loader_Sc>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
