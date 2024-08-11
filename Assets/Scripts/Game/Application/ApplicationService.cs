namespace Game.Application
{
	public class ApplicationService : IApplicationService
	{
		public string GetAppVersion()
		{
			return UnityEngine.Application.version;
		}
	}
}