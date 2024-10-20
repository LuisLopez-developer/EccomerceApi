using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class StatusPresenter : IPresenter<Status, StatusViewModel>
    {
        public IEnumerable<StatusViewModel> Present(IEnumerable<Status> data)
        {
            var statusViewModels = data.Select(status => new StatusViewModel(status.Id, status.Name)).ToList();
            return statusViewModels;
        }
    }
}
