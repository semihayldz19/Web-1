using Course.InAndOutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Interfaces
{

    public partial interface IBase<Response, Form, FilterFrom>
    {
        Task<MethodResponse<Response>> SingleGet(int id, string currentUserId);
        Task<MethodResponse<List<Response>>> MultipleGet(FilterFrom form, string currentUserId);
        Task<MethodResponse<Response>> Add(Form form, string currentUserId);
        Task<MethodResponse<Response>> Update(Form form, string currentUserId);
        Task<MethodResponse<string>> Delete(int id, string currentUserId);
        Task<MethodResponse<string>> Delete(FilterFrom form, string currentUserId);
    }

    public partial interface IBase2<Response, Form, FilterFrom>
    {
        Task<MethodResponse<Response>> SingleGet(string id, string currentUserId);
        Task<MethodResponse<List<Response>>> MultipleGet(FilterFrom form, string currentUserId);
        Task<MethodResponse<Response>> Add(Form form, string currentUserId);
        Task<MethodResponse<Response>> Update(Form form, string currentUserId);
        Task<MethodResponse<string>> Delete(string id, string currentUserId);
        Task<MethodResponse<string>> Delete(FilterFrom form, string currentUserId);
    }


}


