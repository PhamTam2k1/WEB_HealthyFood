using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeathyFood.DAO
{
    public class AnhDAO
    {
        private FoodHeathyContext db = new FoodHeathyContext();
        public List<AnhSanPham> getList()
        {
            List<AnhSanPham> list = db.AnhSanPhams.ToList();



            return list;
        }
        public long getCount()
        {
            var count = db.AnhSanPhams.Count();
            return count;
        }

        public AnhSanPham getRow(int id)
        {
            var row = db.AnhSanPhams.Where(s => s.MaAnh == id).FirstOrDefault();
            return row;
        }
        public void Insert(AnhSanPham row)
        {
            db.AnhSanPhams.Add(row);
            db.SaveChanges();

        }
        public void Update(AnhSanPham row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void Delete(AnhSanPham row)
        {
            db.AnhSanPhams.Remove(row);
            db.SaveChanges();
        }
    }
}