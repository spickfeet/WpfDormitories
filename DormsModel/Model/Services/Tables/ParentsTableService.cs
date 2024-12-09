﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{ 
    public class ParentsTableService : ITableService
    {
        private List<IResidentData> _residents;
        private List<IRoomData> _rooms;
        private List<IDormData> _dorms;
        private List<IStreetData> _streets;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public List<IResidentData> Parents
        {
            get { return _residents; }
        }

        public ParentsTableService()
        {
            _dorms = DataManager.GetInstance().DormsRepository.Read().ToList();
            _streets = DataManager.GetInstance().StreetsRepository.Read().ToList();
            _rooms = DataManager.GetInstance().RoomsRepository.Read().ToList();
        }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IResidentData>(_residents).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }
            text = text.ToUpper();

            List<IResidentData> allResidents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            DataTable res = CreateDataTable();

            _residents = new();

            foreach (IResidentData resident in allResidents)
            {
                if (resident.HaveChildren == true)
                {
                    IRoomData room = _rooms.Find(item => item.Id == resident.RoomId);
                    IDormData dorm = _dorms.Find(item => item.Id == room.DormId);

                    if ($"Общежитие №{dorm.DormNumber}, {_streets.Find(item => item.Id == dorm.StreetId).Name}".ToUpper().Contains(text) ||
                        room.NumberRoom.ToUpper().Contains(text) ||
                        resident.PersonDocuments.RegistrationNumber.Contains(text) ||
                        resident.PersonDocuments.Passport.FullName.Name.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.FullName.Surname.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.FullName.Patronymic.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.Gender.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.Series.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.Number.ToUpper().Contains(text) ||
                        resident.PersonDocuments.Passport.WhoGave.ToUpper().Contains(text) ||
                        resident.PlaceOfWork.ToUpper().Contains(text) ||
                        resident.PlaceOfStudy.ToUpper().Contains(text))
                    {
                        _residents.Add(resident);

                        res.Rows.Add(($"Общежитие №{dorm.DormNumber}, {_streets.Find(item => item.Id == dorm.StreetId).Name}"),
                            room.NumberRoom,
                            resident.PersonDocuments.RegistrationNumber,
                            resident.PersonDocuments.Passport.FullName.Surname,
                            resident.PersonDocuments.Passport.FullName.Name,
                            resident.PersonDocuments.Passport.FullName.Patronymic,
                            resident.PersonDocuments.Passport.Gender,
                            resident.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd"),
                            resident.PersonDocuments.Passport.Series,
                            resident.PersonDocuments.Passport.Number,
                            resident.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd"),
                            resident.PersonDocuments.Passport.WhoGave,
                            string.IsNullOrEmpty(resident.PlaceOfWork) ? "Нет" : "Да",
                            string.IsNullOrEmpty(resident.PlaceOfStudy) ? "Нет" : "Да",
                            resident.HaveChildren ? "Да" : "Нет",
                            resident.ArrivalDate.ToString("yyyy-MM-dd"),
                            resident.Payment,
                            resident.PlaceOfWork,
                            resident.PlaceOfStudy);
                    }
                }
            }
            return res;
        }

        public DataTable Read()
        {
            List<IResidentData> allResidents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            DataTable res = CreateDataTable();

            _residents = new();

            foreach (IResidentData resident in allResidents)
            {
                if (resident.HaveChildren == true)
                {
                    _residents.Add(resident);

                    IRoomData room = _rooms.Find(item => item.Id == resident.RoomId);
                    IDormData dorm = _dorms.Find(item => item.Id == room.DormId);
                    res.Rows.Add(($"Общежитие №{dorm.DormNumber}, {_streets.Find(item => item.Id == dorm.StreetId).Name}"),
                        room.NumberRoom,
                        resident.PersonDocuments.RegistrationNumber,
                        resident.PersonDocuments.Passport.FullName.Surname,
                        resident.PersonDocuments.Passport.FullName.Name,
                        resident.PersonDocuments.Passport.FullName.Patronymic,
                        resident.PersonDocuments.Passport.Gender,
                        resident.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd"),
                        resident.PersonDocuments.Passport.Series,
                        resident.PersonDocuments.Passport.Number,
                        resident.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd"),
                        resident.PersonDocuments.Passport.WhoGave,
                        string.IsNullOrEmpty(resident.PlaceOfWork) ? "Нет" : "Да",
                        string.IsNullOrEmpty(resident.PlaceOfStudy) ? "Нет" : "Да",
                        resident.HaveChildren ? "Да" : "Нет",
                        resident.ArrivalDate.ToString("yyyy-MM-dd"),
                        resident.Payment,
                        resident.PlaceOfWork,
                        resident.PlaceOfStudy);
                }

            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Общежитие");
            res.Columns.Add("Номер комнаты");
            res.Columns.Add("Регистрационный номер");
            res.Columns.Add("Фамилия");
            res.Columns.Add("Имя");
            res.Columns.Add("Отчество");
            res.Columns.Add("Пол");
            res.Columns.Add("Дата рождения");
            res.Columns.Add("Серия паспорта");
            res.Columns.Add("Номер паспорта");
            res.Columns.Add("Дата выдачи паспорта");
            res.Columns.Add("Кем выдан паспорт");
            res.Columns.Add("Работает");
            res.Columns.Add("Учится");
            res.Columns.Add("Есть дети");
            res.Columns.Add("Дата заселения");
            res.Columns.Add("Месячная плата");
            res.Columns.Add("Место работы");
            res.Columns.Add("Место учебы");
            return res;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            List<IChildData> children = DataManager.GetInstance().ChildrenRepository.Read().ToList();
            List<IParentsAndChildrenData> parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();

            List<IParentsAndChildrenData> parentsAndChildrenByParent = parentsAndChildren.FindAll(item => item.ParentId == _residents[index].Id);
            foreach (IParentsAndChildrenData parentAndChild in parentsAndChildrenByParent)
            {
                DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
            }

            DataManager.GetInstance().ResidentsRepository.Delete(_residents[index]);

            parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            foreach (IChildData child in children)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByChildren = parentsAndChildren.FindAll(item => item.ChildId == child.Id);
                if (parentsAndChildrenByChildren.Count == 0)
                {
                    DataManager.GetInstance().ChildrenRepository.Delete(child);
                }
            }

            _residents.Remove(_residents[index]);
        }
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable(_residents).Rows[index];
        }
    }
}