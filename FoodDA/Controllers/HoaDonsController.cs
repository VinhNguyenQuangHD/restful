using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDA.Models;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace FoodDA.Controllers
{
    public class HoaDonsController : Controller
    {
        private FoodEntities db = new FoodEntities();

        // GET: HoaDons
        public ActionResult Index(int? id)
        {
            return HttpNotFound();
        }

        public async Task<ActionResult> Export(int? id, CancellationToken cancellationToken)
        {
            await Task.Yield();

            var bill = db.HoaDons
                .Include(b => b.NguoiDung)
                .FirstOrDefault(b => b.IdHoaDon == id);

            var list = db.ChiTietHoaDons
                .Include(b => b.SanPham)
                .Where(b => b.IdHoaDon == id);

            var total = list.Sum(b => b.SoLuong * b.SanPham.Gia);

            var stream = new MemoryStream();

            try
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("Hóa Đơn Số", typeof(string));
                Dt.Columns.Add("Sản Phẩm", typeof(string));
                Dt.Columns.Add("Đơn Giá", typeof(string));
                Dt.Columns.Add("Số Lượng", typeof(string));
                Dt.Columns.Add("Thành Tiền", typeof(string));

                foreach (var data in list)
                {
                    DataRow row = Dt.NewRow();
                    row[0] = data.IdHoaDon;
                    row[1] = data.SanPham.TenSP;
                    row[2] = data.SanPham.Gia;
                    row[3] = data.SoLuong;
                    row[4] = data.ThanhTien;
                    Dt.Rows.Add(row);
                }

                DataRow CustomerInfo = Dt.NewRow();
                CustomerInfo[0] = null;
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[3] = "Tổng Tiền: ";
                CustomerInfo[4] = bill.TongTien;
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[0] = null;
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[1] = "Thông Tin Giao Hàng: ";
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[1] = "Tên Khách Hàng: ";
                CustomerInfo[2] = bill.NguoiDung.TenND;
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[1] = "Số Điện Thoại: ";
                CustomerInfo[2] = bill.NguoiDung.Sdt;
                Dt.Rows.Add(CustomerInfo);

                CustomerInfo = Dt.NewRow();
                CustomerInfo[1] = "Địa Chỉ Giao Hàng: ";
                CustomerInfo[2] = bill.NguoiDung.DiaChi;
                Dt.Rows.Add(CustomerInfo);

                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.None);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 18;


                    worksheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();

                    byte[] data = excelPackage.GetAsByteArray() as byte[]; 
                    return File(data, "application/octet-stream", "ChiTietHoaDo_" + id + ".xlsx");
                }
            }
            catch (Exception)
            {
                return new EmptyResult();
            }


            /*using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;
    
                workSheet.Cells[workSheet.Cells[1, 1] + ":" + workSheet.Cells[1, 5]].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[workSheet.Cells[1, 1] + ":" + workSheet.Cells[1, 5]].Merge = true;
                workSheet.Cells[workSheet.Cells[1, 1] + ":" + workSheet.Cells[1, 5]].Style.Font.Bold = true;
                workSheet.Cells[workSheet.Cells[1, 1] + ":" + workSheet.Cells[1, 5]].Style.Font.Size = 14;
                workSheet.Cells[workSheet.Cells[1, 1] + ":" + workSheet.Cells[1, 5]].Value = "Hoá Đơn Số: " + id;
    
                workSheet.Cells[workSheet.Cells[2, 1] + ":" + workSheet.Cells[2, 5]].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[workSheet.Cells[2, 1] + ":" + workSheet.Cells[2, 5]].Merge = true;
                workSheet.Cells[workSheet.Cells[2, 1] + ":" + workSheet.Cells[2, 5]].Style.Font.Size = 12;
                workSheet.Cells[workSheet.Cells[2, 1] + ":" + workSheet.Cells[2, 5]].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";
                workSheet.Cells[workSheet.Cells[2, 1] + ":" + workSheet.Cells[2, 5]].Value = bill.Ngay;
    
                workSheet.Cells[workSheet.Cells[4, 1] + ":" + workSheet.Cells[4, 5]].Merge = true;
                workSheet.Cells[workSheet.Cells[4, 1] + ":" + workSheet.Cells[4, 5]].Style.Font.Size = 12;
                workSheet.Cells[workSheet.Cells[4, 1] + ":" + workSheet.Cells[4, 5]].Value = "Khách Hàng: " + bill.NguoiDung.TenND;
    
                workSheet.Cells[workSheet.Cells[5, 1] + ":" + workSheet.Cells[5, 5]].Merge = true;
                workSheet.Cells[workSheet.Cells[5, 1] + ":" + workSheet.Cells[5, 5]].Style.Font.Size = 12;
                workSheet.Cells[workSheet.Cells[5, 1] + ":" + workSheet.Cells[5, 5]].Value = "Số Điện Thoại: " + bill.NguoiDung.Sdt;
    
    
                workSheet.Cells[workSheet.Cells[6, 1] + ":" + workSheet.Cells[6, 5]].Merge = true;
                workSheet.Cells[workSheet.Cells[6, 1] + ":" + workSheet.Cells[6, 5]].Style.Font.Size = 12;
                workSheet.Cells[workSheet.Cells[6, 1] + ":" + workSheet.Cells[6, 5]].Value = "Địa Chỉ: " + bill.NguoiDung.DiaChi;
    
                workSheet.Row(8).Height = 20;
                workSheet.Row(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(8).Style.Font.Bold = true;
    
                workSheet.Column(1).Width = 6;
                workSheet.Column(2).Width = 30;
                workSheet.Column(3).Width = 12;
                workSheet.Column(4).Width = 14;
                workSheet.Column(5).Width = 16;
    
                workSheet.Cells[8, 1].Value = "STT";
                workSheet.Cells[8, 2].Value = "Tên Sách";
                workSheet.Cells[8, 3].Value = "Số Lượng";
                workSheet.Cells[8, 4].Value = "Đơn Giá";
                workSheet.Cells[8, 5].Value = "Thành Tiền";
                int recordIndex = 9;
    
                foreach (var item in list.ToList())
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[recordIndex, 2].Value = item.SanPham.TenSP;
                    workSheet.Cells[recordIndex, 3].Value = item.SoLuong;
                    workSheet.Cells[recordIndex, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[recordIndex, 4].Value = item.SanPham.Gia  ;
                    workSheet.Cells[recordIndex, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[recordIndex, 5].Value = item.SoLuong * item.SanPham.Gia;
                    workSheet.Cells[recordIndex, 5].Style.Font.Bold = true;
                    workSheet.Cells[recordIndex + 1, 5].Style.Font.Bold = true;
                    workSheet.Cells[recordIndex + 1, 5].Value = total;
                    recordIndex++;
                }
                workSheet.Cells[workSheet.Cells[recordIndex, 1] + ":" + workSheet.Cells[recordIndex, 4]].Merge = true;
                workSheet.Cells[workSheet.Cells[recordIndex, 1] + ":" + workSheet.Cells[recordIndex, 4]].Value = "Tổng Tiền";
                workSheet.Cells[workSheet.Cells[recordIndex, 1] + ":" + workSheet.Cells[recordIndex, 4]].Style.Font.Bold = true;
                p/ackage.Save();
            }*/
            //stream.Position = 0;
            //string excelName = $"Bill-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        //public actionresult index()
        //{
        //    var hoadons = db.hoadons.include(h => h.nguoidung);
        //    return view(hoadons.tolist());
        //}

        // GET: HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND");
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHoaDon,Ngay,TongTien,IdNguoiDung")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", hoaDon.IdNguoiDung);
            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", hoaDon.IdNguoiDung);
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHoaDon,Ngay,TongTien,IdNguoiDung")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNguoiDung = new SelectList(db.NguoiDungs, "IdNguoiDung", "TenND", hoaDon.IdNguoiDung);
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}