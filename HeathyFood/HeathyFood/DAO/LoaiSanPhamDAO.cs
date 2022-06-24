using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeathyFood.DAO
{
    public class LoaiSanPhamDAO
    {
        private FoodHeathyContext db = new FoodHeathyContext();
        public List<LoaiSanPham> getList()
        {
            List<LoaiSanPham> list = db.LoaiSanPhams.ToList();
            return list;
        }
        public long getCount()
        {
            var count = db.LoaiSanPhams.Count();
            return count;
        }

        public LoaiSanPham getRow(int id)
        {
            var row = db.LoaiSanPhams.Where(s => s.MaLoai == id).FirstOrDefault();
            return row;
        }
        public void Insert(LoaiSanPham row)
        {
            db.LoaiSanPhams.Add(row);
            db.SaveChanges();

        }
        public void Update(LoaiSanPham row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void Delete(LoaiSanPham row)
        {
            db.LoaiSanPhams.Remove(row);
            db.SaveChanges();
        }
    }
}