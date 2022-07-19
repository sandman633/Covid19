using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_App.ViewModels.Base;

namespace wpf_App.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Window Title
        private string _title;
        /// <summary>Window Title</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        } 
        #endregion



    }
}
