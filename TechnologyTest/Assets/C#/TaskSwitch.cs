using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSwitch : MonoBehaviour
{
   [SerializeField] private Text explanationText;

   [SerializeField] private GameObject instractor;
   
   private const string aButtonClick = "\n右手のAボタンクリックで進む";
   
   private List<String> beforeExplanationTextList = new List<string>()
   {
      "本実験では、VR仮想空間における仮想身体の動きの精度について実験します" + aButtonClick,
      "まず、自身の身体位置を中央にリセットします" + aButtonClick,
      "右手下のボタンを押した後、正面をリセットボタンを選択してください" + aButtonClick,
      "身体位置が適切な場所に設定できているか、実験主導者に確認をとってください" + aButtonClick,
      "身体位置の確認が取れましたら、実験を開始します" + aButtonClick,
      "では、実験Aを行う方は右トリガーを、実験Bを行う方は左トリガーを引いてください"
   };
   
   private List<String> AExperimentTextList = new List<string>()
   {
      "実験Aでは、身体を一定時間自由に動かしてもらった後、タスクを行います" + aButtonClick,
      "全身を" + MusicStartAndSceneChange.musicTime.ToString() + "秒間の間自由に動かし、仮想身体の動きを把握してください" + aButtonClick,
      MusicStartAndSceneChange.musicTime.ToString() + "秒経過後に、自動でタスクを行う画面に遷移します" + aButtonClick,
      "それでは、右手のAボタンを押した後、身体を自由に動かしてください" + aButtonClick
   };

   private List<String> BExperimentTextList = new List<string>()
   {
      "実験Bでは、ラジオ体操の音楽に合わせて体操を行った後、タスクを行います" + aButtonClick,
      "体操の動作は、正面に現れるインストラクターの動きを真似し、行ってください" + aButtonClick,
      "体操中は、なるべく自分の身体の動きに目を向けてください" + aButtonClick,
      "体操方法の補助音声として、各体操ごとにナレーションが流れます" + aButtonClick,
      "体操終了後、自動でタスクを行う画面に遷移します" + aButtonClick,
      "それでは、右手のAボタンを押した後、体操を始めます" + aButtonClick
   };
  
   private int readExplanationCount;
   
   private ExplanationStatus explanationStatus;

   private enum ExplanationStatus
   {
      Before,
      A,
      B
   }

   private void Start()
   {
      readExplanationCount = 0;
      explanationStatus = ExplanationStatus.Before;
      explanationText.text = beforeExplanationTextList[readExplanationCount];
   }

   private void Update()
   {
      if (explanationStatus == ExplanationStatus.Before)
      {
         if (readExplanationCount < beforeExplanationTextList.Count - 1)
         {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
               readExplanationCount++;
               explanationText.text = beforeExplanationTextList[readExplanationCount];
            }
         }
         else if (readExplanationCount == beforeExplanationTextList.Count - 1)
         {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
               explanationStatus = ExplanationStatus.A;
               readExplanationCount = 0;
               explanationText.text = AExperimentTextList[readExplanationCount];
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
               explanationStatus = ExplanationStatus.B;
               readExplanationCount = 0;
               explanationText.text = BExperimentTextList[readExplanationCount];
            }
         }
      }
      else if (explanationStatus == ExplanationStatus.A)
      {
         if (readExplanationCount < AExperimentTextList.Count - 1)
         {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
               readExplanationCount++;
               explanationText.text = AExperimentTextList[readExplanationCount];
            }
         }
         else if (readExplanationCount == AExperimentTextList.Count - 1)
         {
            explanationText.text = "";
            instractor.GetComponent<InstractorMove>().ExerciseStart(false); 
         }
      }
      else if (explanationStatus == ExplanationStatus.B)
      {
         if (readExplanationCount < AExperimentTextList.Count - 1)
         {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
               readExplanationCount++;
               explanationText.text = BExperimentTextList[readExplanationCount];
            }
         }
         else if (readExplanationCount == AExperimentTextList.Count - 1)
         {
            explanationText.text = "";
            instractor.GetComponent<InstractorMove>().ExerciseStart(true);
         }
      }
   }
}
