using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Covid19.ViewModels.Base;
using Covid19.Infrastructure.Commands;
using Covid19.Models;

namespace Covid19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Test DataPoints
        private IEnumerable<DataPoint> _testDataPoints;
        /// <summary>Test Data points</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }
        #endregion

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

        #region Selected Page Index
        private int _selectedPageIndex = 0;
        /// <summary>Window Title</summary>
        public int SelectedPageIndex
        {
            get => _selectedPageIndex;
            set => Set(ref _selectedPageIndex, value);
        }
        #endregion

        #region Tabs Count
        private int _tabsCount;
        /// <summary>Window Title</summary>
        public int TabsCount
        {
            get => _tabsCount;
            set => Set(ref _tabsCount, value);
        }
        #endregion

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

        #region ChangePageCommand
        public ICommand ChangePageCommand { get; set; }
        private bool CanChangePageCommandExecute(object p) => _selectedPageIndex>=0;

        private void OnChangePageCommandExecuted(object p)
        {
            var index = Int32.Parse(p.ToString());
            if(SelectedPageIndex>=0)
                SelectedPageIndex += Int32.Parse(p.ToString());
        }
        #endregion


        #endregion


        public MainWindowViewModel()
        {
            #region Commands

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            ChangePageCommand = new LambdaCommand(OnChangePageCommandExecuted, CanChangePageCommandExecute);

            #endregion

            const double rad = Math.PI / 180;
            List<DataPoint> points = new List<DataPoint>((int)(360/0.1));
            for(var x = 0d; x <= 360; x += 0.1)
            {
                var y = Math.Sin(2 * Math.PI * x * rad);
                points.Add(new DataPoint { X = x, Y = y });
            }

            TestDataPoints = points;
        }
    }
}
