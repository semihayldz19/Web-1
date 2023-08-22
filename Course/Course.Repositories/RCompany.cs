using Course.Data;
using Course.Enums;
using Course.InAndOutModels;
using Course.Interfaces;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Course.Repositories
{
    internal class RCompany : ICompany
    {
        private object _context;

        public string? CompanyName { get; private set; }
        public string? Address { get; private set; }
        public string? CompanyAbout { get; private set; }
        public string? Phone { get; private set; }
        public string? Mail { get; private set; }
        public string? Website { get; private set; }

        public async Task<MethodResponse<MCompany.Response>> Add(MCompany.Form form, string currentUserId)
        {
            try
            {
                MethodResponse<MCompany.Response> mr = new MethodResponse<MCompany.Response>();

                var company = new Companys();
                company.Id = Guid.NewGuid().ToString();
                company.CompanyName = form.CompanyName;
                company.Address = form.Address;
                company.CompanyAbout = form.CompanyAbout;
                company.Phone = form.Phone;
                company.Mail = form.Mail;
                company.Website = form.Website;
                company.CreatedBy = currentUserId;
                company.CreatedOn = DateTime.Now;




                object value = _context.Companys.Add(company);
                _context.SaveChanges();

                var item = new MCompany().Response()
                    {
                    CompanyName = company.CompanyName;
                    Address = company.Address;
                    CompanyAbout = company.CompanyAbout;
                    Phone = company.Phone;
                    Mail = company.Mail;
                    Website = company.Website;

                };
                mr.Status = 200;
                mr.StatusTexts.Add("company eklendi");
                mr.Item = (MCompany.Response)item;
                return await Task.FromResult(mr);



            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<string>> Delete(string id, string currentUserId)
        {
            try
            {
                MethodResponse<string> mr = new MethodResponse<string>();
                var currentUser = _context.users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı Bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var companys = _context.Companys.Where(k => k.Id == id && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (companys == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("şirket Bulunamadı!");
                    return await Task.FromResult(mr);
                }
                companys.IsDeleted = EuIsDeleted.Yes;
                companys.DeletedBy = currentUserId;
                companys.DeletedOn = DateTime.Now;
                _context.SaveChanges();
                mr.StatusTexts.Add("şirket kaydı silindi");
            }
            catch (Exception ex)
            {


                throw await Task.FromResult(ex);
            }


        }

        public Task<MethodResponse<string>> Delete(MCompany.FilterForm form, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse<List<MCompany.Response>>> MultipleGet(MCompany.FilterForm form, string currentUserId)
        {

            try
            {
                MethodResponse<List<MCompany.Response>> mr =new MethodResponse<List<MCompany.Response>>();
                var currentUser = _context.users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı Bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var companys = (form c in _context.companys
                                select new  MCompany.Response
                                {
                                    Id = char.Id,
                                    Brand = char.Brand // bu kısmı kendşne uyarlla
                                }).ToList();
                mr.count = companys.Count;
                mr.Item = companys;

                return await Task.FromResult(mr);


            }
            catch (Exception ex)
            {


                throw await Task.FromResult(ex);

            }

        }

        public async Task<MethodResponse<MCompany.Response>> SingleGet(string id, string currentUserId)
        {
            try
            {

                MethodResponse<MCompany.Repsonse> mr = new MethodResponse<MCompany.Repsonse>();
                var currentUser = _context.users.Where
                    (k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı Bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var companys = _context.companys.Where(k => k.Id == id && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (companys == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("şirket Bulunamadı!");
                    return await Task.FromResult(mr);
                }
                MCompany.Response response = new()
                {
                    //Id = companys.Id,
                    //capaciy=companys.capat,
                    //brand
                    //number plate kendşne göre düzenle
                };
                mr.Item = response;
                return await Task.FromResult(mr);

            }

            catch (Exception ex)
            {

            throw await Task.FromResult(ex);
            }                                        

        }
    }

        public async Task<MethodResponse<MCompany.Response>> Update(MCompany.Form form, string currentUserId)
    {
        try
        {
            MethodResponse<MCompany.Response> mr = new MethodResponse<MCompany.Response>();
            var currentUser = _context.users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
            if (currentUser == null)
            {
                mr.Status = 400;
                mr.StatusTexts.Add("Kullanıcı Bulunamadı!");
                return await Task.FromResult(mr);
            }
            var companys = _context.companys.Where(a => a.Id == form.Id && a.IsDeleted == EuIsDeleted.No).FirstOrDefault();
            if (companys == null)
            {
                mr.Status = 400;
                mr.StatusTexts.Add("araç Bulunamadı!");
                return await Task.FromResult(mr);
            }
            if (mr.Status != 200)
            {
                return await Task.FromResult(mr);
            }
            //companys.numberplate = form.numberplte;
            //companys.capacity = ...;
            //    company.brand = ..;
            //    companys.createdby = currentUserId;
            //companys.updateon = DateTime.Now;     kendine göre düzenle 
            _context.SaveChanges();
            mr.StatusTexts.Add("araç başarıyla güncellendi");
            return await Task.FromResult(mr);
        }
        catch (Exception ex)
        {


            throw await Task.FromResult(mr);
        }
        



    }
}
}
