using System.Collections.Generic;
using SRF.Service;

namespace SRDebugger.Services.Implementation
{
	public class KeyboardShortcutListenerService : SRServiceBase<KeyboardShortcutListenerService>
	{
		private List<Settings.KeyboardShortcut> _shortcuts;

		protected override void Awake()
		{
		}

		private void ToggleTab(DefaultTabs t)
		{
		}

		private void ExecuteShortcut(Settings.KeyboardShortcut shortcut)
		{
		}

		protected override void Update()
		{
		}

		public KeyboardShortcutListenerService()
		{
			((SRServiceBase<T>)(object)this)._002Ector();
		}
	}
}
