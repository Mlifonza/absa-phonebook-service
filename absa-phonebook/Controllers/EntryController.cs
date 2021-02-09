using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Models;
using Data;

namespace absa_phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        public string connect { get; set; }
        public EntryController(IConfiguration configuration)
        {
            connect = configuration["ConnectionStrings:phonebook"];
        }

        [Route("get")]
        [HttpGet("[action]")]
        public dynamic GetEntries()
        {
            try
            {
                var retval = phonebookDBContext.ExecuteProc<Entry>("[dbo].[sp_GetEntries]", null, phonebookDBContext.MyConnection(connect));
                return Newtonsoft.Json.JsonConvert.SerializeObject(retval);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Route("add")]
        [HttpPost("[action]")]
        public dynamic AddEntry([FromBody] Entry entry)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("name", entry.name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("number", entry.number, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var retval = phonebookDBContext.ExecuteProc<bool>("[dbo].[sp_AddEntry]", param, phonebookDBContext.MyConnection(connect)).FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(retval);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Route("remove/{id}")]
        [HttpPost("[action]")]
        public dynamic RemoveContact(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("id", id, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var retval = phonebookDBContext.ExecuteProc<bool>("[dbo].[sp_RemoveEntry]", param, phonebookDBContext.MyConnection(connect)).FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(retval);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Route("update/name/{id}/{name}")]
        [HttpPost("[action]")]
        public dynamic UpdateName(int id, string name)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("id", id, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("name", name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var retval = phonebookDBContext.ExecuteProc<bool>("[dbo].[sp_UpdateName]", param, phonebookDBContext.MyConnection(connect)).FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(retval);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Route("update/number/{id}/{number}")]
        [HttpPost("[action]")]
        public dynamic UpdateNumber(int id, string number)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("id", id, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("number", number, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var retval = phonebookDBContext.ExecuteProc<bool>("[dbo].[sp_UpdateNumber]", param, phonebookDBContext.MyConnection(connect)).FirstOrDefault();
                return Newtonsoft.Json.JsonConvert.SerializeObject(retval);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}