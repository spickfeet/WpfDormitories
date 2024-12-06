using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase;
using System.Data;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.Model.Services.Tables.ExportFormattedTables
{
    public class ExportRoomsTableService : RoomsTableService
    {
        private List<IDormData> _dorms;
        public virtual DataTable Read()
        {
            List<IRoomData> rooms = DataManager.GetInstance().RoomsRepository.Read().ToList();
            DataTable res = CreateDataTable();
            foreach (IRoomData room in rooms)
            {
                IDormData dorm = _dorms.Find(item => item.Id == room.DormId);
                res.Rows.Add(dorm.DormNumber, room.NumberRoom, room.RoomArea, room.Floor, room.TotalNumberPlaces, room.NumberFreePlaces);
            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Номер общежития");
            res.Columns.Add("Номер");
            res.Columns.Add("Площадь");
            res.Columns.Add("Этаж");
            res.Columns.Add("Общее число мест");
            res.Columns.Add("Число свободных мест");
            return res;
        }
    }
}
