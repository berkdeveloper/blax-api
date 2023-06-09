namespace BlaX.CryptoAutoTrading.Application.ViewModels.BinanceViewModels.WalletViewModels
{
    public class UserAssetListViewModel
    {
        public List<UserAssetsViewModel> UserAssetsViewModels { get; set; }

        public UserAssetListViewModel() => UserAssetsViewModels = new List<UserAssetsViewModel>();
    }
}