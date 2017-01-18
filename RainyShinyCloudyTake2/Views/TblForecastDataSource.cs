using System;
using UIKit;

namespace RainyShinyCloudyTake2
{
	public class TblForecastDataSource: UITableViewDataSource
	{
		public TblForecastDataSource()
		{
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return 10;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("CellForecast", indexPath);

			return cell;
		}
	}
}
