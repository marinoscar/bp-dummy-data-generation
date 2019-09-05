using luval.bpddg.app.Entities;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Loader
{
    public class ProcessMaster
    {
        public IEnumerable<ProcessSim> GetSims(string filename)
        {
            var res = new List<ProcessSim>();
            using (var package = new ExcelPackage(new FileInfo(filename)))
            {
                using (var workbook = package.Workbook)
                {
                    using (var sheet = workbook.Worksheets[1])
                    {
                        var row = 2;
                        while (true)
                        {
                            if (string.IsNullOrWhiteSpace(Convert.ToString(sheet.Cells[row, 1].Value))) return res;
                            var item = new ProcessSim()
                            {
                                Region = Convert.ToString(sheet.Cells[row, 1].Value),
                                ServiceLine = Convert.ToString(sheet.Cells[row, 2].Value),
                                BusinessUnit = Convert.ToString(sheet.Cells[row, 3].Value),
                                Id = Convert.ToInt32(sheet.Cells[row, 4].Value),
                                Name = Convert.ToString(sheet.Cells[row, 5].Value),
                                StartDate = Convert.ToDateTime(sheet.Cells[row, 6].Value),
                                StartMinDate = Convert.ToInt32(sheet.Cells[row, 7].Value),
                                EndMinDate = Convert.ToInt32(sheet.Cells[row, 8].Value),
                                Interval = Convert.ToInt32(sheet.Cells[row, 9].Value),
                                MinTransaction = Convert.ToInt32(sheet.Cells[row, 10].Value),
                                MaxTransaction = Convert.ToInt32(sheet.Cells[row, 11].Value),
                                FPY = Convert.ToDouble(sheet.Cells[row, 12].Value),
                                BusinessException  = Convert.ToDouble(sheet.Cells[row, 13].Value),
                                SystemException = Convert.ToDouble(sheet.Cells[row, 14].Value),
                            };
                            res.Add(item);
                            row++;
                        }
                    }
                }
            }
        }
    }
}
