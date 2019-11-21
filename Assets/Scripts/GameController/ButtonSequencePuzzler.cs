using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSequencePuzzler : MonoBehaviour
{
    private int buttonsAmount = 0;
    private int cooldown = 0;
    private int timeToComplete = 0;
    private bool randomSequence = false;
    private List<int> buttonSequence;

    private int nextButtonPos = -1;
    private int sequenceCounter = 0;

    public PuzzleRoomButtonSequenceData data;

    [SerializeField] private List<Button> puzzleButtons;
    [SerializeField] private List<Image> puzzleLight;

    public void SetPuzzle(PuzzleRoomButtonSequenceData data)
    {
        this.buttonsAmount = data.buttonSequence.Count;
        this.cooldown = data.cooldown;
        this.timeToComplete = data.timeToComplete;
        this.randomSequence = data.randomSequence;
        this.buttonSequence = data.buttonSequence;

        init();
    }

    private void init()
    {
        for(int i = 0; i < buttonsAmount; i++)
        {
            var index = buttonSequence[i];
            puzzleButtons[index].gameObject.SetActive(true);
            puzzleLight[index].gameObject.SetActive(true);
        }
    }

    void Start()
    {
        SetPuzzle(data);
    }

    public void CheckSequencePos(Button btn)
    {
        if(sequenceCounter >= buttonsAmount) return; //finish

        if(sequenceCounter > 0)
        {
            for(int i = 0; i < buttonsAmount; i++)
            {
                if(puzzleButtons[i].Equals(btn))
                {
                    if(i == buttonSequence[nextButtonPos])
                    {
                        puzzleLight[nextButtonPos].color = Color.green;
                        nextButtonPos++;
                        nextButtonPos %= buttonSequence.Count;
                        sequenceCounter++;
                    } 
                    else 
                    {
                        SequenceFail(i);
                    }
                }
            }
        }
        else 
        {
            var indexOfPressedBtn = 0;
            for(int i = 0; i < buttonsAmount; i++)
            {
                if(puzzleButtons[i].Equals(btn))
                {
                    indexOfPressedBtn = i;

                    for(int j = 0; j < buttonSequence.Count; j++)
                    {
                        if(buttonSequence[j].Equals(indexOfPressedBtn))
                        {
                            nextButtonPos = j+1;
                            nextButtonPos %= buttonSequence.Count;
                            puzzleLight[j].color = Color.green;
                            sequenceCounter=1;
                        }
                    }
                }
            }
        }

        Debug.Log($"Sequence: {sequenceCounter}");
    }

    private void SequenceFail(int lastTryIndex)
    {
        sequenceCounter = 0;
        foreach(Image btn in puzzleLight)
        {
            btn.color = Color.white;
        }

        for(int i = 0; i < buttonsAmount; i++)
        {
            if(buttonSequence[i].Equals(lastTryIndex))
            {
                puzzleLight[i].color = Color.red;
            }
        }
    }

    private void CheckButtonInSequence(int btnIndex)
    {
        if(nextButtonPos.Equals(-1) || nextButtonPos.Equals(btnIndex + 1)) 
        {    
            nextButtonPos = btnIndex;
            puzzleLight[btnIndex].enabled = true;
            //prosseguir o jogo
        }
        else 
        {
            nextButtonPos = -1;
            
        }
    }
}