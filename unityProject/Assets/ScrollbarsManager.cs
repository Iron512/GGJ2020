using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarsManager : MonoBehaviour
{
	private float lastModification= -1;
	private int lastModifiedProgressbar = -1;
	public Scrollbar[] sliders = new Scrollbar[5];
	public Toggle[] locks = new Toggle[5];

	public void updateScrollbars(int updatedIndex)
	{
		// perdonatemi per questo fix, non vi era altro modo 
		if (lastModifiedProgressbar != updatedIndex && Time.time - lastModification < 0.5f)
		{
			return;
		}

		lastModification = Time.time;
		lastModifiedProgressbar = updatedIndex;
		float sum = getSum();
		float lockedSum = this.getSum(skipUnlocked:true);
		float lastValueSet = sliders[updatedIndex].value;

		float currentSumValue = this.getSum(skipLoked: true);
		double targetSumValue = 1 - lockedSum;
		
		for(int i=0;i<5;i++){
			if (!locks[i].isOn)
			{
				continue;
			}
			if (updatedIndex==i)
			{
				if (sliders[i].value > targetSumValue)
				{
					sliders[i].value = (float)targetSumValue;
				}
				continue;
			}

			float v;
			if (currentSumValue != 0)
			{
				 v = (float) (sliders[i].value * ((double) targetSumValue / (double) currentSumValue));
			}
			else
			{
				v = 0;
			}

			sliders[i].value = Mathf.Max(0,Mathf.Min(1,v));
		}
	}

	public float getSum(bool skipLoked= false,bool skipUnlocked = false)
	{
		float sum = 0;
		for(int i=0;i<5;i++)
		{
			if ((!locks[i].isOn && skipLoked)||(locks[i].isOn && skipUnlocked))
			{
				continue;
			}
			sum += sliders[i].value;
		}

		return sum;
	}

	public void toggleProgressbar(int index)
	{
		sliders[index].enabled = locks[index].isOn;
	}
	
}
