﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.DataBase;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;
using WpfDormitories.DataBase.Entity.Contract;

namespace WpfDormitories.ViewModel.ResidentsVM
{
    public class ResidentsViewModel : BasicVM
    {
        private ITableService _tableService;
        private string _contractInfo;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private IUserAbilitiesData _userAbilitiesData;


        public bool DeleteConfirmStatus { get; set; }

        public Action<IUserAbilitiesData, DataRow> OnChildren;
        public Action<DataRow> OnEdit;
        public Action<uint> OnAdd;
        public Action OnDelete;

        public string ContractInfo
        {
            get { return _contractInfo; }
            set
            {
                Set(ref _contractInfo, value);
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
            }
        }

        public DataTable Table
        {
            get { return _table; }
            set
            {
                Set(ref _table, value);
            }
        }

        public string FindText
        {
            get { return _findText; }
            set
            {
                Set(ref _findText, value);
            }
        }

        public Visibility W
        {
            get { return _userAbilitiesData.W ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility E
        {
            get { return _userAbilitiesData.E ? Visibility.Visible : Visibility.Collapsed; ; }
        }

        public Visibility D
        {
            get { return _userAbilitiesData.D ? Visibility.Visible : Visibility.Collapsed; ; }
        }

        public ResidentsViewModel(IUserAbilitiesData userAbilities, uint contractId)
        {
            _selectedIndex = -1;
            _userAbilitiesData = userAbilities;
            List<IContractData> contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();
            IContractData contract = contracts.Find(item => item.Id == contractId);
            _contractInfo = $"Контракт №{contract.DocumentNumber}, {contract.Name}, {contract.StartAction.ToString("yyyy-MM-dd")}";
            DeleteConfirmStatus = false;
            _tableService = new ResidentsTableService(contractId);

            _table = _tableService.Read();

            _tableService.OnEdit += (dataRow) => { OnEdit?.Invoke(dataRow); };
            _tableService.OnAdd += () => { OnAdd?.Invoke(contractId); };
        }

        public void UpdateTable()
        {
            Table = _tableService.Read();
        }

        public ICommand Delete
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnDelete?.Invoke();
                        if (DeleteConfirmStatus)
                        {
                            _tableService.Delete(SelectedIndex);
                            Table = _tableService.Read();
                            DeleteConfirmStatus = false;
                        }
                    }

                });

            }
        }

        public ICommand FindAll
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Table = _tableService.FindAll(FindText);
                });

            }
        }

        public ICommand Edit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        _tableService.Edit(SelectedIndex);
                    }
                });

            }
        }

        public ICommand Add
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _tableService.Add();
                });

            }
        }

        public ICommand Inventory
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnChildren?.Invoke(_userAbilitiesData, _tableService.GetByIndex(SelectedIndex));
                    }
                });

            }
        }
    }
}
