using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoop
{
	internal class GravSystem
	{
		private GravObject[] systemState0 = { };
		private GravObject[] systemState1 = { };

		private bool activeState = false;

		private GravObject[] GetActiveState()
		{
			if (activeState)
			{
				return systemState1;
			}
			else
			{
				return systemState0;
			}
		}

		private void SwapStates()
		{
			activeState = !activeState;
		}

		GravObject test;
	}
}
