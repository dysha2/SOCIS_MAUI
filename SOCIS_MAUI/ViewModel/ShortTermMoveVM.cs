using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpfulProjectCSharp.ASP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.ViewModel
{
    public partial class ShortTermMoveVM : BaseVM
    {
        private ICollection<ShortTermMove> _ActiveMoves;
        private const int _pageSize = 10;
        [ObservableProperty]
        private int _currentPage = 0;
        [ObservableProperty]
        private int _pages;

        private AccountingUnit _Unit;
        private Place _Place;
        [ObservableProperty]
        private ObservableCollection<ShortTermMove> _ShortTermMoves;
        private bool _IsActiveOnly = true;
        public ShortTermMoveVM(INavigationService navigationService, MainModel MainModel, DisplayService displayService, DataTransferService dataTransferService) : base(navigationService, MainModel, displayService, dataTransferService)
        {
            _Unit = _dataTransferService.TransferAccountingUnit;
            _dataTransferService.TransferAccountingUnit = null;
            _Place = _dataTransferService.TransferPlace;
            _dataTransferService.TransferPlace = null;
            if (_Unit is not null)
            {
                IsUnitMode = true;
            }
            else
            {
                IsUnitMode = false;
            }
        }
        public bool IsActiveOnly
        {
            get => _IsActiveOnly;
            set
             {
                _IsActiveOnly = value;
                OnPropertyChanged();
                CurrentPage = 0;
                UpdateCollection();
            }
        }
        public bool IsUnitMode { get; private set; }
        [RelayCommand]
        private void NextPage()
        {
            if (CurrentPage < Pages)
            {
                CurrentPage++;
                UpdateCollection();
            }
        }
        [RelayCommand]
        private void PreviousPage()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                UpdateCollection();
            }
        }
        [RelayCommand]
        private void ShortTermMoveUpdate(ShortTermMove ShortTermMove)
        {
            _dataTransferService.TransferShortTermMove = ShortTermMove;
            _navigationService.NavigateToAsync(nameof(ShortTermMoveEditPage));
        }
        [RelayCommand]
        private void AddShortTermMove()
        {
            if (_Unit.ShortTermMoves.Where(x => x.DateTimeEndFact == null).Count() == 0)
            {
                _dataTransferService.TransferShortTermMove = new ShortTermMove() { Unit = _Unit, UnitId = _Unit.Id };
                _navigationService.NavigateToAsync(nameof(ShortTermMoveEditPage));
            }
            else
            {
               _displayService.ShowAlert("Ошибка","Чтобы добавить новое перемещение, для начала завершите старое");
            }
        }
        [RelayCommand]
        private void Appearing()
        {
            try
            {
                if (_Place is not null)
                {
                    _Place.ShortTermMoves = _mainModel.GetAllShortTermMoveByPlace(_Place.Id);
                }
                else
                {
                    if (_Unit is not null)
                    {
                        _Unit.ShortTermMoves = _mainModel.GetAllShortTermMoveByUnit(_Unit.Id);
                    }
                    else
                    {
                        _ActiveMoves = _mainModel.GetAllShortTermMovesActive();
                    }
                }
               
                UpdateCollection();
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
        private void UpdateCollection()
        {
            try
            {
                ICollection<ShortTermMove> shortTermMoves;
                if (_Place is not null)
                {
                    shortTermMoves = _Place.ShortTermMoves;
                }
                else
                {
                    if (_Unit is not null)
                    {
                        shortTermMoves = _Unit.ShortTermMoves;
                    }
                    else
                    {
                        shortTermMoves = _ActiveMoves;
                    }
                }
                if (IsActiveOnly) shortTermMoves = shortTermMoves.Where(y => y.DateTimeEndFact == null).ToList();
                shortTermMoves = shortTermMoves.OrderBy(x => x.DateTimeStart).ToList();
                Pages = shortTermMoves.Count() % _pageSize == 0 ? shortTermMoves.Count() / _pageSize - 1 : shortTermMoves.Count() / _pageSize;
                ShortTermMoves = shortTermMoves
                    .Skip(CurrentPage * _pageSize)
                    .Take(_pageSize)
                    .ToObservableCollection();
                OnPropertyChanged(nameof(ShortTermMoves));
            }
            catch (Exception ex)
            {
                Errors = ValidateAndErrorsTools.GetInfo(ex);
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
