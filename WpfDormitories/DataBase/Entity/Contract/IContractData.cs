﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Contract
{
    public interface IContractData
    {
        uint Id { get; set; }
        [DisplayName("Номер документа")]
        uint DocumentNumber { get; set; }
        [DisplayName("Наименование документа")]
        string Name { get; set; }
        [DisplayName("Кто выдал")]
        string WhoGave { get; set; }
        [DisplayName("Дата начала действия")]
        DateOnly StartAction { get; set; }
        [DisplayName("Комментарий")]
        string Comment { get; set; }
    }
}
