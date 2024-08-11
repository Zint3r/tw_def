using Game.Features.Actions;
using System;
using Zenject;

namespace Game.Features.MapCreator
{
	public class MapCreatorAction : AbstractAction<MapSelectRequest, MapSelectParams>
	{
		[Inject]
		private IMapCreatorService mapCreatorService;

		public event Action<string> OnComplete;
		public event Action<string> OnFail;

		protected override MapSelectRequest CreateNetworkRequest(MapSelectParams actionParams)
		{
			return new MapSelectRequest { };
		}
		public override bool CanExecute(MapSelectParams actionParams, DateTime timeStamp, out string errorMessage)
		{
			return base.CanExecute(actionParams, timeStamp, out errorMessage);
		}
		protected override void UpdateModel(MapSelectParams actionParams, DateTime timeStamp)
		{
			mapCreatorService.SelectedMap(actionParams.SelectMap);
			messageHubService.Publish(new MapCreatorMessages.MapCreatorMessage(actionParams.SelectMap));
		}
	}
}