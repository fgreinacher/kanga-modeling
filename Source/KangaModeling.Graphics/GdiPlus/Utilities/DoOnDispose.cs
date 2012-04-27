using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KangaModeling.Graphics.GdiPlus.Utilities
{
	internal sealed class DoOnDispose : IDisposable
	{
		#region Fields

		private readonly Action m_Do;

		#endregion

		#region Construction

		public DoOnDispose(Action @do)
		{
			m_Do = @do;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			m_Do();
		}

		#endregion
	}
}
