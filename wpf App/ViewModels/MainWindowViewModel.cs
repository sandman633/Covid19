using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Covid19.ViewModels.Base;
using Covid19.Infrastructure.Commands;

namespace Covid19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Window Title
        private string _title = "Анализ статистики";
        /// <summary>Window Title</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Status programm
        private string _status = "Готов";
        /// <summary>Window Title</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }
        #endregion

        #region Commands

        #region CloseAppCommand
        public ICommand CloseAppCommand { get; set; }
        private bool CanCloseAppCommandExecute(object p) => true;

        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion


        #endregion

        public MainWindowViewModel()
        {
            #region Commands

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);

            #endregion
        }
    }
}
