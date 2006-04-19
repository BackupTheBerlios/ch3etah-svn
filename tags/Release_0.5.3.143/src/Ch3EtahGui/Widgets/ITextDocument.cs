using System;

namespace Ch3Etah.Gui.Widgets
{
	public interface ITextDocument
	{
		string GetText();
		int GetSelectionOffset();
		int GetSelectionLength();
		void SetSelection(int offset, int length);
		//void ReplaceText(int start, int length, string newText);
	}
}
