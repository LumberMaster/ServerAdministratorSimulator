using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractSwitcher : MonoBehaviour
{
	[SerializeField] protected bool isEnable;
	public virtual bool IsEnable
	{
		get =>  isEnable;
		protected set  => isEnable = value;

	}
	public virtual void ToSwitch()
	{
		if (IsEnable) ToDisable();
		else ToEnable();
	}

	public virtual void ToEnable() => IsEnable = true;
	public virtual void ToDisable() => IsEnable = false;
}
