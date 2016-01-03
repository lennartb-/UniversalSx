using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using UniversalSx.Annotations;

namespace UniversalSx
{
    [ImplementPropertyChanged]
    internal class MainVm
    {
        public int UserId { get; set; }

        public ObservableCollection<StackExchangeUser> Users { get; set; }

        public bool IsUserIdEntered => UserId >0;

        public RelayCommand FetchUserInformation
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var sxr = new StackExchangeApi(StackExchangeApi.PublicApiKey);
                    var user = sxr.GetAssociatedUsers(UserId);

                    Users = new ObservableCollection<StackExchangeUser>(await user);
                });
            }
        }
    }
}
